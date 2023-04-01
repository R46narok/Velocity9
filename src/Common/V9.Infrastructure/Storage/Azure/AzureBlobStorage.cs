using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace V9.Infrastructure.Storage.Azure;

public class AzureBlobStorage
{
    private readonly string _connectionString;
    private readonly string _containerName;
    private readonly BlobContainerClient _container;

    public AzureBlobStorage(string connectionString, string containerName)
    {
        _connectionString = connectionString;
        _containerName = containerName;
        _container = new BlobContainerClient(_connectionString, _containerName);
    }

    public async Task CreateBlobAsync(string blobName, Stream content)
    {
        await _container.CreateIfNotExistsAsync();
        var blob = _container.GetBlobClient(blobName);
        var response = await blob.UploadAsync(content);
    }

    public async Task<Stream> ReadBlobAsync(string blobName)
    {
        await _container.CreateIfNotExistsAsync();
        var blob = _container.GetBlobClient(blobName);
        var response = await blob.DownloadAsync();

        return response.Value.Content;
    }

    public async Task UpdateBlobAsync(string blobName, Stream content)
    {
        await _container.CreateIfNotExistsAsync();
        var blob = _container.GetBlobClient(blobName);
        var response = await blob.UploadAsync(content, overwrite: true);
    }

    public async Task DeleteBlobAsync(string blobName)
    {
        await _container.CreateIfNotExistsAsync();
        var blob = _container.GetBlobClient(blobName);
        var response = await blob.DeleteIfExistsAsync();
    }
}