using APIClientes.Model;
using APIClientes.Model.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIClientes
{
    public class MappingConfiguration
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfiguration = new MapperConfiguration(config =>

            {
                config.CreateMap<ClientDTO, Client>();
                config.CreateMap<Client, ClientDTO>();
            });

            return mappingConfiguration;

        }
    }
}
