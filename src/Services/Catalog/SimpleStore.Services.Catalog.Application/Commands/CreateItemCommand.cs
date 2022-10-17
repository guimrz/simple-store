using MediatR;
using SimpleStore.Services.Catalog.Objects.Requests;
using SimpleStore.Services.Catalog.Objects.Responses;

namespace SimpleStore.Services.Catalog.Application.Commands
{
    public class CreateItemCommand : CreateItemRequest, IRequest<ItemResponse>
    {
        //
    }
}
