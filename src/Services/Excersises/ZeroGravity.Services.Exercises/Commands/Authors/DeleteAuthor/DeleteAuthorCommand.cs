using AutoMapper;
using MediatR;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Exercises.Data.Entities;
using ZeroGravity.Services.Exercises.Data.Repositories;

namespace ZeroGravity.Services.Exercises.Commands.Authors.DeleteAuthor;

public class DeleteAuthorCommand : IRequest<ApiResponse>
{
    public string? ExternalId { get; set; }
    public string? UserName { get; set; }
}

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, ApiResponse>
{
    private readonly IMapper _mapper;
    private readonly IAuthorRepository _repository;

    public DeleteAuthorCommandHandler(IMapper mapper, IAuthorRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ApiResponse> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await FindUserByNameOrId(request);
        await _repository.DeleteAsync(author);
        return new();
    }
    
    private async Task<Author> FindUserByNameOrId(DeleteAuthorCommand command)
    {
        if (command.ExternalId is null) return await _repository.GetByUserNameAsync(command.UserName);
        return await _repository.GetByExternalIdAsync(command.ExternalId);
    }   
}