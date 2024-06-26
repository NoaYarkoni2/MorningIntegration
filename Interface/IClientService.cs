﻿using MorningIntegration.Models;

namespace MorningIntegration.Interface
{
    public interface IClientService
    {
        Task<Client> CreateClientAsync(Client client);
        Task<Client> UpdateClientAsync(string id, Client updatedClient);


    }
}