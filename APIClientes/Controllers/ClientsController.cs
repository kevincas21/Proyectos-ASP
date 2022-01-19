using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIClientes.Data;
using APIClientes.Model;
using APIClientes.Repositorio;
using APIClientes.Model.DTO;
using Microsoft.AspNetCore.Authorization;

namespace APIClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ClientsController : ControllerBase
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        protected ResponseDTO _reponse;

        public ClientsController(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
            _reponse = new ResponseDTO();
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            try
            {
                var lista = await _clienteRepositorio.GetClients();
                _reponse.Result = lista;
                _reponse.DisplayMessage = "Lista de Clientes";
            }
            catch (Exception ex)
            {

                _reponse.IsSucces = false;
                _reponse.ErrorMessages = new List<string> { ex.ToString() };

            }

            return Ok(_reponse);
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var cliente = await _clienteRepositorio.GetClientById(id);
            if (cliente == null)
            {
                _reponse.IsSucces = false;
                _reponse.DisplayMessage = "Cliente no existe";
                return NotFound(_reponse);
            }
            _reponse.Result = cliente;
            _reponse.DisplayMessage = "Informacion del cliente";
            return Ok(_reponse);
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, ClientDTO client)
        {
            try
            {
                ClientDTO model = await _clienteRepositorio.CreateUpdate(client);
                _reponse.Result = model;
                return Ok();
            }
            catch (Exception ex)
            {
                _reponse.IsSucces = false;
                _reponse.DisplayMessage = "Error al actualizar";
                _reponse.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_reponse);
            }
        }

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(ClientDTO client)
        {
            try
            {
                ClientDTO model = await _clienteRepositorio.CreateUpdate(client);
                
                _reponse.Result = model;
                return Ok(_reponse);
            }
            catch(Exception ex)
            {
                _reponse.IsSucces = false;
                _reponse.DisplayMessage = "Error al actualizar";
                _reponse.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_reponse);

            }
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                bool Delete = await _clienteRepositorio.DeleteCliente(id);
                if (Delete)
                {
                    _reponse.Result = Delete;
                    _reponse.DisplayMessage = "Cliente eliminado con exito";
                    return Ok(_reponse);
                }

                else
                {
                    _reponse.IsSucces = false;
                    _reponse.DisplayMessage = "Error al eliminar Cliente";
                    return BadRequest(_reponse);
                }
            }
            catch(Exception ex)
            {
                _reponse.IsSucces = false;
                _reponse.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_reponse);
            }

        }

        
    }
}
