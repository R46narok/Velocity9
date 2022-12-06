using AutoMapper;
using MediatR;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Skeletal.Data.Repositories;
using ZeroGravity.Services.Skeletal.Dto;

namespace ZeroGravity.Services.Skeletal.Queries;

public record GetFiberQuery(string Name) : IRequest<PipelineResult<FiberDto>>;

public class GetFiberQueryHandler : IRequestHandler<GetFiberQuery, PipelineResult<FiberDto>>
{
    private readonly IFiberRepository _repository;
    private readonly IMapper _mapper;

    public GetFiberQueryHandler(IFiberRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<PipelineResult<FiberDto>> Handle(GetFiberQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByNameAsync(request.Name, false);
        var dto = _mapper.Map<FiberDto>(entity);
        return new(dto, "Successfully retrieved a fiber");
    }
}