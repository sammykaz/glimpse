using System.Threading.Tasks;

namespace Glimpse.Core.Extensions
{
    public static class TaskExtensions
    {
        public static void Forget(this Task task)
        {
            task.ConfigureAwait(false);
        }
    }
}