using Nest;

namespace Poc_Elastic_Search.Api.Model
{
    public class StatusResponse
    {
        public int Id { get; set; }
        public string Index { get; set; }
        public bool IsValid { get; set; }
        public Result Result { get; set; }
        public string Type { get; set; }
        public bool IsSuccess { get; set; }
    }
}
