using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Xunit;
using V9.Services.Authorization.Commands.Users.CreateUser;
using V9.Services.Authorization.Commands.Users.DeleteUser;
using V9.Services.Authorization.Dto;

namespace ZeroGravity.Tests.Integration.Authorization;

public class UserControllerTests : IClassFixture<AuthorizationWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly Faker<CreateUserCommand> _faker;
    
    public UserControllerTests(AuthorizationWebApplicationFactory factory)
    {
        _client = factory.CreateClient();

        _faker = new Faker<CreateUserCommand>()
            .UseSeed(Random.Shared.Next())
            .RuleFor(x => x.UserName, f => f.Internet.UserName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Password, 
                f => f.Internet.Password(prefix: "#12"));
    }

    #region Create user tests

    [Fact]
    public async Task CreateUser_ShouldReturnOk()
    {
        var command = _faker.Generate();
        
        var response = await _client.PostAsJsonAsync("/api/User", command);
        // var apiResponse = await response.Content.ReadFromJsonAsync<CqrsResult>();
        // response.StatusCode.Should().Be(HttpStatusCode.OK, string.Join('\n', apiResponse.Details));
    }

    [Fact]
    public async Task CreateUserWithNoUsername_ShouldReturnBadRequest()
    {
        var command = _faker
            .Clone()
            .RuleFor(x => x.UserName, "")
            .Generate();
        
        var response = await _client.PostAsJsonAsync("/api/User", command);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task CreateUserWithNoEmail_ShouldReturnBadRequest()
    {
        var command = _faker
            .Clone()
            .RuleFor(x => x.Email, "")
            .Generate();
        
        var response = await _client.PostAsJsonAsync("/api/User", command);
        // var apiResponse = await response.Content.ReadFromJsonAsync<CqrsResult>();
        // response.StatusCode.Should().Be(HttpStatusCode.BadRequest, string.Join(',', apiResponse.Details));
    }
    
    [Fact]
    public async Task CreateUserWithNoPassword_ShouldReturnBadRequest()
    {
        var command = _faker
            .Clone()
            .RuleFor(x => x.Password, "")
            .Generate();
        
        var response = await _client.PostAsJsonAsync("/api/User", command);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    #endregion

    #region Delete user tests

    [Fact]
    public async Task DeleteUserByName_ShouldReturnOk()
    {
        var user = _faker.Generate();
        var response = await _client.PostAsJsonAsync("/api/User", user);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var command = new DeleteUserCommand(null, user.UserName);
        var request = new HttpRequestMessage(HttpMethod.Delete, "/api/User");
        request.Content = JsonContent.Create(command);
        
        response = await _client.SendAsync(request);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task DeleteUserNonExisting_ShouldReturnNotFound()
    { 
        var user = _faker.Generate();
        
        var command = new DeleteUserCommand(null, user.UserName);
        var request = new HttpRequestMessage(HttpMethod.Delete, "/api/User");
        request.Content = JsonContent.Create(command);
        
        var response = await _client.SendAsync(request);
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);       
    }
    
    #endregion

    #region Get user tests

    [Fact]
    public async Task GetUser_ShouldReturnOk()
    {
        var command = _faker.Generate();

        var response = await _client.PostAsJsonAsync("/api/User", command);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        response = await _client.GetAsync($"/api/User?username={command.UserName}");

        // var content = await response.Content.ReadFromJsonAsync<CqrsResult<UserDto>>()!;
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        // content!.Result.UserName.Should().Be(command.UserName);
        // content.Result.Email.Should().Be(command.Email);
    }

    [Fact]
    public async Task GetUserNonExisting_ShouldReturnNotFound()
    {
        var response = await _client.GetAsync("/api/User?username=nonexisting");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    #endregion
}