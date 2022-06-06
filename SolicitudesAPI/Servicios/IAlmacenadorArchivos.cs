namespace SolicitudesAPI.Servicios
{
    public interface IAlmacenadorArchivos
    {
        Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor,
           string contentType, string companyName, string fileName);
        Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor,
            string ruta, string contentType, string companyName, string fileName);
        Task BorrarArchivo(string ruta, string contenedor);

    }
}
