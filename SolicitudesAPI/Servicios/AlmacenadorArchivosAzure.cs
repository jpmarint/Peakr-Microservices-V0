using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;

namespace SolicitudesAPI.Servicios
{
    public class AlmacenadorArchivosAzure : IAlmacenadorArchivos
    {
        private readonly string _connectionString;
        private readonly string documentos = "documentos";

        public AlmacenadorArchivosAzure(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AzureStorage");
        }

        public async Task BorrarArchivo(string ruta, string contenedor, string companyName)
        {

            var fileNamewExt = string.Empty;

            if (string.IsNullOrEmpty(ruta))
            {
                return;
            }

            try
            {
                companyName = companyName.Trim().Replace(" ", String.Empty).ToLowerInvariant();
                string connectionString = _connectionString;
                BlobContainerClient container = new BlobContainerClient(connectionString, documentos);
                fileNamewExt = string.Format($"{ruta}");
                BlobClient blobcli = container.GetBlobClient(fileNamewExt);  
                await blobcli.DeleteIfExistsAsync();

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }


        public async Task<string> EditarArchivo(byte[] contenido, string extension,
            string contenedor, string ruta, string contentType, string companyName, string fileName)
        {
            await BorrarArchivo(ruta,contenedor,companyName);
            return await GuardarArchivoCompany(contenido, extension, contenedor, contentType, companyName, fileName);
        }


        //Enviando archivos hacia Azure
        public async Task<string> GuardarArchivoCompany(byte[] contenido, string extension, string contenedor,
            string contentType, string companyName, string fileName)
        {
            companyName = companyName.Trim().Replace(" ", String.Empty).ToLowerInvariant();
            var cliente = new BlobContainerClient(_connectionString, contenedor);
            await cliente.CreateIfNotExistsAsync();
            cliente.SetAccessPolicy(PublicAccessType.Blob);
            var archivoNombre = $"{companyName}/{fileName}{extension}";
            var blob = cliente.GetBlobClient(archivoNombre);
            var blobUploadOptions = new BlobUploadOptions();
            var blobHttpHeader = new BlobHttpHeaders();
            blobHttpHeader.ContentType = contentType;
            blobUploadOptions.HttpHeaders = blobHttpHeader;
            await blob.UploadAsync(new BinaryData(contenido), blobUploadOptions);
            return (archivoNombre);
        }

        public async Task<string> UploadFileToBlob(string companyName, IFormFile file)
        {
            var fileNamewExt = String.Empty;
            var fileGuid = String.Empty;
            if (file == null)
                return fileNamewExt;
            try
            {
                companyName = companyName.Trim().Replace(" ", String.Empty).ToLowerInvariant();
                string connectionString = _connectionString;
                BlobContainerClient container = new BlobContainerClient(connectionString, documentos);
                container.CreateIfNotExists();
                fileGuid = $"{Guid.NewGuid()}{ Path.GetExtension(file.FileName)}";
                fileNamewExt = string.Format($"{companyName}/{fileGuid}");
                BlobClient blob = container.GetBlobClient(fileNamewExt);
                // Upload local file
                using (var stream = file.OpenReadStream())
                {
                    await blob.UploadAsync(stream, true);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return fileNamewExt;
        }

        public string GenerateSASTokenForFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return string.Empty;
            }

            if (fileName.EndsWith("Temp.png") || fileName.StartsWith(@"https://peakrweb.blob.core.windows.net"))
            {
                return fileName;
            }

            BlobClient blobClient = new BlobClient(_connectionString, documentos, fileName);
            if (blobClient.CanGenerateSasUri)
            {
                BlobSasBuilder sasBuilder = new BlobSasBuilder()
                {
                    BlobContainerName = blobClient.GetParentBlobContainerClient().Name,
                    BlobName = blobClient.Name,
                    Resource = "b",
                };

                sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(5);
                sasBuilder.SetPermissions(BlobSasPermissions.Read);
                Uri sasUri = blobClient.GenerateSasUri(sasBuilder);
                return sasUri.ToString();
            }

            return string.Empty;
        }


    }
}
