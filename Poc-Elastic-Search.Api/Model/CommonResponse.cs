using System.Collections.Generic;

namespace Poc_Elastic_Search.Api.Model
{
    public class CommonResponse<T> where T: class
    {

        public T Data { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }

    }
}
