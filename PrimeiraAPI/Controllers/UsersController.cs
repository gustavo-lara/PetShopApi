using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Models;

namespace PrimeiraAPI.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        //[Authorize]
        [HttpPost("api/register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }

        // GET : api/users - Listar todos os usuários
        [HttpGet("api/users")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users != null)
            {
                return Ok(users);
            }
            return NoContent();
        }

        // GET : api/users/{id} - Buscar usuário por id
        [HttpGet("api/users/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NoContent();
        }

        // PUT : api/users/{id} - Atualizar usuário
        [HttpPut("api/users/{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] RegisterModel model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.UserName = model.UserName;
                user.Email = model.Email;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Ok();
                }
                return BadRequest();
            }
            return NoContent();
        }

        // DELETE : api/users/{id} - Deletar usuário
        [HttpDelete("api/users/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Ok();
                }
                return BadRequest();
            }
            return NoContent();
        }
    }
}
