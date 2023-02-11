namespace DLHApi.Common.Models
{
    public class DlhValidationError
    {
        public string Name { get; }

        public string Reason { get; }

        public DlhValidationError(string name, string reason)
        {
            Name = name;
            Reason = reason;
        }
    }
}
