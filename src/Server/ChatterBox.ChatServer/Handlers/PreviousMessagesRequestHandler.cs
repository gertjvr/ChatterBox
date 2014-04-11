using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using ChatterBox.Core.Mapping;
using ChatterBox.Core.Persistence;
using ChatterBox.Domain.Aggregates.MessageAggregate;
using ChatterBox.Domain.Queries;
using ChatterBox.MessageContracts.Dtos;
using ChatterBox.MessageContracts.Requests;

namespace ChatterBox.ChatServer.Handlers
{
    public class PreviousMessagesRequestHandler : ScopedRequestHandler<PreviousMessagesRequest, PreviousMessagesResponse>
    {
        private readonly IMapToNew<Message, MessageDto> _mapper;

        public PreviousMessagesRequestHandler(
            Func<Owned<IUnitOfWork>> unitOfWork,
            IMapToNew<Message, MessageDto> mapper) 
            : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public override async Task<PreviousMessagesResponse> Execute(IUnitOfWork context, PreviousMessagesRequest request)
        {
            var repository = context.Repository<Message>();

            var messages = repository.Query(new PreviousMessagesQuery(request.FromId, request.NumberOfMessages));

            return new PreviousMessagesResponse(messages.Select(_mapper.Map).ToArray());
        }
    }
}