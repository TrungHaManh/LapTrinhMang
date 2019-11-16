using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RpcHandler.Message;
using System.Reflection;

namespace RpcHandler.Server
{
    // server sub
    public class ServerImport
    {
        private string name_space;
        public ServerImport(string @namespace) { name_space = @namespace; }
        public Response Run(Request request)
        {
            try
            {
                var assembly = Assembly.GetEntryAssembly().GetName();
                var type_class = Type.GetType($"{name_space}.{request.Class}, {assembly}");
                var constructor = type_class.GetConstructor(Type.EmptyTypes);
                var instance = constructor.Invoke(null);
                var method = type_class.GetMethod(request.Method);
                var parameters = method.GetParameters().Select(p => Convert.ChangeType(request.parameters[p.Name], p.ParameterType)).ToArray();
                Response res = new Response
                {
                    Success = true,
                    Phrase = request.Method
                };
                if (method.ReturnType == typeof(void))
                {
                    method.Invoke(instance, parameters);
                    res.Result = null;
                }
                else
                {
                    res.Result = method.Invoke(instance, parameters);
                }
                return res;
            }
            catch(Exception e)
            {
                return new Response
                {
                    Success = false,
                    Phrase = e.Message,
                    Result = null
                };
            }
        }
    }
}
