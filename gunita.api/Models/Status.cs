
namespace Gunita.Api.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
