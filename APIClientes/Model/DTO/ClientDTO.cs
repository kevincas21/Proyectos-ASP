using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIClientes.Model.DTO
{
    public class ClientDTO
    {
        
        public int Id { get; set; }

       
        public string Nombre { get; set; }

        
        public string Apellido { get; set; }

        
        public string Direccion { get; set; }

        
        public int Telefono { get; set; }
    }
}
