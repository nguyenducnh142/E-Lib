using AccountService.DbContexts;
using AccountService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Principal;

namespace AccountService.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountContext _dbContext;

        public AccountRepository(AccountContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }


        public Account GetAccountDetail(string accountId)
        {
            return _dbContext.Accounts.Find(accountId);
        }

        public void InsertAccount(Account account)
        {
            _dbContext.Add(account);
            Save();
        }

        public string WriteFile(IFormFile file, string userId)
        {
            string filename = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                //filename = DateTime.Now.Ticks.ToString() + extension;
                filename = userId + extension;

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
            }
            return filename;
        }

        public void DeleteAvatar(string fileName)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");
            string filePath = fileName;

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filePath);
            File.SetAttributes(filepath, FileAttributes.Normal);
            File.Delete(exactpath);
        }

        public string ChangePassword(string userId, string oldPassword, string newPassword)
        {
            var account = _dbContext.Accounts.Find(userId);
            if (_dbContext.Accounts.Find(userId).Password == oldPassword)
            {
                account.Password = newPassword;
                Save();
                return "Change Password Success!!";
            }
            return "Change Password Fail!!";

        }

        
    }
}
