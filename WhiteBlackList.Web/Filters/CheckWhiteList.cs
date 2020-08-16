using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using WhiteBlackList.Web.Middlewares;

namespace WhiteBlackList.Web.Filters
{
    public class CheckWhiteList : ActionFilterAttribute
    {
        private readonly IpList _ipList;

        public CheckWhiteList(IOptions<IpList> ipList)
        {
            _ipList = ipList.Value;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var requestIpAddress = context.HttpContext.Connection.RemoteIpAddress;
            var isWhiteList = _ipList.WhiteList.Any(x => IPAddress.Parse(x).Equals(requestIpAddress));

            if (!isWhiteList)
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
