using FileService.Models;
using FileService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Transactions;

namespace FileService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ILeadershipRepository _leadershipRepository;

        public FileController(ILeadershipRepository leadershipRepository)
        {
            _leadershipRepository = leadershipRepository;
        }


        [HttpDelete("DeleteFile")]
        public IActionResult DeleteFile(string fileId)
        {
            _leadershipRepository.DeleteFile(fileId);
            return new OkResult();
        }

        [HttpPut("ChangeFileName")]
        public IActionResult UpdateFileName([FromBody] string fileName, string fileId)
        {
            if (fileName != null && fileId != null)
            {
                using (var scope = new TransactionScope())
                {
                    _leadershipRepository.ChangeFileName(fileName, fileId);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }


        //AddLessonFile
        [HttpPost("UploadFile")]
        public IActionResult UploadFile(IFormFile file,string fileId, string fileName, string subjectId, CancellationToken cancellationtoken)
        {
            _leadershipRepository.WriteFile(file, fileName, subjectId, fileId);
            return new OkObjectResult(fileName);
        }

        [HttpGet("GetAllFile")]
        public IActionResult GetAllFile(string fileId)
        {
            var file = _leadershipRepository.GetAllFile(fileId);
            return new OkObjectResult(file);
        }

        [HttpGet("GetFileByName")]
        public IActionResult GetFileByName(string fileName)
        {
            var file = _leadershipRepository.GetFileByName(fileName);
            return new OkObjectResult(file);
        }

        [HttpGet("GetFileBySubject")]
        public IActionResult GetFileBySubject(string subjectId)
        {
            var file = _leadershipRepository.GetFileBySubject(subjectId);
            return new OkObjectResult(file);
        }

        [HttpGet]
        [Route("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", fileName);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes, contenttype, Path.GetFileName(filepath));
        }
    }
}
