using MorningIntegration.Models;

namespace MorningIntegration.Interface
{
    public interface IClientService
    {
        Task<Client> CreateClientAsync(Client client, string id, string secret);
        Task<Client> UpdateClientAsync(string clientId, Client updatedClient, string id, string secret);
    }
}