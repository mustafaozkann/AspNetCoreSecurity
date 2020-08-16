using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace WhiteBlackList.Web.Middlewares
{
    public class IPSafeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IpList _ipList;

        public IPSafeMiddleware(RequestDelegate next, IOptions<IpList> ipList)
        {
            this._next = next;
            this._ipList = ipList.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestApiAddress = context.Connection.RemoteIpAddress;
            var isWhiteList = _ipList.WhiteList.Any(x => IPAddress.Parse(x).Equals(requestApiAddress));

            if (!isWhiteList)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }

            await _next(context);
        }

    }
}
