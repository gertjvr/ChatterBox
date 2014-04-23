using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ChatterBox.MessageContracts.Messages.Commands;
using Nimbus.Handlers;
using Nimbus.MessageContracts;
using NUnit.Framework;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.MessageContracts.Conventions
{
    [TestFixture]
    public class AllRequestTypes
    {
        private readonly Dictionary<string, string> _knownDifferentials = new Dictionary<string, string>();

        [Test]
        [TestCaseSource("GetTypesToVerify")]
        public void ShouldEndWithRequest(Type requestType)
        {
            requestType.Name.ShouldEndWith("Request");
        }

        [Test]
        [TestCaseSource("GetTypesToVerify")]
        public void ShouldHaveAResponseWithACorrespondingName(Type requestType)
        {
            GetResponseType(requestType).ShouldNotBe(null);
        }

        [Test]
        [TestCaseSource("GetTypesToVerify")]
        public void ShouldHaveAtLeastOneHandler(Type requestType)
        {
            var responseType = GetResponseType(requestType);

            var handlerType = typeof (IHandleRequest<,>).MakeGenericType(requestType, responseType);

            var relevantHandlers = new[] {typeof (ChatServer) }
                .Select(t => t.Assembly)
                .SelectMany(a => a.GetExportedTypes())
                .Where(handlerType.IsAssignableFrom)
                .ToArray();

            relevantHandlers.ShouldNotBeEmpty();
        }

        public static IEnumerable<Type> GetTypesToVerify()
        {
            return typeof (SendMessageCommand)
                .Assembly
                .GetExportedTypes()
                .Where(t => t.IsClosedTypeOf(typeof (IBusRequest<,>)))
                .Where(t => !t.IsAbstract && !t.IsInterface);
        }

        private Type GetResponseType(Type requestType)
        {
            var responseTypeName = _knownDifferentials.ContainsKey(requestType.Name)
                ? requestType.Namespace + "." + _knownDifferentials[requestType.Name]
                : requestType.Namespace + "." + requestType.Name.Replace("Request", "Response");

            return requestType.Assembly.GetType(responseTypeName);;
        }
    }
}