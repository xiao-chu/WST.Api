using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//引用
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WST.DAL;
namespace WSTWeb.API.Models
{
    public class MyAuthorization:ActionFilterAttribute
    {
        UserDal dal;

        public MyAuthorization(UserDal _dal)
        {
            dal = _dal;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string id = context.HttpContext.Request.Cookies["userId"]?.ToString();
            string url = context.HttpContext.Request.Path.ToString();
            var list = dal.GetJurisdictions(int.Parse(id));
            int cout = list.Where(j => j.Jurl.Equals(url)).Count();
            if (cout<=0)
            {
                context.Result = new JsonResult("-3");
            }
            base.OnActionExecuting(context);
        }
    }
}
