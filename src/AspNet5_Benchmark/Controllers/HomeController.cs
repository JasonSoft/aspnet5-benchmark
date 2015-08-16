﻿// Copyright (c) .NET Foundation. All rights reserved. 
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace AspNet5_Benchmark.Controllers
{
    [Route("mvc")]
    public class HomeController : Controller
    {
        [HttpGet("plaintext")]
        public IActionResult Plaintext()
        {
            return new PlainTextActionResult();
        }

        [HttpGet("json")]
        [Produces("application/json")]
        public object Json()
        {
            return new { message = "Hello, World!" };
        }
        
        [HttpGet("view")]
        public ViewResult Index()
        {
            return View();
        }

        private class PlainTextActionResult : IActionResult
        {
            private static readonly byte[] _helloWorldPayload = Encoding.UTF8.GetBytes("Hello, World!");

            public Task ExecuteResultAsync(ActionContext context)
            {
                context.HttpContext.Response.StatusCode = 200;
                context.HttpContext.Response.ContentType = "text/plain";
                context.HttpContext.Response.ContentLength = _helloWorldPayload.Length;
                return context.HttpContext.Response.Body.WriteAsync(_helloWorldPayload, 0, _helloWorldPayload.Length);
            }
        }
    }
}
