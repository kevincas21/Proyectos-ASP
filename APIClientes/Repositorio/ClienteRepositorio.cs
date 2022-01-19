using APIClientes.Data;
using APIClientes.Model;
using APIClientes.Model.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIClientes.Repositorio
{
    public class ClienteRepositorio : IClienteRepositorio
    {

        private readonly ApplicationDBContext _db;
        private IMapper _mapper;

        public ClienteRepositorio(ApplicationDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }

        public async Task<ClientDTO> CreateUpdate(ClientDTO clientDTO)
        {
            Client client = _mapper.Map<ClientDTO, Client>(clientDTO);
            if(client.Id > 0)
            {
                _db.Clients.Update(client);
            }
            else
            {
                await _db.Clients.AddAsync(client);
            }
            await _db.SaveChangesAsync();

            return _mapper.Map<Client, ClientDTO>(client);
        }

        public async Task<bool> DeleteCliente(int id)
        {
            try
            {
                Client client = await _db.Clients.FindAsync(id);
                if(client == null)
                {
                    return false;
                }
                _db.Clients.Remove(client);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ClientDTO> GetClientById(int id)
        {
            Client cliente = await _db.Clients.FindAsync(id);

            return _mapper.Map<ClientDTO>(cliente);
        }

        public async Task<List<ClientDTO>> GetClients()
        {
            List<Client> lista = await _db.Clients.ToListAsync();

            return _mapper.Map<List<ClientDTO>>(lista);
        }
    }
}
