

namespace HelpService.Repository
{
    public interface IHelpRepository
    {
        Task SendEmail(Message message);
    }
}
