using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactameYa.Filters;
using ContactameYa.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;

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

        private conFOPpForoPreguntas PobjForoPreguntas = new conFOPpForoPreguntas();

        // GET: conClsServicio
        public ActionResult conFrmServicioVista(int id = 0)
        {
            //Listas
            ViewBag.lstCategoriaServicios = mtdListarCategoria();
            //Departamento, Provincia, Ciudad
            ViewBag.lstDepartamentos = mtdCargarDepartamentos();

            conSERpServicio GobjServicio = new conSERpServicio();
            GobjServicio.USUid_usuario = SessionHelper.GetUser();

            return View(
                id == 0 ? GobjServicio :
                PobjServicio.mtdObtener(id)
                );
        }

        //General
        public ActionResult conFrmListarServiciosVista()
        {
            ViewBag.lstDepartamentos = mtdCargarDepartamentos();

            return View(PobjServicio.mtdListar());
        }

        public ActionResult conFrmMisServiciosVista()
        {                 
            //Departamento, Provincia, Ciudad
            ViewBag.lstDepartamentos = mtdCargarDepartamentos();
            //devolvemos los servicios del usuario logeado
            return View(PobjServicio.mtdListarMisServicios(SessionHelper.GetUser()));
        }

        public ActionResult conFrmVerServicioVista(int id = 0)
        {
            ViewBag.lstDepartamentos = mtdCargarDepartamentos();
            ViewBag.lstPreguntas = PobjForoPreguntas.mtdListarPreguntasServicio(id);
            ViewBag.idUsuario = SessionHelper.GetUser();
            return View(PobjServicio.mtdObtener(id));
        }
        public ActionResult mtdPreguntar(conFOPpForoPreguntas xPobjForoPreguntas)
        {
            xPobjForoPreguntas.mtdGuardar();
            return Redirect("conFrmVerServicioVista/" + xPobjForoPreguntas.SERid_servicio);
        }
        public ActionResult mtdResponder(conFOPpForoPreguntas xPobjForoPreguntas)
        {
            string respuesta = xPobjForoPreguntas.FOPrespuesta;
            DateTime fecha_respuesta = DateTime.Now;
            xPobjForoPreguntas = PobjForoPreguntas.mtdObtener(xPobjForoPreguntas.FOPid_foroPregunta);
            xPobjForoPreguntas.FOPrespuesta = respuesta;
            xPobjForoPreguntas.FOPfecha_respuesta = fecha_respuesta;
            xPobjForoPreguntas.mtdGuardar();
            return Redirect("conFrmVerServicioVista/" + xPobjForoPreguntas.SERid_servicio);
        }
        public ActionResult mtdGuardar(conSERpServicio PobjServicioModelo, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if(!string.IsNullOrEmpty(PobjServicioModelo.SERimagenes))
                {
                    if(file != null)
                    {
                        //string fullPath = Server.MapPath("~/Uploads/" + PobjServicioModelo.SERimagenes);
                        //if (System.IO.File.Exists(fullPath))
                        //{
                        //    System.IO.File.Delete(fullPath);
                        //}
                        PobjServicioModelo.SERimagenes = mtdSubirImagen(file);
                    }
                }
                else
                {
                    if(file != null)
                    {
                        PobjServicioModelo.SERimagenes = mtdSubirImagen(file);
                    }

                }
                PobjServicioModelo.mtdGuardar();
            }
            else
            {
                ViewBag.lstCategoriaServicios = mtdListarCategoria();
                ViewBag.lstServicios = mtdCargarServicios();
                ViewBag.lstDepartamentos = mtdCargarDepartamentos();

                return View("conFrmServicioVista", PobjServicioModelo);
            }
            return Redirect("~/conClsServicio/conFrmMisServiciosVista");
        }

        public string mtdSubirImagen(HttpPostedFileBase file)
        {
            //string fileName = Path.GetFileName(file.FileName);
            //string fileLocationString = "~/Uploads/";
            //string fileLocation = Path.Combine(Server.MapPath(fileLocationString), fileName);
            string path = Server.MapPath("~/Uploads/");
            string fileLocation = path + file.FileName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            file.SaveAs(fileLocation);
            //BlobHandler bh = new BlobHandler("contactameyara");
            //bh.UploadSingle(file);

            //var account = new CloudStorageAccount(new StorageCredentials("jhcondori@upt.pe", "QevfVJjwEogD0HbvW3qFw+U9H1b7+tomu8nj4ezXvHMLfk5Fydgm4kcowg5H/8F4o2RI0xF43CdPFq/tl/fpdA=="), true);
            //var cloudBlobClient = account.CreateCloudBlobClient();
            //var container = cloudBlobClient.GetContainerReference("contactameyara");
            //var blob = container.GetBlockBlobReference(file.FileName);
            //blob.UploadFromStream(file.InputStream);
            //var blobUrl = blob.Uri.AbsoluteUri;
            return file.FileName;           
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

        public ActionResult conFrmFiltrarLocalizacionVista(int xGintIdDepartamento, int xGintIdProvincia, int xGintIdDistrito)
        {
            //Listas
            ViewBag.lstCategoriaServicios = mtdListarCategoria();
            ViewBag.lstServicios = mtdCargarServicios();
            //Departamento, Provincia, Ciudad
            ViewBag.lstDepartamentos = mtdCargarDepartamentos();
            //obtenermos longitud y latidu del distrito
            var LobjDistrito = PobjDistrito.mtdObtenerLatitudLongitud(xGintIdDepartamento, xGintIdProvincia, xGintIdDistrito);
            var LobjMarkers = PobjUsuario.mtdListarProveedores();
            
            //Viewbag para los maracadores
            ViewBag.GobjMarkers = LobjMarkers;
            ViewBag.GdecLatitud = LobjDistrito.DSTlatitud.ToString().Replace(",",".");
            ViewBag.GdecLongitud= LobjDistrito.DSTlongitud.ToString().Replace(",", ".");

            return View();
        }

        public JsonResult mtdActualizarEstado(int id_servicio, string estado)
        {
            var LobjServicio = PobjServicio.mtdObtener(id_servicio);
            var nombre_servicio = LobjServicio.SERnombre_servicio;

            LobjServicio.SERestado = estado;
            LobjServicio.mtdGuardar();
            return Json(nombre_servicio, JsonRequestBehavior.AllowGet);
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