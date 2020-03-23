using System.Threading.Tasks;
using Tutor101.Service.BO.Security;

namespace Tutor101.Service.Services.Security
{
    public interface ISecurityService
    {
        Task<LoginBO> LoginAsync(LoginBO loginBO);
    }
}
