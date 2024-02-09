using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMe.Service.Tests
{

    [Xunit.TraitAttribute("HandlerRegistration","Handler")]
    public class HandlerRegistrationTests
    {


        [Fact]
        public void AllRequests_ShouldHaveMatchingHandler()
        {
            var requestTypes = typeof(AccountMe.Service.Account.Commands.UpsertCommand).Assembly.GetTypes()
                .Where(IsRequest)
                .ToList();

            var handlerTypes = typeof(AccountMe.Service.Account.Handlers.UpsertHandler).Assembly.GetTypes()
                .Where(IsIRequestHandler)
                .ToList();

            foreach (var requestType in requestTypes) ShouldContainHandlerForRequest(handlerTypes, requestType);
        }

        private static void ShouldContainHandlerForRequest(IEnumerable<Type> handlerTypes, Type requestType)
        {
            bool found = false;
            foreach (var ht in handlerTypes)
            {
                if(IsHandlerForRequest(ht, requestType))
                {
                    if(found)                    
                        Assert.Fail($"{requestType.FullName} has multiple handler, one of those is {ht.FullName}. Every request must have only one handler.");
                    else
                        found = true;

                }

            }
            Assert.True(found, $"Missing handler for request type {requestType.FullName}");
            
        }

        private static bool IsRequest(Type type)
        {
            return typeof(IBaseRequest).IsAssignableFrom(type);
        }

        private static bool IsIRequestHandler(Type type)
        {
            return type.GetInterfaces().Any(interfaceType => interfaceType.IsGenericType && 
                (interfaceType.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) || (interfaceType.GetGenericTypeDefinition() == typeof(IRequestHandler<>))));
        }

        private static bool IsHandlerForRequest(Type handlerType, Type requestType)
        {
            return handlerType.GetInterfaces().Any(i => i.GenericTypeArguments.Any(ta => ta == requestType));
        }

    }
}
