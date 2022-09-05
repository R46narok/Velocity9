using MediatR;
using ZeroGravity.Application;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Exercises.Data.Entities;
using ZeroGravity.Services.Exercises.Data.Repositories;

namespace ZeroGravity.Services.Exercises.Queries.Muscles.GetAllMuscles;

public class GetAllMusclesQuery : IRequest<ApiResponse<List<Muscle>>>
{
    
}

public class GetAllMusclesHandler : IRequestHandler<GetAllMusclesQuery, ApiResponse<List<Muscle>>>
{
    private readonly IMuscleRepository _repository;

    public GetAllMusclesHandler(IMuscleRepository repository)
    {
        _repository = repository;
    }

    public async Task<ApiResponse<List<Muscle>>> Handle(GetAllMusclesQuery request, CancellationToken cancellationToken)
    {
        var muscles = _repository.GetAll();
        return new(muscles, 
            statusCode: StatusCode.Ok, 
            details: DetailsMessage.For(StatusCode.Ok));
    }
}