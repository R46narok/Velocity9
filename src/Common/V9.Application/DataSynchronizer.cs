using AutoMapper;
using MediatR;
using Refit;

namespace V9.Application;

public interface IRemoteSynchronizerProvider<TEntity>
{
    Task<IApiResponse<List<TEntity>>> GetAllAsync();
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

        if (remoteEntities.IsSuccessStatusCode)
        {
            var mappedEntities = remoteEntities.Content
                .Select(e => _mapper.Map<TCreateCommand>(e))
                .ToList();

            foreach (var entity in mappedEntities)
            {
                await _mediator.Send(entity);
            }
        }
        else
        {
            throw new Exception();
        }
        
    }
}