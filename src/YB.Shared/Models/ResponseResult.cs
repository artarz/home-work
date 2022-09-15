using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace YB.Shared.Models
{

    public class ResponseResult
    {
        public object Data { get; set; }
        public bool HasError { get; set; }
        public string Message { get; set; }
        public int? ErrorCode { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    }
}
