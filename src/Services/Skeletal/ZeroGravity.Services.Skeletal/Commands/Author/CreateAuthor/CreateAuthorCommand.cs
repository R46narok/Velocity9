using AutoMapper;
using ErrorOr;
using MediatR;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Skeletal.Data.Entities;
using ZeroGravity.Services.Skeletal.Data.Repositories;

namespace ZeroGravity.Services.Skeletal.Commands.CreateAuthor;

public record CreateAuthorCommandResponse(int ExternalId);

public class CreateAuthorCommand : IRequest<ErrorOr<CreateAuthorCommandResponse>>
{
    public string UserName { get; set; }
    public string ExternalId { get; set; }
}

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, ErrorOr<CreateAuthorCommandResponse>>
{
    private readonly IMapper _mapper;
    private readonly IAuthorRepository _repository;

    public CreateAuthorCommandHandler(IMapper mapper, IAuthorRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<ErrorOr<CreateAuthorCommandResponse>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Author>(request);
        var id = await _repository.CreateAsync(entity);
        return new CreateAuthorCommandResponse(id);
    }
}