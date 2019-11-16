using System;
using System.Collections.Generic;
namespace RpcHandler.Message
{
    [System.Serializable]
    public class Request
    {
        public string Class { get; set; }
        public string Method { get; set; }
        public Dictionary<string, object> parameters { get; set; }

    }
}
