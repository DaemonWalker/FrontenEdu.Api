using FrontenEdu.Api.Filters;
using FrontenEdu.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontenEdu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [HeaderCheck]
    public class DataController : ControllerBase
    {
        private readonly IFreeSql freeSql;
        public DataController(IFreeSql freeSql)
        {
            this.freeSql = freeSql;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] PersonModel person)
        {
            if (await freeSql.Select<PersonModel>().AnyAsync(p => p.Email == person.Email && p.Partition == person.Partition))
            {
                return Ok(ResponseModel.Failed("Email已经存在"));
            }

            var result = await freeSql.Insert<PersonModel>(person).ExecuteAffrowsAsync();
            return Ok(ResponseModel.Success("添加成功"));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PersonModel person)
        {
            if (!await freeSql.Select<PersonModel>().AnyAsync(p => p.Id == person.Id && p.Partition == person.Partition))
            {
                return Ok(ResponseModel.Failed("用户信息不存在"));
            }

            var result = await freeSql.Update<PersonModel>().ExecuteAffrowsAsync();
            return Ok(ResponseModel.Success("修改成功"));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] PersonModel person)
        {
            var result = await freeSql.Delete<PersonModel>(person).ExecuteAffrowsAsync();
            if (result > 0)
            {
                return Ok(ResponseModel.Success("删除成功!"));
            }
            else
            {
                return Ok(ResponseModel.Failed("删除失败，用户不存在"));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Select([FromQuery] PersonModel person)
        {
            var select = freeSql.Select<PersonModel>().Where(p => p.Partition == person.Partition);
            if (!string.IsNullOrEmpty(person.Name))
            {
                select = select.Where(p => p.Name == person.Name);
            }
            if (!string.IsNullOrEmpty(person.Email))
            {
                select = select.Where(p => p.Email == person.Email);
            }
            var list = await select.ToListAsync();
            return Ok(ResponseModel.SuccessData(list));
        }
    }
}
