namespace DLHApi.Common.Models
{
    public class DlhErrorModel
    {
        public string? Message { get; set; }

        public int Status { get; set; }

        public IEnumerable<DlhValidationError>? ValidationErrors { get; set; }
    }
}
