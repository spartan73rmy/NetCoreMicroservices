using Existence.grpc.Protos;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace Existence.grpc.Services
{
    public class HelloWorldService : HelloWorld.HelloWorldBase
    {
        public override Task<Protos.HelloReply> SayHello(Protos.HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new Protos.HelloReply
            {
                Message = $"Por alguna razon rara es el parametro 1 : {request.Name} \n Pasaron: {DateTime.Now.Year - request.Year} años"
            });
        }
    }
}
