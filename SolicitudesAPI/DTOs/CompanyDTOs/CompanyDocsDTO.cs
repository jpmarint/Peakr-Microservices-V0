namespace SolicitudesAPI.DTOs.CompanyDTOs
{
    public class CompanyDocsDTO
    {
        public int CompanyId { get; set; }
        public int CompanyType { get; set; }
        public string LegalExistenceDocPath { get; set; }
        public string BankAccountDocPath { get; set; }
        public string RutDocPath { get; set; }
        public string PeakrContractDocPath { get; set; }
        public string ImagePath { get; set; } 
        public string LogoPath { get; set; } 
    }
}