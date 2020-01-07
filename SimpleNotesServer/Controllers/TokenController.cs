using System;
using Microsoft.AspNetCore.Mvc;
using SimpleNotesServer.Data.Contexts;
using SimpleNotesServer.Data.Models.Requests;
using SimpleNotesServer.Data.Models.Responses;
using SimpleNotesServer.Data.Models.Users;
using SimpleNotesServer.Data.Repositories;
using SimpleNotesServer.Extensions.Repository;

namespace SimpleNotesServer.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : RepositoryController<SimpleServerRepository, BaseServerContext>
    {
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] RequestUser requestUser)
        {
            if (ModelState.IsValid)
            {
                if (!repository.AnyUser(user => user.Name == requestUser.Username))
                {
                    SimpleUser newUser = new SimpleUser(requestUser.Password, requestUser.Username);

                    newUser.RegistrationDate = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    newUser.LastEntrance = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                    repository.AddUser(newUser).SaveChangesAsync();

                    return Ok(new AuthSuccess("Успешная регистрация", newUser));
                }

                return BadRequest("Пользователь с таким именем уже существует в системе");
            }

            return BadRequest("Модель данных не валидна.");
        }

        [HttpPost("maketoken")]
        public IActionResult MakeToken([FromBody] RequestUser requestUser)
        {
            SimpleUser needleUser = repository.GetUserByNameAndPassword(requestUser.Username, requestUser.Password);

            if (needleUser != default)
            {
                needleUser.LastEntrance = DateTimeOffset.Now.ToUnixTimeSeconds();

                repository.SaveChangesAsync();

                return Ok(new AuthSuccess("Успешная авторизация", needleUser));
            }

            return BadRequest("Введены неверные данные пользователя.");
        }
    }
}
