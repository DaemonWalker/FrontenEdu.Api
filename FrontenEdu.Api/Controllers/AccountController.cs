using FrontenEdu.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontenEdu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IFreeSql freeSql;
        private readonly IPasswordHasher<LoginModel> passwordHasher;
        public AccountController(IFreeSql freeSql, IPasswordHasher<LoginModel> passwordHasher)
        {
            this.freeSql = freeSql;
            this.passwordHasher = passwordHasher;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var dbLogin = await freeSql.Select<LoginModel>().Where(p => p.UserName == login.UserName).FirstAsync();
            if (passwordHasher.VerifyHashedPassword(login, dbLogin.Password, login.Password) == PasswordVerificationResult.Success)
            {
                return Ok(ResponseModel.SuccessData(dbLogin));
            }
            else
            {
                return Ok(ResponseModel.FailedData());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LoginModel login)
        {
            login.Password = passwordHasher.HashPassword(login, login.Password);
            var result = await freeSql.Insert(login).OnDuplicateKeyUpdate().ExecuteAffrowsAsync();
            return Ok(result);
        }
    }
}
