using AutoMapper;
using MediatR;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Skeletal.Data.Entities;
using ZeroGravity.Services.Skeletal.Data.Repositories;
using ZeroGravity.Services.Skeletal.Dto;

namespace ZeroGravity.Services.Skeletal.Queries.GetAllExercises;

public record GetAllExercisesQuery : IRequest<CqrsResult<List<ExerciseDto>>>;

public class GetAllExercisesQueryHandler : IRequestHandler<GetAllExercisesQuery, CqrsResult<List<ExerciseDto>>>
{
    private readonly IMapper _mapper;
    private readonly IExerciseRepository _repository;

    public GetAllExercisesQueryHandler(IMapper mapper, IExerciseRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<CqrsResult<List<ExerciseDto>>> Handle(GetAllExercisesQuery request, CancellationToken cancellationToken)
    {
        var exercises = _repository
            .GetAll()
            .Select(x => _mapper.Map<ExerciseDto>(x))
            .ToList();

        return new(exercises);
    }
}