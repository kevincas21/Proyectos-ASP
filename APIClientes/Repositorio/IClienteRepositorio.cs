using APIClientes.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIClientes.Repositorio
{
    public interface IClienteRepositorio
    {
        Task<List<ClientDTO>> GetClients();

        Task<ClientDTO> GetClientById(int id);

        Task<ClientDTO> CreateUpdate(ClientDTO clientDTO);

        Task<bool> DeleteCliente(int id);


    }
}
