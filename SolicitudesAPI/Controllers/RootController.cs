using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SolicitudesAPI.DTOs;

namespace SolicitudesAPI.Controllers
{
  
    [ApiController]
    [Route("api")]
    public class RootController : ControllerBase
    {

        /// <summary>
        /// Get Routes API
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>

        [HttpGet(Name = "ObtenerRoot")]
        public ActionResult<IEnumerable<DatoHATEOAS>> Get()
        {
            var datoHateoas = new List<DatoHATEOAS>();
            datoHateoas.Add(new DatoHATEOAS(enlace: Url.Link("ObtenerRoot", new { })
                , descripcion: "self", metodo: "GET"));

            datoHateoas.Add(new DatoHATEOAS(enlace: Url.Link("RegisterCompany", new { })
               , descripcion: "RegisterCompany", metodo: "POST"));

            datoHateoas.Add(new DatoHATEOAS(enlace: Url.Link("Request&QuoteReadSellerModal", new { })
               , descripcion: "QuoteDetails", metodo: "GET"));

            datoHateoas.Add(new DatoHATEOAS(enlace: Url.Link("QuoteCreationModal", new { })
               , descripcion: "NewQuote", metodo: "POST"));

            datoHateoas.Add(new DatoHATEOAS(enlace: Url.Link("GetRequestCreationQuote", new { })
               , descripcion: "requestNewQuote", metodo: "GET"));

            datoHateoas.Add(new DatoHATEOAS(enlace: Url.Link("NewRequest", new { })
                , descripcion: "NewRequest", metodo: "POST"));

            datoHateoas.Add(new DatoHATEOAS(enlace: Url.Link("RequestDetailasSeller", new { })
                , descripcion: "RequestDetail", metodo: "GET"));

            datoHateoas.Add(new DatoHATEOAS(enlace: Url.Link("RequestAsSeller/Pending", new { })
                , descripcion: "Pending", metodo: "GET"));

            datoHateoas.Add(new DatoHATEOAS(enlace: Url.Link("RequestAsSeller/Sold", new { })
                , descripcion: "Sold", metodo: "GET"));

            return datoHateoas;
        }
    }
}
