using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Xrm.Sdk.Workflow;

namespace AzureFuncTask.Service
{
    public class StorageService : IStorage
    {
        private readonly IConfiguration _configuration;

        public StorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Upload(IFormFile file)
        {
            string systemFileName = file.FileName;
            string blobstorage = _configuration.GetValue<string>("ConnectionString");
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorage);
            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(_configuration.GetValue<string>("ContainerName"));
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(systemFileName);
          
            await using (var data = file.OpenReadStream())
            {
                await blockBlob.UploadFromStreamAsync(data);
            }
        }
    }
}
