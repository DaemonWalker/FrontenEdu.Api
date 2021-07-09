using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontenEdu.Api.Models
{
    public class ResponseModel
    {
        public string Message { get; set; }
        public bool Result { get; set; }
        public object Data { get; set; }

        public static ResponseModel Success(string msg)
            => new() { Result = true, Message = msg };

        public static ResponseModel Failed(string msg)
            => new() { Result = false, Message = msg };

        public static ResponseModel SuccessData(object data = null)
            => new() { Result = true, Data = data };

        public static ResponseModel FailedData(object data = null)
            => new() { Result = false, Data = data };
    }
}
