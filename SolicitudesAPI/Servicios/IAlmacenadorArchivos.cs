namespace SolicitudesAPI.Servicios
{
    public interface IAlmacenadorArchivos
    {
        Task<string> GuardarArchivoCompany(string companyName, string fileName, IFormFile file);
        Task<string> EditarArchivo(string companyName, string fileName, IFormFile file);
        Task BorrarArchivo(string ruta, string contenedor, string companyName);
        Task<string> UploadFileToBlob(string companyName, IFormFile file);
        string GenerateSASTokenForFile(string fileName);
    }
}
