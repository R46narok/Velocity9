using AutoMapper;
using MediatR;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Exercises.Data.Entities;
using ZeroGravity.Services.Exercises.Data.Repositories;

namespace ZeroGravity.Services.Exercises.Commands.Authors.CreateAuthor;

public class CreateAuthorCommand : IRequest<ApiResponse>
{
    public string ExternalId { get; set; }
    public string UserName { get; set; }
}

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, ApiResponse>
{
    private readonly IAuthorRepository _repository;
    private readonly IMapper _mapper;
    
    public CreateAuthorCommandHandler(IAuthorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ApiResponse> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = _mapper.Map<Author>(request);
        await _repository.CreateAsync(author);

        return new();
    }
}