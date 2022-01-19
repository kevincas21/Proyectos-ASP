using APIClientes.Model;
using APIClientes.Model.DTO;
using APIClientes.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepositorio _userRepositorio;
        protected ResponseDTO _response; 


        public UsersController (IUserRepositorio userRepositorio)
        {
            _userRepositorio = userRepositorio;
            _response = new ResponseDTO();
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserDto user)
        {
            var respuesta = await _userRepositorio.Register(
                new User
                {
                    Username = user.UserName
                }, user.Password);
            if(respuesta == "existe")
            {
                _response.IsSucces = false;
                _response.DisplayMessage = "Usuario ya existe";
                return BadRequest(_response);
            }
            if(respuesta == "error")
            {
                _response.IsSucces = false;
                _response.DisplayMessage = "Error al crear el usuario";
                return BadRequest(_response);
            }

            _response.DisplayMessage = "Usuario Creado con exito";
            _response.Result = respuesta;
            JwTPackage jtp = new JwTPackage();
            jtp.UserName = user.UserName;
            jtp.Token = respuesta;
            _response.Result = jtp;

            return Ok(_response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserDto user)
        {
            var respuesta = await _userRepositorio.Login(user.UserName, user.Password);

            if (respuesta == "nouser") 
            {
                _response.IsSucces = false;
                _response.DisplayMessage = "usuario no existe";
                return BadRequest(_response);
            }
            else if (respuesta == "wrongPassword")
            {
                _response.IsSucces = false;
                _response.DisplayMessage = "contraseña incorrecta";
                return BadRequest(_response);

            }
            JwTPackage jtp = new JwTPackage();
            jtp.UserName = user.UserName;
            jtp.Token = respuesta;
            _response.Result = jtp;
            _response.DisplayMessage = "usuarioConectado";
            return Ok(_response);

        }
        public class JwTPackage
        {
            public string UserName { get; set; }
            public string Token { get; set; }
        }


    }
}
