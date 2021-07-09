using FrontenEdu.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontenEdu.Api.Filters
{
    public class HeaderCheckAttribute : ActionFilterAttribute
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey(nameof(PersonModel.Partition).ToLower()))
            {
                context.Result = new BadRequestObjectResult("Header缺少partition项");
                return Task.CompletedTask;
            }
            (context.ActionArguments.FirstOrDefault(p => p.Value is PersonModel).Value as PersonModel).Partition =
                context.HttpContext.Request.Headers[nameof(PersonModel.Partition).ToLower()];
            return next();
        }
    }
}
