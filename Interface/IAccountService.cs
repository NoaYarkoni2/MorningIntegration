using System.Collections.Generic;
using System.Threading.Tasks;
using MorningIntegration.Models;

namespace MorningIntegration.Interface
{
    public interface IAccountService
    {
        Task<String> GetToken(string id, string secret);
    }
}