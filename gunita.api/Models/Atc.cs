
namespace Gunita.Api.Models
{
    public class Atc
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ArrangementId { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
