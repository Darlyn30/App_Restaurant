﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Application.ViewModels.User;
using UberEats.Infrastructure.Persistence.Contexts;

namespace WebApi.UberEats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            //esto solo es para tener las cuentas de una forma rapida
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] SaveUserViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.CreateUserAsync(model);
            if (!result)
                return Conflict("El nombre de usuario ya existe.");

            return Ok(new { Message = "Usuario creado exitosamente." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
                return NotFound("Usuario no encontrado.");

            return Ok(new {Message = "Usuario eliminado exitosamente."});
        }
    }
}
