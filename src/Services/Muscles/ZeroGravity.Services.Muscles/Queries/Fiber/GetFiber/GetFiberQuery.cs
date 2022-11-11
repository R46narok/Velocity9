using AutoMapper;
using MediatR;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Muscles.Data.Repositories;
using ZeroGravity.Services.Muscles.Dto;

namespace ZeroGravity.Services.Muscles.Queries;

public record GetFiberQuery(string Name) : IRequest<ApiResponse<FiberDto>>;

public class GetFiberQueryHandler : IRequestHandler<GetFiberQuery, ApiResponse<FiberDto>>
{
    private readonly IFiberRepository _repository;
    private readonly IMapper _mapper;

    public GetFiberQueryHandler(IFiberRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ApiResponse<FiberDto>> Handle(GetFiberQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByNameAsync(request.Name, false);
        var dto = _mapper.Map<FiberDto>(entity);
        return new(dto, "Successfully retrieved a fiber");
    }
}