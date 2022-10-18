using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleStore.Core.EntityFrameworkCore.Abstractions;
using SimpleStore.Services.Basket.Application.Responses;
using SimpleStore.Services.Basket.Domain;

namespace SimpleStore.Services.Basket.Application.Queries.Handlers
{
    public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, CustomerBasketResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBasketQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CustomerBasketResponse> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            CustomerBasket? basket = await _unitOfWork.Repository<CustomerBasket>()
                .Entities
                .Include(basket => basket.Items)
                .AsNoTracking()
                .SingleOrDefaultAsync(basket => basket.BuyerId == request.BuyerId);

            if (basket is null)
            {
                basket = new CustomerBasket(request.BuyerId);
            }

            return _mapper.Map<CustomerBasketResponse>(basket);
        }
    }
}