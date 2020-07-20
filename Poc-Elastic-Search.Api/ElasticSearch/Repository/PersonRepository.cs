using Elasticsearch.Net;
using Nest;
using Poc_Elastic_Search.Api.Contract;
using Poc_Elastic_Search.Api.ElasticSearch.Client;
using Poc_Elastic_Search.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poc_Elastic_Search.Api.ElasticSearch.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private ElasticClient _client;

        public PersonRepository()
        {
            var client = new ElasticSearchClient();
            _client = client.Connect;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            var result = await _client.SearchAsync<Person>(p => p
                .Index("people")
                .Query(q => q
                    .MatchAll()
                ));

            if (result.Total > 0)
            {
                return result.Hits.Select(h =>
                {
                    h.Source.Id = int.Parse(h.Id);
                    return h.Source;
                }).ToList();
            }

            return null;
        }

        public async Task<Person> GetAsync(int id)
        {
            var result = await _client.GetAsync<Person>(id);
            return result.Source;
        }

        public async Task<IEnumerable<Person>> GetTermAsync(string term)
        {
            var result = _client.Search<Person>(q => q
            .Query(f => f
               .QueryString(t => t.Query(term + "*")))

            );

            if (result.IsValid)
                return result.Hits.Select(t => t.Source);
            else
                return null;
        }

        public async Task<Person> Save(Person person)
        {
            if (person.Id > 0)
                return await UpdateAsync(person);
            else
                return await CreateAsync(person);
        }

        public async Task<Person> UpdateAsync(Person person)
        {
            try
            {
                var updatedoc = await _client.GetAsync<Person>(person.Id);

                // Create partial document with a dynamic
                dynamic updateDoc = new System.Dynamic.ExpandoObject();
                updateDoc.FirstName = person.FirstName;
                updateDoc.LastName = person.LastName;
                updateDoc.Id = person.Id;
                updateDoc.ModifiedAt = DateTime.Now;

                var response = await _client.UpdateAsync<Person>(person.Id, u => u
                 .Index("people")
                 .Doc(new Person
                     {
                         FirstName = person.FirstName
                     })
                     .Refresh(Refresh.True)
                 );

                if (response.IsValid)
                    return response.Get.Source;
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Person> CreateAsync(Person person)
        {
            try
            {
                // Create an index
                var indexResponse = _client.IndexDocument(person);
                if (!indexResponse.IsValid)
                {
                    throw new Exception("Não foi possível criar o indice especificado");
                }

                var indexResponseAsync = await _client.IndexDocumentAsync(person);
                var status = new StatusResponse
                {
                    Id = int.Parse(indexResponseAsync.Id),
                    Index = indexResponseAsync.Index,
                    IsValid = indexResponseAsync.IsValid,
                    Result = indexResponseAsync.Result,
                    Type = indexResponseAsync.Type,
                    IsSuccess = indexResponseAsync.ApiCall.Success
                };

                if (indexResponseAsync.ApiCall.Success)
                    return person;
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
    }
}
