using AutoMapper;
using ErrorOr;
using MediatR;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Skeletal.Data.Repositories;
using ZeroGravity.Services.Skeletal.Dto;

namespace ZeroGravity.Services.Skeletal.Queries.GetAllFibers;

public record GetAllFibersQuery : IRequest<ErrorOr<List<FiberDto>>>;

public class GetAllFibersQueryHandler : IRequestHandler<GetAllFibersQuery, ErrorOr<List<FiberDto>>>
{
    private readonly IMapper _mapper;
    private readonly IFiberRepository _repository;

    public GetAllFibersQueryHandler(IMapper mapper, IFiberRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<ErrorOr<List<FiberDto>>> Handle(GetAllFibersQuery request, CancellationToken cancellationToken)
    {
        var fibers = _repository
            .GetAll()
            .Select(x => _mapper.Map<FiberDto>(x))
            .ToList();
        
        return fibers;
    }
}