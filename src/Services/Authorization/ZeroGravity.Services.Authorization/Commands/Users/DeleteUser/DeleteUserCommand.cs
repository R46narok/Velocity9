using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ZeroGravity.Application;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Authorization.Data.Entities;

namespace ZeroGravity.Services.Authorization.Commands.Users.DeleteUser;

public class DeleteUserCommand : IRequest<PipelineResult>
{
    public string? Id { get; set; }
    public string? UserName { get; set; } 
    
    public DeleteUserCommand(string? id, string? userName)
    {
        Id = id;
        UserName = userName;
    }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, PipelineResult>
{
    private readonly IMapper _mapper;
    private readonly IMessagePublisher _publisher;
    private readonly UserManager<User> _userManager;
    
    public DeleteUserCommandHandler(IMapper mapper, IMessagePublisher publisher, UserManager<User> userManager)
    {
        _mapper = mapper;
        _publisher = publisher;
        _userManager = userManager;
    }

    public async Task<PipelineResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await FindUserByNameOrId(request);
        var identityResult = await _userManager.DeleteAsync(user);

        if (identityResult.Succeeded)
        {
            var @event = _mapper.Map<UserDeletedEvent>(user);
            await _publisher.PublishTopicAsync(@event, MessageMetadata.Now(), cancellationToken);
            return new("Successfully deleted a user");
        }

        return new(identityResult.Errors.Select(x => x.Description), StatusCode.BadRequest);
    }

    private async Task<User> FindUserByNameOrId(DeleteUserCommand command)
    {
        if (command.Id is null) return await _userManager.FindByNameAsync(command.UserName);
        return await _userManager.FindByIdAsync(command.Id);
    }
}