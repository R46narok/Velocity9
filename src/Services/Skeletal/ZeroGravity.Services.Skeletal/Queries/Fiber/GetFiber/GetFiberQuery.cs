using AutoMapper;
using ErrorOr;
using MediatR;
using ZeroGravity.Services.Skeletal.Data.Repositories;
using ZeroGravity.Services.Skeletal.Dto;

namespace ZeroGravity.Services.Skeletal.Queries;

public record GetFiberQuery(string Name) : IRequest<ErrorOr<FiberDto>>;

public class GetFiberQueryHandler : IRequestHandler<GetFiberQuery, ErrorOr<FiberDto>>
{
    private readonly IFiberRepository _repository;
    private readonly IMapper _mapper;

    public GetFiberQueryHandler(IFiberRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ErrorOr<FiberDto>> Handle(GetFiberQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByNameAsync(request.Name, false);
        var dto = _mapper.Map<FiberDto>(entity);
        return dto;
    }
}