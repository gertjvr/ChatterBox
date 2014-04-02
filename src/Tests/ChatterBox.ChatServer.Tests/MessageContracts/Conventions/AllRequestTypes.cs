using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ChatterBox.MessageContracts.Commands;
using Nimbus.Handlers;
using Nimbus.MessageContracts;
using NUnit.Framework;
using Shouldly;

namespace ChatterBox.ChatServer.Tests.MessageContracts.Conventions
{
    [TestFixture]
    public class AllRequestTypes
    {
        readonly Dictionary<string, string> _knownDifferentials = new Dictionary<string, string>()
        {   
        };
            
        [Test]
        [TestCaseSource(typeof (TestCases))]
        public void ShouldEndWithRequest(Type requestType)
        {
            requestType.Name.ShouldEndWith("Request");
        }

        [Test]
        [TestCaseSource(typeof (TestCases))]
        public void ShouldHaveAResponseWithACorrespondingName(Type requestType)
        {
            GetResponseType(requestType).ShouldNotBe(null);
        }

        private Type GetResponseType(Type requestType)
        {
            var responseTypeName = _knownDifferentials.ContainsKey(requestType.Name)
                ? requestType.Namespace + "." + _knownDifferentials[requestType.Name]
                : requestType.Namespace + "." + requestType.Name.Replace("Request", "Response");

            return requestType.Assembly.GetType(responseTypeName);;
        }

        [Test]
        [TestCaseSource(typeof (TestCases))]
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

        internal class TestCases : IEnumerable<TestCaseData>
        {
            public IEnumerator<TestCaseData> GetEnumerator()
            {
                return typeof (CreateMessageCommand)
                    .Assembly
                    .GetExportedTypes()
                    .Where(t => t.IsClosedTypeOf(typeof (IBusRequest<,>)))
                    .Select(t => new TestCaseData(t)
                                .SetName(t.FullName))
                    .GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}