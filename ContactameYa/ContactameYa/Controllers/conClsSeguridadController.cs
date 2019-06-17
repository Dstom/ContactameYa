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

        private conDTOtDepartamento PobjDepartamento = new conDTOtDepartamento();
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
                rm.href = Url.Content("/conClsServicio/conFrmListarServiciosVista/");
            }
            return Json(rm);
        }
        public ActionResult mtdCerrarSesion()
        {
            SessionHelper.DestroyUserSession();
            return RedirectToAction("conFrmListarServiciosVista", "conClsServicio");


            //return Redirect("~/cnfFrmSeguridadVista");
        }

        //Registrar Cuentas Nuevas

        public ActionResult conFrmRegistrarUsuario(int tipousuario)
        {
            ViewBag.lstDepartamentos = PobjDepartamento.mtdListar();

            conUSUpUsuario LobjUsuario = new conUSUpUsuario();
            LobjUsuario.TIUid_tipo_usuario = tipousuario;
            LobjUsuario.USUestado = "A";
            return View(LobjUsuario);
            
        }

        public ActionResult mtdRegistrarUsuario(conUSUpUsuario xGobjUsuarioModelo)
        {
            /*xGobjUsuarioModelo.USUlatitud = decimal.Parse(xGobjUsuarioModelo.USUlatitud.ToString().Replace(".", ","));
            xGobjUsuarioModelo.USUlongitud = decimal.Parse(xGobjUsuarioModelo.USUlongitud.ToString().Replace(".", ","));*/

            if (ModelState.IsValid)
            {
                xGobjUsuarioModelo.USUclave = HashHelper.SHA1(xGobjUsuarioModelo.USUclave);
                xGobjUsuarioModelo.mtdGuardar();
            }
            else
            {
                ViewBag.lstDepartamentos = PobjDepartamento.mtdListar();
                return View("conFrmRegistrarUsuario", xGobjUsuarioModelo);
            }
            return Redirect("");
        }
    }
}