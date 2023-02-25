using AutoMapper;
using MediatR;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Coach.Data.Repositories;

namespace ZeroGravity.Services.Coach.Commands;

public record CreateExerciseCommand(string Name, int ExternalId) 
    : IRequest<CqrsResult>;

public class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseCommand, CqrsResult>
{
    private readonly IExerciseRepository _repository;
    private readonly IMapper _mapper;

    public CreateExerciseCommandHandler(IExerciseRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CqrsResult> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
    {
        return null;
    }
}