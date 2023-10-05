using FileService.Models;
using FileService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Security.Claims;
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
        //Get Current UserDetail
        private string GetUserId()
        {
            string id = Convert.ToString(HttpContext.User.FindFirstValue("name"));
            return id;
        }

        //Delete File
        [HttpDelete("DeleteFile")]
        public IActionResult DeleteFile(string fileId)
        {
            _leadershipRepository.DeleteFile(fileId);
            return new OkResult();
        }

        //Change File Name
        [HttpPut("ChangeFileName")]
        public IActionResult UpdateFileName(string fileName, string fileId)
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


        //Add File
        [HttpPost("UploadFile")]
        public IActionResult UploadFile(IFormFile file,string userId, string fileId, string fileName, string subjectId, CancellationToken cancellationtoken)
        {
            _leadershipRepository.WriteFile(file, userId, fileName, subjectId, fileId);
            return new OkObjectResult(fileName);
        }

        //Get All File 
        [HttpGet("GetAllFile")]
        public IActionResult GetAllFile()
        {
            var files = _leadershipRepository.GetAllFile(GetUserId());
            return new OkObjectResult(files);
        }

        //Search File By FileName
        [HttpGet("GetFileByName")]
        public IActionResult GetFileByName( string fileName)
        {
            var file = _leadershipRepository.GetFileByName(GetUserId(), fileName);
            return new OkObjectResult(file);
        }


        //Search File By SubjectId
        [HttpGet("GetFileBySubject")]
        public IActionResult GetFileBySubject( string subjectId)
        {
            var file = _leadershipRepository.GetFileBySubject(GetUserId(), subjectId);
            return new OkObjectResult(file);
        }

        //Download File (fileId = fileId+.filetype)
        [HttpGet]
        [Route("DownloadFile")]
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
