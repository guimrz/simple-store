using MediatR;
using SimpleStore.Services.Catalog.Objects.Responses;

namespace SimpleStore.Services.Catalog.Application.Commands.Handlers
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, CreateItemResponse>
    {
        public Task<CreateItemResponse> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
