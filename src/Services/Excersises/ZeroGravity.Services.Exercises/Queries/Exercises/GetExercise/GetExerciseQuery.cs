using AutoMapper;
using MediatR;
using ZeroGravity.Application;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Exercises.Data.Entities;
using ZeroGravity.Services.Exercises.Data.Repositories;
using ZeroGravity.Services.Exercises.Dto;

namespace ZeroGravity.Services.Exercises.Queries.Exercises.GetExercise;

public class GetExerciseQuery : IRequest<ApiResponse<ExerciseDto>>
{
    public int Id { get; set; }

    public GetExerciseQuery(int id)
    {
        Id = id;
    }
}

public class GetExerciseQueryHandler : IRequestHandler<GetExerciseQuery, ApiResponse<ExerciseDto>>
{
    private readonly IExerciseRepository _repository;
    private readonly IMapper _mapper;
    private readonly IMessagePublisher _publisher;

    public GetExerciseQueryHandler(IExerciseRepository repository, IMapper mapper, IMessagePublisher publisher)
    {
        _repository = repository;
        _mapper = mapper;
        _publisher = publisher;
    }
    
    public async Task<ApiResponse<ExerciseDto>> Handle(GetExerciseQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        var dto = _mapper.Map<ExerciseDto>(entity);
        
        return new(dto,
            details: DetailsMessage.For(StatusCode.Fetched, nameof(Exercise)));
    }
}