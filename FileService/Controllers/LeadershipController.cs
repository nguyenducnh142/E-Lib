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
    public class LeadershipController : ControllerBase
    {
        private readonly ILeadershipRepository _leadershipRepository;

        public LeadershipController(ILeadershipRepository leadershipRepository)
        {
            _leadershipRepository = leadershipRepository;
        }

        //Delete File
        [HttpDelete("/DeleteFile/{fileId}")]
        public async Task<IActionResult> DeleteFile(string fileId)
        {
            await _leadershipRepository.DeleteFile(fileId);
            return new OkResult();
        }

        //Change File Name
        [HttpPut("/ChangeFileName")]
        public async Task<IActionResult> UpdateFileName([FromBody] string fileName, string fileId)
        {
            if (fileName != null && fileId != null)
            {
                using (var scope = new TransactionScope())
                {
                    await _leadershipRepository.ChangeFileName(fileName, fileId);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }


        //Add File
        [HttpPost("/UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file,string userId, string fileId, string fileName, string subjectId, CancellationToken cancellationtoken)
        {
            await _leadershipRepository.WriteFile(file, userId, fileName, subjectId, fileId);
            return new OkObjectResult(fileName);
        }

        //Get All File 
        [HttpGet("/GetAllFile")]
        public async Task<IActionResult> GetAllFile(string userId)
        {
            var file = await _leadershipRepository.GetAllFile(userId);
            return new OkObjectResult(file);
        }

        //Search File By FileName
        [HttpGet("/GetFileByName/{fileName}")]
        public async Task<IActionResult> GetFileByName(string userId, string fileName)
        {
            var file = await _leadershipRepository.GetFileByName(userId, fileName);
            return new OkObjectResult(file);
        }


        //Search File By SubjectId
        [HttpGet("/GetFileBySubject/{subjectId}")]
        public async Task<IActionResult> GetFileBySubject(string userId, string subjectId)
        {
            var file = await _leadershipRepository.GetFileBySubject(userId, subjectId);
            return new OkObjectResult(file);
        }

        //Download File (fileId = fileId+.filetype)
        [HttpGet]
        [Route("/DownloadFile/{fileId}")]
        public async Task<IActionResult> DownloadFile(string fileId)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", fileId);

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
