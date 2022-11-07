using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using DAL.Model;
using DAL.Persistence;
using iText.Html2pdf;
using iText.StyledXmlParser.Css.Media;
using TelaCadastro.Models;
using TelaCadastro.Util;
using TelaCadastro.ViewModels;

namespace TelaCadastro.Controllers
{
    public class RelatorioController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }


        public ActionResult TabelaCidade()
        {
            var listacidade = new CidadeDal().ObterTodos().ToList();

            return View(listacidade);
        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        public FileContentResult Imprimir(string registros)
        {
            string html = "";

            if (registros == "TabelaAluno")
                html = RenderRazorViewToString("TabelaAluno", new AlunoDal().ObterTodos().ToList());
            if (registros == "TabelaCidade")
                html = RenderRazorViewToString("TabelaCidade", new CidadeDal().ObterTodos().ToList());
            if (registros == "TabelaAlunoCidade")
                html = RenderRazorViewToString("TabelaAlunoCidade", new CidadeDal().ObterTodos().ToList());

            var properties = new ConverterProperties();
            var baseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            properties.SetBaseUri(baseUrl);

            MediaDeviceDescription med = new MediaDeviceDescription(MediaType.ALL);
            med.SetOrientation("landscape");
            properties.SetMediaDeviceDescription(med);

            using (var stream = new MemoryStream())
            {
                HtmlConverter.ConvertToPdf(html, stream, properties);

                return File(stream.ToArray(), "application/pdf");
            }
        }
    }
}