namespace DLHAPI.Models
{
    public class DlhModel
    {
        public int Id { get; set; }
        public string MVID { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime DateOfExpire { get; set; }
        public byte[]? PDF { get; set; }
    }
}
