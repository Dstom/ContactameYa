using ContactameYa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactameYa.Controllers
{
    public class conClsUsuarioController : Controller
    {
        private conUSUpUsuario PobjUsuario = new conUSUpUsuario();
        private conDTOtDepartamento PobjDepartamento = new conDTOtDepartamento();
        // GET: conClsUsuario
        public ActionResult conFrmModificarPerfilVista()
        {
            ViewBag.lstDepartamentos = mtdCargarDepartamentos();
            return View(PobjUsuario.mtdObtener(SessionHelper.GetUser()));
        }
        public object mtdCargarDepartamentos()
        {
            return PobjDepartamento.mtdListar();
        }
        public ActionResult mtdGuardarPerfil(conUSUpUsuario xGobjUsuarioModelo)
        {
            if (Request.Form["USUlatitud"].Contains(',') && Request.Form["USUlongitud"].Contains(','))
            {
                xGobjUsuarioModelo.USUlatitud = Convert.ToDecimal(Request.Form["USUlatitud"].Replace(',', '.'));
                xGobjUsuarioModelo.USUlongitud = Convert.ToDecimal(Request.Form["USUlongitud"].Replace(',', '.'));
            }
            try
            {
                xGobjUsuarioModelo.USUclave = HashHelper.SHA1(xGobjUsuarioModelo.USUclave);
                xGobjUsuarioModelo.mtdGuardar();
            }
            catch (Exception)
            {
                ViewBag.lstDepartamentos = PobjDepartamento.mtdListar();
                return View("conFrmModificarPerfilVista", xGobjUsuarioModelo);
            }
            return Redirect("~/conClsUsuario/conFrmModificarPerfilVista");
        }
    }
}