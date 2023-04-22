using AutoMapper;
using ErrorOr;
using MediatR;
using V9.Services.Skeletal.Data.Repositories;
using V9.Services.Skeletal.Dto;

namespace V9.Services.Skeletal.Queries;

public class GetExerciseQuery : IRequest<ErrorOr<ExerciseDto>>
{
    public string? Name { get; set; }

    public GetExerciseQuery(string? name)
    {
        Name = name;
    }
}

public class GetExerciseQueryHandler : IRequestHandler<GetExerciseQuery, ErrorOr<ExerciseDto>>
{
    private readonly IMapper _mapper;
    private readonly IExerciseRepository _repository;

    public GetExerciseQueryHandler(IMapper mapper, IExerciseRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<ErrorOr<ExerciseDto>> Handle(GetExerciseQuery request, CancellationToken cancellationToken)
    {
        var entry = await _repository.GetByNameAsync(request.Name!, false);
        var dto = _mapper.Map<ExerciseDto>(entry);

        return dto;
    }
}
