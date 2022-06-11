using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolicitudesAPI.DTOs;
using SolicitudesAPI.DTOs.CompanyDTOs;
using SolicitudesAPI.Models;
using SolicitudesAPI.Servicios;

namespace SolicitudesAPI.Controllers
{
    [ApiController]
    [Route("Api/Companies")]
    public class CompanyController : ControllerBase
    {
        private readonly string RutFileName = "RUT";
        private readonly string LegalExistenceFileName = "Cert_Existencia";
        private readonly string BankAccountFileName = "Cert_Bancario";
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "documentos";
        public CompanyController(ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        /// <summary>
        /// Register Company
        /// </summary>
        /// <param name="companyCreationDTO"></param>
        /// <returns></returns>

        [HttpPost("Register", Name = "RegisterCompany")]
        public async Task<IActionResult> RegisterCompany(CompanyCreationDTO companyCreationDTO)
        {

            var ExisteCompanymismocorreo = await context.Companies.AnyAsync(x => x.Email == companyCreationDTO.Email);

            if (ExisteCompanymismocorreo)
            {
                return BadRequest($"Ya existe una compañia con el correo {companyCreationDTO.Email}, inicie sesión o registre una " +
                    $"empresa diferente, en caso de no estar registrado en Peakr, contactar con support@peakr.app.");
            }

            var ExisteCompanymismoNIT = await context.Companies.AnyAsync(x => x.NIT == companyCreationDTO.Nit);

            if (ExisteCompanymismoNIT)
            {
                return BadRequest($"Ya existe una compañia con el NIT {companyCreationDTO.Nit}, inicie sesión o registre una " +
                    $"empresa diferente, en caso de no estar registrado en Peakr, contactar con support@peakr.app.");
            }

            var ExisteCompanymismoNombre = await context.Companies.AnyAsync(x => x.Name == companyCreationDTO.Name);

            if (ExisteCompanymismoNombre)
            {
                return BadRequest($"Ya existe una compañia con el nombre {companyCreationDTO.Name}, inicie sesión o registre una " +
                    $"empresa diferente, en caso de no estar registrado en Peakr, contactar con support@peakr.app.");
            }

            var company = mapper.Map<Company>(companyCreationDTO);

            context.Add(company);
            await context.SaveChangesAsync();
            var dto = mapper.Map<CompanyDTO>(company);
            return Ok(company.CompanyId);

        }

        /// <summary>
        /// Details Company
        /// </summary>
        /// <param name="companyCreationDTO"></param>
        /// <returns></returns>

        [HttpGet("GetDetails")]
        public async Task<IActionResult> GetCompanyDetails(int companyId)
        {
            var exist = await context.Companies.AnyAsync(x => x.CompanyId == companyId);
            if (!exist)
            {
                return NotFound("No existe esta compañía");
            }

            var company = await context.Companies.FirstOrDefaultAsync(x => x.CompanyId == companyId);

            var companyDetails = mapper.Map<CompanyDetailsDTO>(company);

            return Ok(companyDetails);
        }

        /// <summary>
        /// Get Selected Categories
        /// </summary>
        /// <param name="companyCreationDTO"></param>
        /// <returns></returns>

        [HttpGet("GetSelectedCategories")]
        public async Task<IActionResult> GetSelectedCategories(int companyId)
        {
            var noexiste = await context.Companies.AnyAsync(x => x.CompanyId == companyId);

            if (!noexiste)
            {
                return NotFound("No existe esa compañia");
            }

            if (context.Companies.Where(x => x.CompanyId == companyId)
                .Select(x => x.CompanyType).FirstOrDefault())
            {
                var categories = await context.CompanyCategories.Where(x => x.CompanyId == companyId)
               .Select(y => y.CategoryId)
               .ToListAsync();

                var categoriesList = context.Categories.Where(y => categories.Contains(y.CategoryId)).ToList();

                var categoriesDTO = mapper.Map<List<CategoryDTO>>(categoriesList);
                categoriesDTO.ForEach(x => x.IsSelected = true);

                return Ok(categoriesDTO);
            }
            else
            {
                return Ok(new List<CategoryDTO>());
            }
    

        }
  
        /// <summary>
        /// Update Details Company
        /// </summary>
        /// <param name="CompanyDetailsDTO"></param>
        /// <returns></returns>

        [HttpPut("UpdateDetails")]
        public async Task<IActionResult> UpdateCompanyDetails(CompanyDetailsDTO companyDetails)
        {
            var exist = await context.Companies.AnyAsync(x => x.CompanyId == companyDetails.CompanyId);
            if (!exist)
            {
                return NotFound("No existe esta compañía");
            }

            var companyRecord = await context.Companies
                       .Where(x => x.CompanyId == companyDetails.CompanyId).FirstOrDefaultAsync();

            companyRecord.Name = companyDetails.Name;
            companyRecord.Description = companyDetails.Description;
            companyRecord.EstablishedSince = companyDetails.EstablishedSince;
            companyRecord.NIT = companyDetails.NIT;
            companyRecord.WebSiteUrl = companyDetails.WebSiteUrl;
            companyRecord.TotalEmployees = companyDetails.TotalEmployees;
            companyRecord.YearlySalesVolume = companyDetails.YearlySalesVolume;

            context.Update(companyRecord);
            await context.SaveChangesAsync();

            return Ok("Los detalles de la compañía se actualizaron.");
        }


    }
}
