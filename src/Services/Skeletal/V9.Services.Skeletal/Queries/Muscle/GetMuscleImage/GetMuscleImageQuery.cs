using ErrorOr;
using MediatR;
using V9.Services.Skeletal.Data.Repositories;

namespace V9.Services.Skeletal.Queries.GetMuscleImage;

public record GetMuscleImageQuery(string Name) : IRequest<ErrorOr<byte[]>>;

public class GetMuscleImageQueryHandler : IRequestHandler<GetMuscleImageQuery, ErrorOr<byte[]>>
{
    private readonly IMuscleRepository _repository;

    public GetMuscleImageQueryHandler(IMuscleRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<byte[]>> Handle(GetMuscleImageQuery request, CancellationToken cancellationToken)
    {
        var muscle = await _repository.GetByNameAsync(request.Name);
        return muscle!.Image!;
    }
}