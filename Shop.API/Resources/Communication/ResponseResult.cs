using System.Collections.Generic;

namespace Shop.API.Resources.Communication
{
    public class ResponseResult
    {
        public object Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}