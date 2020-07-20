using Poc_Elastic_Search.Api.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poc_Elastic_Search.Api.Contract
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person> GetAsync(int id);
        Task<Person> Save(Person person);
        Task<IEnumerable<Person>> GetTermAsync(string term);
    }
}
