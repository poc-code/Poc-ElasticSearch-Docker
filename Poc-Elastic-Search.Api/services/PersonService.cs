using Poc_Elastic_Search.Api.Contract;
using Poc_Elastic_Search.Api.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poc_Elastic_Search.Api.services
{
    public class PersonService : IPersonService
    {
        private IPersonRepository _repository;

        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Person> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<IEnumerable<Person>> GetTermAsync(string term)
        {
            return await _repository.GetTermAsync(term);
        }

        public async Task<Person> Save(Person person)
        {
            return await _repository.Save(person);
        }
    }
}
