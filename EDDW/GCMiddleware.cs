using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EDDW
{
    public class GCMiddleware
    {
        private readonly RequestDelegate _next;

        public GCMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IApplicationLifetime applicationLiftime)
        {
            await _next(httpContext);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Process currentProcess = Process.GetCurrentProcess();
            
            long PrivateMemorySize64 = currentProcess.PrivateMemorySize64;

            if (PrivateMemorySize64 > 600000000)
            {
                applicationLiftime.StopApplication();
            }

            Debug.WriteLine("Basem Called");

        }
    }
}
