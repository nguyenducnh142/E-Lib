using HelpService.DBContexts;
using HelpService.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelpService.Repository
{
    public class HelpRepository : IHelpRepository
    {
        private readonly HelpContext _dbContext;

        public HelpRepository(HelpContext helpContext)
        {
            _dbContext = helpContext;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public void SendHelp(string helpDetail)
        {
            var help = new Help();
            help.HelpDetail = helpDetail;
            _dbContext.Add(help);
            Save();

        }
    }
}
