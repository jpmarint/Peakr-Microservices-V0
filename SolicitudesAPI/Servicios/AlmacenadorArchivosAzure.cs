using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace SolicitudesAPI.Servicios
{
    public class AlmacenadorArchivosAzure : IAlmacenadorArchivos
    {
        private readonly string connectionString;

        public AlmacenadorArchivosAzure(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("AzureStorage");
        }

        public async Task BorrarArchivo(string ruta, string contenedor)
        {
            if (string.IsNullOrEmpty(ruta))
            {
                return;
            }

            var cliente = new BlobContainerClient(connectionString, contenedor);
            await cliente.CreateIfNotExistsAsync();
            var archivo = Path.GetFileName(ruta);
            var blob = cliente.GetBlobClient(archivo);
            await blob.DeleteIfExistsAsync();

        }
     

        public async Task<string> EditarArchivo(byte[] contenido, string extension,
            string contenedor, string ruta, string contentType, string companyName, string fileName)
        {
            await BorrarArchivo(ruta, contenedor);
            return await GuardarArchivo(contenido, extension, contenedor, contentType, companyName, fileName);
        }
    

        //Enviando imagen hacia Azure
        public async Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor,
            string contentType, string companyName, string fileName)
        {
            var cliente = new BlobContainerClient(connectionString, contenedor);
            await cliente.CreateIfNotExistsAsync();
            cliente.SetAccessPolicy(PublicAccessType.Blob);

            var archivoNombre = $"{companyName}/{fileName}{extension}";
            var blob = cliente.GetBlobClient(archivoNombre);
            var blobUploadOptions = new BlobUploadOptions();
            var blobHttpHeader = new BlobHttpHeaders();
            blobHttpHeader.ContentType = contentType;
            blobUploadOptions.HttpHeaders = blobHttpHeader;

            await blob.UploadAsync(new BinaryData(contenido), blobUploadOptions);
            return blob.Uri.ToString();
        }

    }
}
