using System;

namespace RpcHandler.Message
{
    [System.Serializable]
    public class Response 
    {
        public bool Success { get; set; }
        public string Phrase { get; set; }
        public object Result { get; set; }

    }
}
