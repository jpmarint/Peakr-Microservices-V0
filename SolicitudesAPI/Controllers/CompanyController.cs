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
        public async Task<IActionResult> RegisterCompany([FromForm] CompanyCreationDTO companyCreationDTO)
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

            if (companyCreationDTO.LegalExistenceDoc != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await companyCreationDTO.LegalExistenceDoc.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(companyCreationDTO.LegalExistenceDoc.FileName);
                    var companyName = companyCreationDTO.Name;
                    company.LegalExistenceDocPath = await almacenadorArchivos.GuardarArchivoCompany(companyName, LegalExistenceFileName, companyCreationDTO.LegalExistenceDoc);

                }
            }

            if (companyCreationDTO.BankAccountDoc != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await companyCreationDTO.BankAccountDoc.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var companyName = companyCreationDTO.Name;
                    var extension = Path.GetExtension(companyCreationDTO.BankAccountDoc.FileName);
                    company.BankAccountDocPath = await almacenadorArchivos
                        .GuardarArchivoCompany(companyName, BankAccountFileName, companyCreationDTO.BankAccountDoc);

                }
            }

            if (companyCreationDTO.RutDoc != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await companyCreationDTO.RutDoc.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var companyName = companyCreationDTO.Name;
                    var extension = Path.GetExtension(companyCreationDTO.RutDoc.FileName);
                    company.RutDocPath = await almacenadorArchivos
                        .GuardarArchivoCompany(companyName, RutFileName, companyCreationDTO.RutDoc);

                }
            }
            context.Add(company);
            await context.SaveChangesAsync();
            var dto = mapper.Map<CompanyDTO>(company);
            return Ok(company);

        }

        [HttpGet("Details")]
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


        //[HttpGet("GetLogo")]
        //public async Task<IActionResult> GetLogo(int companyId)
        //{

        //    var exist = await context.Companies.AnyAsync(x => x.CompanyId == companyId);
        //    if (!exist)
        //    {
        //        return NotFound("No existe esta compañía");
        //    }

        //    var companyLogoPath = await context.Companies
        //        .Where(x => x.CompanyId == companyId)
        //        .Select(u => u.LogoPath)
        //        .FirstOrDefaultAsync();

        //    companyLogoPath = almacenadorArchivos.GenerateSASTokenForFile(companyLogoPath);

        //    return Ok(companyLogoPath);

        //}

        [HttpGet("Address")]
        public async Task<IActionResult> GetCompanyAddress(int companyId)
        {
            var exist = await context.Companies.AnyAsync(x => x.CompanyId == companyId);
            if (!exist)
            {
                return NotFound("No existe esta compañía");
            }

            var companyAddressId = await context.Companies
                .Where(x => x.CompanyId == companyId)
                .Select(u => u.AddressId)
                .FirstOrDefaultAsync();

            var address = await context.Address
                .Where(addressDB => addressDB.AddressId == companyAddressId).FirstOrDefaultAsync();
            return Ok(mapper.Map<AddressDTO>(address));
        }

        //[HttpGet("GetCategories")]
        //public async Task<ActionResult<CategoriesDTO>> GetCategories(int companyId)
        //{
        //    var noexiste = await context.Companies.AnyAsync(x => x.CompanyId == companyId);

        //    if (!noexiste)
        //    {
        //        return NotFound("No existe esa compañia");
        //    }

        //    var company = await context.Companies.Where(x => x.CompanyId == companyId).FirstOrDefaultAsync();

        //    context.Entry(company).Collection(c => c.companyCategories).Load();
        //    company.ProductCategories = _db.Categories.Select(x => new SelectListItem { Text = x.Name, Value = x.CategoryId.ToString() }).ToList();
        //    company.ProductCategories.ForEach(x => {
        //        if (company.Categories.Any(y => (y.CategoryId.ToString() == x.Value)))
        //        {
        //            x.Selected = true;
        //        }
        //    });

        //}


        [HttpGet("GetFiles")]
        public async Task<IActionResult> GetCompanyFiles(int companyId)
        {
            var exist = await context.Companies.AnyAsync(x => x.CompanyId == companyId);
            if (!exist)
            {
                return NotFound("No existe esta compañía");
            }

            var company = await context.Companies.FirstOrDefaultAsync(x => x.CompanyId == companyId);

            var companyDocs = mapper.Map<CompanyDocsDTO>(company);

            companyDocs.LogoPath = almacenadorArchivos.GenerateSASTokenForFile(companyDocs.LogoPath);
            companyDocs.ImagePath = almacenadorArchivos.GenerateSASTokenForFile(companyDocs.ImagePath);
            companyDocs.RutDocPath = almacenadorArchivos.GenerateSASTokenForFile(companyDocs.RutDocPath);
            companyDocs.BankAccountDocPath = almacenadorArchivos.GenerateSASTokenForFile(companyDocs.BankAccountDocPath);
            companyDocs.LegalExistenceDocPath = almacenadorArchivos.GenerateSASTokenForFile(companyDocs.LegalExistenceDocPath);
            //companyDocs.PeakrContractDocPath = almacenadorArchivos.GenerateSASTokenForFile(companyDocs.PeakrContractDocPath);

            //generar SASToken para cada archivo...

            return Ok(companyDocs);
        }

        [HttpPut("UpdateFile")]
        public async Task<IActionResult> UpdateCompanyFile([FromForm] int companyId,
            string fileToUpdate, IFormFile file)
        {

            var exist = await context.Companies.AnyAsync(x => x.CompanyId == companyId);
            if (!exist)
            {
                return NotFound("No existe esta compañía");
            }


            if (file != null)
            {
                string newPath = string.Empty;
                var companyRecord = await context.Companies
                        .Where(x => x.CompanyId == companyId).FirstOrDefaultAsync();

                string companyName = companyRecord.Name;

                fileToUpdate = fileToUpdate.Trim().Replace(" ", String.Empty).ToLowerInvariant();
                switch (fileToUpdate)
                {
                    case "logo":
                        string companyLogo = await context.Companies
                            .Where(x => x.CompanyId == companyId).Select(y => y.LogoPath).FirstOrDefaultAsync();
                        if (companyLogo.EndsWith("Temp.png") || companyLogo.StartsWith(@"https://peakrweb.blob.core.windows.net"))
                        {
                            newPath = await almacenadorArchivos.UploadFileToBlob(companyName, file);
                        }
                        else
                        {
                            await almacenadorArchivos.BorrarArchivo(companyLogo, contenedor, companyName);
                            newPath = await almacenadorArchivos.UploadFileToBlob(companyName, file);
                        }
                        companyRecord.LogoPath = newPath;
                        break;
                    case "banner":
                        string companyBanner = await context.Companies
                            .Where(x => x.CompanyId == companyId).Select(y => y.ImagePath).FirstOrDefaultAsync();
                        if (companyBanner.EndsWith("Temp.png") || companyBanner.StartsWith(@"https://peakrweb.blob.core.windows.net"))
                        {
                            newPath = await almacenadorArchivos.UploadFileToBlob(companyName, file);
                        }
                        else
                        {
                            await almacenadorArchivos.BorrarArchivo(companyBanner, contenedor, companyName);
                            newPath = await almacenadorArchivos.UploadFileToBlob(companyName, file);
                        }
                        companyRecord.ImagePath = newPath;
                        break;

                    case "rut":
                        string companyRut = await context.Companies
                        .Where(x => x.CompanyId == companyId).Select(y => y.RutDocPath).FirstOrDefaultAsync();
                        await almacenadorArchivos.BorrarArchivo(companyRut, contenedor, companyName);
                        newPath = await almacenadorArchivos.GuardarArchivoCompany(companyName, RutFileName, file);
                        companyRecord.RutDocPath = newPath;
                        break;

                    case "exist":
                        string companyExist = await context.Companies
                        .Where(x => x.CompanyId == companyId).Select(y => y.LegalExistenceDocPath).FirstOrDefaultAsync();

                        await almacenadorArchivos.BorrarArchivo(companyExist, contenedor, companyName);
                        newPath = await almacenadorArchivos.GuardarArchivoCompany(companyName, LegalExistenceFileName, file);

                        companyRecord.LegalExistenceDocPath = newPath;
                        break;
                    case "bank":
                        string companyBank = await context.Companies
                                                   .Where(x => x.CompanyId == companyId).Select(y => y.BankAccountDocPath).FirstOrDefaultAsync();

                        await almacenadorArchivos.BorrarArchivo(companyBank, contenedor, companyName);
                        newPath = await almacenadorArchivos.GuardarArchivoCompany(companyName, BankAccountFileName, file);
                        
                        companyRecord.BankAccountDocPath = newPath;
                        break;

                    default:

                        return BadRequest("El archivo que deseas cambiar no existe");

                }

                context.Update(companyRecord);
                await context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                return BadRequest("Debe subir un archivo");
            }

            return Ok();

        }


        [HttpPut("UpdateAddress")]
        public async Task<IActionResult> UpdateCompanyAddress(AddressDTO companyAddress, int companyId)
        {
            var exist = await context.Companies.AnyAsync(x => x.CompanyId == companyId);
            if (!exist)
            {
                return NotFound("No existe esta compañía");
            }

            var companyRecord = await context.Companies
                       .Where(x => x.CompanyId == companyId).FirstOrDefaultAsync();
            
            var address = new Address();

            address.Line1 = companyAddress.Line1;
            address.Line2 = companyAddress.Line2;
            address.PostalCode = companyAddress.PostalCode;
            address.Department = companyAddress.Department;
            address.City = companyAddress.City;
            address.Notes = companyAddress.Notes;

            context.Address.Add(address);
            context.SaveChanges();
            companyRecord.AddressId = address.AddressId;
            context.SaveChanges();


            return Ok("Los detalles de la compañía se actualizaron.");
        }

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
