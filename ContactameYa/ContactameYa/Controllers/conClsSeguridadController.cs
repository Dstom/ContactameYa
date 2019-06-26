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
            if (Request.Form["USUlatitud"].Contains(',') && Request.Form["USUlongitud"].Contains(','))
            {
                xGobjUsuarioModelo.USUlatitud = Convert.ToDecimal(Request.Form["USUlatitud"].Replace(',', '.'));
                xGobjUsuarioModelo.USUlongitud = Convert.ToDecimal(Request.Form["USUlongitud"].Replace(',', '.'));
            }
            try
            {
                xGobjUsuarioModelo.USUclave = HashHelper.SHA1(xGobjUsuarioModelo.USUclave);
                //validar usuario unico
                if (PobjUsuario.mtdObtenerPorUsuario(xGobjUsuarioModelo.USUusuario) == null)
                {
                    xGobjUsuarioModelo.mtdGuardar();
                }
                else
                {
                    throw new Exception("El usuario ingresado ya existe.");
                }
            }
            catch(Exception ex)
            {
                ViewBag.lstDepartamentos = PobjDepartamento.mtdListar();
                ViewBag.Exception = ex.Message;
                return View("conFrmRegistrarUsuario", xGobjUsuarioModelo);
            }
            return Redirect("~/conClsServicio/conFrmListarServiciosVista");
        }
    }
}