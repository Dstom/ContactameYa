using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Routing;
using ContactameYa.Models;
using System.Web.Mvc;


namespace ContactameYa.Filters
{
    public class AutenticadoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!SessionHelper.ExistUserInSession())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "cnfClsSeguridad",
                    action = "cnfFrmSeguridadVista",
                    area = "cnfSeguridad"
                }));
            }
        }
    }

    // Si estamos logeado ya no podemos acceder a la página de Login
    public class NoLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (SessionHelper.ExistUserInSession())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "cnfClsUsuario",
                    action = "cnfFrmUsuarioVista",
                    area = "cnfMantenimiento"
                }));
            }
        }
    }
}