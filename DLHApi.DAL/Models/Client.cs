namespace DLHApi.DAL.Models;

public partial class Client
{
    public long ClientId { get; set; }

    public int Mvid { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public DateTime? Dob { get; set; }

    public int? ProtectedFlag { get; set; }

    public virtual Licence Mv { get; set; } = null!;
}
