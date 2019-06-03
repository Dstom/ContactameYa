using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ContactameYa.Models;

namespace ContactameYa.Controllers
{
    public class conClsServicioController : Controller
    {
        private conSERpServicio PobjServicio = new conSERpServicio();
        private conCATtCategoria PobjCategoriaServicio = new conCATtCategoria();

        private conDTOtDepartamento PobjDepartamento = new conDTOtDepartamento();
        private conPRVtProvincia PobjProvincia = new conPRVtProvincia();
        private conDSTtDistrito PobjDistrito = new conDSTtDistrito();
        private conUSUpUsuario PobjUsuario = new conUSUpUsuario();


        // GET: conClsServicio
        public ActionResult conFrmServicioVista(int id = 0)
        {
            //Listas
            ViewBag.lstCategoriaServicios = mtdListarCategoria();
            ViewBag.lstServicios = mtdCargarServicios();
            //Departamento, Provincia, Ciudad
            ViewBag.lstDepartamentos = mtdCargarDepartamentos();

            return View(
                id == 0 ? new conSERpServicio() :
                PobjServicio.mtdObtener(id)
                );
        }

        //REALIZAR PEDIDO DE UN SERVICIO
        public ActionResult conFrmPedidoVista(int id = 0) //xGintIdServicio
        {

            conPDSpPedidoServicio LobjPedidoServicio = new conPDSpPedidoServicio();
            if( id > 0 )
            {
                ViewBag.GobjServicio =  PobjServicio.mtdObtener(id);
            }
            LobjPedidoServicio.SERid_servicio = id;


            return View(LobjPedidoServicio);
        }
        public ActionResult mtdGuardar(conSERpServicio PobjServicioModelo)
        {
            if (ModelState.IsValid)
            {
                PobjServicioModelo.mtdGuardar();
            }
            else
            {
                ViewBag.lstCategoriaServicios = mtdListarCategoria();
                ViewBag.lstServicios = mtdCargarServicios();
                ViewBag.lstDepartamentos = mtdCargarDepartamentos();

                return View("conFrmServicioVista", PobjServicioModelo);
            }
            return Redirect("~/conClsServicio/conFrmServicioVista");
        }
        public object mtdListarCategoria()
        {
            return PobjCategoriaServicio.Listar();
        }

        public object mtdCargarServicios()
        {
            return PobjServicio.mtdListar();
        }

        public object mtdCargarDepartamentos()
        {
            return PobjDepartamento.mtdListar();
        }

        public ActionResult mtdFiltrarLocalizacion(int xGintIdDepartamento, int xGintIdProvincia, int xGintIdDistrito)
        {
            var idDepartamento = xGintIdDepartamento;
            var idProvincia = xGintIdProvincia;
            var idDistrito = xGintIdDistrito;

            //Listas
            ViewBag.lstCategoriaServicios = mtdListarCategoria();
            ViewBag.lstServicios = mtdCargarServicios();
            //Departamento, Provincia, Ciudad
            ViewBag.lstDepartamentos = mtdCargarDepartamentos();
            //obtenermos longitud y latidu del distrito
            //var LobjDistrito= PobjServicio.mtdObtenerLatitudLongitud(xGintIdDepartamento, xGintIdProvincia, xGintIdDistrito);
            var LobjDistrito = PobjServicio.mtdObtenerLatitudLongitud(idDepartamento, idProvincia, idDistrito);
            var LobjMarkers = PobjUsuario.mtdListarProveedores();
            
            //Viewbag para los maracadores
            ViewBag.GobjMarkers = LobjMarkers;
            ViewBag.GdecLatitud = LobjDistrito.DSTlatitud.ToString().Replace(",",".");
            ViewBag.GdecLongitud= LobjDistrito.DSTlongitud.ToString().Replace(",", ".");

            return View();
        }

        public JsonResult mtdListarProvincias(int id_departamento)
        {
            if(id_departamento > 0)
            {
                var lstProvincias = PobjProvincia.mtdListar(id_departamento).Select(a => new {
                    PRVid_provincia = a.PRVid_provincia,
                    PRVnombre = a.PRVnombre
                });
                return Json(lstProvincias, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error");
            }
           

            
        }
        public JsonResult mtdListarDistritos(int id_provincia)
        {
            if (id_provincia > 0)
            {
                var lstDistritos = PobjDistrito.mtdListar(id_provincia).Select(a => new {
                    DSTid_distrito = a.DSTid_distrito,
                    DSTnombre = a.DSTnombre
                });

                return Json(lstDistritos, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error");
            }
           
        }
    }
}