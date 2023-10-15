using Microsoft.AspNetCore.Identity;
using Domain.DTO;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
namespace DynamicAuthApi.Middlewaare
{
    public class UnAuthorized : IMiddleware
    {
        
        public  async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var endpoint = context.GetEndpoint();

            // Check if the endpoint has the [Authorize] attribute
            var hasAuthorizeAttribute = endpoint?.Metadata.GetMetadata<AuthorizeAttribute>() != null;
            if (hasAuthorizeAttribute)
            {
            await next(context);

                if (!context.User.Identity.IsAuthenticated)
                {

                     //await context.Response.WriteAsync("you have to login first");
                }
                else
                {

                    //await context.Response.WriteAsync("unauthorized");
                }
            }
            
            await next(context);

        }
    }
}
