using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactameYa.Filters;
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
            ViewBag.lstDepartamentos = mtdCargarDepartamentos();

            return View(PobjCotizacion.mtdObtenerCotizacion(id));
        }
        //vista para listar MIS cotizaciones
        public ActionResult conFrmMisCotizacionesVista()
        {
            ViewBag.lstDepartamentos = mtdCargarDepartamentos();
            return View(PobjCotizacion.mtdListarMisCotizaciones(SessionHelper.GetUser()));
        }

        //vista para ver todas las cotizaciones pendientes

        public ActionResult conFrmCotizacionesVista()
        {
            ViewBag.lstDepartamentos = mtdCargarDepartamentos();

            return View(PobjCotizacion.mtdListarCotizaciones());
        }

        //vista para responder una cotizacion
        public ActionResult conFrmResponderCotizacionVista(int id) //xGintIdCotizacion
        {
            ViewBag.lstDepartamentos = mtdCargarDepartamentos();
            conCTRpCotizacionRespuesta LobjCotizacionRespuesta = new conCTRpCotizacionRespuesta();
            //asigamos el ID cotizacion y ID usuario
            LobjCotizacionRespuesta.COTid_cotizacion = id;
            LobjCotizacionRespuesta.USUid_usuario = SessionHelper.GetUser();

            ViewBag.GobjCotizacion = PobjCotizacion.mtdObtenerCotizacion(id);
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
                ViewBag.GobjCotizacion = (PobjCotizacion.mtdObtenerCotizacion(PobjCotizacionRespuestaModelo.COTid_cotizacion));
                return View("conFrmResponderCotizacionVista", PobjCotizacionRespuestaModelo);
            }
            return Redirect("~/conClsCotizacion/conFrmCotizacionesVista");

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