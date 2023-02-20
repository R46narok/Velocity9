using AutoMapper;
using MediatR;
using Refit;
using ZeroGravity.Domain.Types;

namespace ZeroGravity.Application;

public interface IRemoteSynchronizerProvider<TEntity>
{
    Task<IApiResponse<CqrsResult<List<TEntity>>>> GetAllAsync();
}

public class DataSynchronizer<TRemoteEntity, TCreateCommand>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IRemoteSynchronizerProvider<TRemoteEntity> _remoteService;

    public DataSynchronizer(IMapper mapper, IMediator mediator, IRemoteSynchronizerProvider<TRemoteEntity> remoteService)
    {
        _mapper = mapper;
        _mediator = mediator;
        _remoteService = remoteService;
    }

    public async Task Synchronize()
    {
        var remoteEntities = await _remoteService.GetAllAsync();
        var mappedEntities = remoteEntities.Content.Result
            .Select(e => _mapper.Map<TCreateCommand>(e))
            .ToList();

        foreach (var entity in mappedEntities)
        {
            await _mediator.Send(entity);
        }
    }
}