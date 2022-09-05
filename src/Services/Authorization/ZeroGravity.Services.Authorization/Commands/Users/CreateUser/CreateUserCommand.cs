using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ZeroGravity.Application;
using ZeroGravity.Application.Infrastructure.MessageBrokers;
using ZeroGravity.Domain.Types;
using ZeroGravity.Services.Authorization.Data.Entities;

namespace ZeroGravity.Services.Authorization.Commands.Users.CreateUser;

public class CreateUserCommand : IRequest<ApiResponse>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public CreateUserCommand(string userName, string email, string password)
    {
        UserName = userName;
        Email = email;
        Password = password;
    }

    public CreateUserCommand() { }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IMessagePublisher _publisher;

    public CreateUserCommandHandler(IMapper mapper, IMessagePublisher publisher, UserManager<User> userManager)
    {
        _mapper = mapper;
        _publisher = publisher;
        _userManager = userManager;
    }
    
    public async Task<ApiResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        
        user.CreatedOn = DateTime.Now;
        user.UpdatedOn = DateTime.Now;

        var identityResult = await _userManager.CreateAsync(user, request.Password);

        if (identityResult.Succeeded)
        {
            await _userManager.AddClaimsAsync(user, new []
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "User")
            });

            var @event = _mapper.Map<UserCreatedEvent>(user);
            await _publisher.PublishTopicAsync(@event, MessageMetadata.Now(), cancellationToken);
            
            return new ApiResponse("Successfully created a user");
        }
        
        return new ApiResponse(identityResult.Errors.Select(x => x.Description), StatusCode.BadRequest);
    }
}
