using System.Threading.Tasks;

namespace Cleaner.Interface
{
    public interface ICommandService
    {
        Task ProcessCommand(string command);
    }
}