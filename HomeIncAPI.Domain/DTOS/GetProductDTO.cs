

namespace HomeInc.Domain.DTOS
{
    public class GetProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string TypeOfGuarantee { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}
