using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using ZeroGravity.Application;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Exercises.Data.Entities;
using ZeroGravity.Services.Exercises.Data.Repositories;
using ZeroGravity.Services.Exercises.Dto;

namespace ZeroGravity.Services.Exercises.Queries.Muscles.GetMuscle;

public class GetMuscleQuery : IRequest<ApiResponse<MuscleDto>>
{
    public int? Id { get; set; }
    public string? Group { get; set; }

    public GetMuscleQuery(int? id = null, string? group = null)
    {
        Id = id;
        Group = group;
    }
}

public class GetMuscleQueryHandler : IRequestHandler<GetMuscleQuery, ApiResponse<MuscleDto>>
{
    private readonly IMuscleRepository _repository;
    private readonly IMapper _mapper;

    public GetMuscleQueryHandler(IMuscleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<MuscleDto>> Handle(GetMuscleQuery request, CancellationToken cancellationToken)
    {
        Muscle entity;
        if (request.Group is not null)
            entity = (await _repository.GetByGroupAsync(request.Group, false))!;
        else
            entity = (await _repository.GetByIdAsync(request.Id!.Value, false))!;

        var dto = _mapper.Map<MuscleDto>(entity);

        return new(dto,
            details: DetailsMessage.For(StatusCode.Fetched, nameof(Muscle)));
    }
}