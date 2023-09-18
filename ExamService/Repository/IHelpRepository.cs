
using Microsoft.AspNetCore.Mvc;

namespace HelpService.Repository
{
    public interface IHelpRepository
    {
        void SendHelp(string helpDetail);
    }
}
