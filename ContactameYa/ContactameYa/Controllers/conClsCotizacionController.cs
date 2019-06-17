using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ContactameYa.Models;

namespace ContactameYa.Controllers
{
    public class conClsCotizacionController : Controller
    {
        private conCOTpCotizacion PobjCotizacion = new conCOTpCotizacion();

        private conDTOtDepartamento PobjDepartamento = new conDTOtDepartamento();

        // GET: conClsCotizacion
        //vista para crear una cotizacion
        public ActionResult conFrmRealizarCotizacionVista()
        {
            conCOTpCotizacion GobjCotizacion = new conCOTpCotizacion();
            GobjCotizacion.USUid_usuario = SessionHelper.GetUser();
            ViewBag.lstDepartamentos = mtdCargarDepartamentos();
            return View(GobjCotizacion);
        }
        //vista ver cotizacion con respuestas
        public ActionResult conFrmVerCotizacion(int id) //xGintIdCotizacion
        {
            return View();
        }
        //vista para listar las cotizaciones pendientes
        public ActionResult conFrmCotizacionesVista()
        {
            return View();
        }

        //vista para responder una cotizacion
        public ActionResult conFrmResponderCotizacionVista(int id) //xGintIdCotizacion
        {
            ViewBag.lstDepartamentos = mtdCargarDepartamentos();
            conCTRpCotizacionRespuesta LobjCotizacionRespuesta = new conCTRpCotizacionRespuesta();
            //asigamos el ID cotizacion al objeto  cotizacion respuesta
            LobjCotizacionRespuesta.COTid_cotizacion = id;
            //Obtenemos el objeto cotizacion por su ID
            ViewBag.GobjCotizacion = PobjCotizacion.mtdObtener(id);
            //devolvemos la vista con el objeto CTR creado
            return View(LobjCotizacionRespuesta);
        }

        //Guardar Respuesta Cotizacion
        public ActionResult mtdGuardarRespuestaCotizacion(conCTRpCotizacionRespuesta PobjCotizacionRespuestaModelo)
        {
            if (ModelState.IsValid)
            {                
                PobjCotizacionRespuestaModelo.mtdGuardarCotizacionRespuesta();               
            }
            else
            {
                ViewBag.lstDepartamentos = mtdCargarDepartamentos();
                ViewBag.GobjCotizacion = (PobjCotizacion.mtdObtener(PobjCotizacionRespuestaModelo.COTid_cotizacion));
                return View("conFrmResponderCotizacionVista", PobjCotizacionRespuestaModelo);
            }
            return Redirect("~/conClsCotizacion/Home");

        }

        //Guardar una cotizacion
        public ActionResult mtdGuardarCotizacion(conCOTpCotizacion PobjCotizacionModelo)
        {

            if (ModelState.IsValid)
            {
                //agregar la fecha de publicacion
                PobjCotizacionModelo.COTfecha_publicacion = DateTime.Now;

                if(PobjCotizacionModelo.COTfecha_limiteEntrega < PobjCotizacionModelo.COTfecha_publicacion)
                {
                    ViewBag.lstDepartamentos = mtdCargarDepartamentos();

                    ModelState.AddModelError("COTfecha_limiteEntrega", "La fecha de entrega no puede ser menor a la actual");
                    return View("conFrmRealizarCotizacionVista", PobjCotizacionModelo);
                }
                else
                {
                    PobjCotizacionModelo.mtdGuardar();
                }
            }
            else
            {              

                return View("conFrmRealizarCotizacionVista", PobjCotizacionModelo);
            }
            return Redirect("~/conClsCotizacion/conFrmRealizarCotizacionVista");
        }

        public object mtdCargarDepartamentos()
        {
            return PobjDepartamento.mtdListar();
        }

    }
}