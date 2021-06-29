using EmployeeDetails.Entities.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EmployeeDetails.ExceptionHandling
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
