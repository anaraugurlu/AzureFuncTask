using AzureFuncTask.Service;
using Microsoft.AspNetCore.Mvc;


namespace AzureFuncTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {


        private readonly IStorage _storageService;
        private readonly IConfiguration _configuration;

        public UploadController(IStorage storage,IConfiguration configuration)
        {
            _storageService = storage;
            _configuration = configuration;
        }
        [HttpGet("Get")]
        public IActionResult Get()
        {
            return Ok("File Uploading ..");
        }

        [HttpPost(nameof(UploadFile))]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            await _storageService.Upload(file);

            return Ok("File Upload Successfully");
        }


       
    }
}