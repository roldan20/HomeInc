using System.ComponentModel.DataAnnotations.Schema;

namespace HomeInc.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreate { get; set; }
        public string Category { get; set; }
        public string TypeOfGuarantee { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }


    }
}
