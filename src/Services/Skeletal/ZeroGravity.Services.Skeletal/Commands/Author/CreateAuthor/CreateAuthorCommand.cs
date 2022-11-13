using AutoMapper;
using MediatR;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Skeletal.Data.Entities;
using ZeroGravity.Services.Skeletal.Data.Repositories;

namespace ZeroGravity.Services.Skeletal.Commands.CreateAuthor;

public class CreateAuthorCommand : IRequest<ApiResponse>
{
    public string UserName { get; set; }
    public string ExternalId { get; set; }
}

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, ApiResponse>
{
    private readonly IMapper _mapper;
    private readonly IAuthorRepository _repository;

    public CreateAuthorCommandHandler(IMapper mapper, IAuthorRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<ApiResponse> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Author>(request);
        await _repository.CreateAsync(entity);
        return new();
    }
}