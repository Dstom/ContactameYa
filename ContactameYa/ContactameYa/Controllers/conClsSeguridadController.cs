using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ContactameYa.Models;
using ContactameYa.Filters;

namespace ContactameYa.Controllers
{
    public class conClsSeguridadController : Controller
    {
        private conUSUpUsuario PobjUsuario = new conUSUpUsuario();
        // GET: conClsSeguridad
        [NoLogin]
        public ActionResult cnfFrmSeguridadVista()
        {
            return View();
        }
        //login
        public JsonResult mtdSeguridad(string Usuario, string Contraseña)
        {
            var rm = PobjUsuario.mtdSeguridad(Usuario, Contraseña);
            if (rm.response)
            {
                //rm.href = Url.Content("/Usuario");
                rm.href = Url.Content("/cnfMantenimiento/cnfClsUsuario/cnfFrmUsuarioVista");
            }
            return Json(rm);
        }
        public ActionResult mtdCerrarSesion()
        {
            SessionHelper.DestroyUserSession();
            return RedirectToAction("cnfFrmSeguridadVista", "cnfClsSeguridad", new { area = "cnfSeguridad" });


            //return Redirect("~/cnfFrmSeguridadVista");
        }

        public ActionResult mtdRegistrarUsuario(conUSUpUsuario xGobjUsuarioModelo)
        {
            if(ModelState.IsValid)
            {
                xGobjUsuarioModelo.USUclave = HashHelper.SHA1(xGobjUsuarioModelo.USUclave);
                xGobjUsuarioModelo.mtdGuardar();
            }
            return Redirect("");
        }
    }
}