namespace Lab09_MuñozHerrera.Application.DTOs
{
    public class OrderWithDetailsDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string ClientName { get; set; }
        public List<OrderProductDetailDto> Details { get; set; }
    }
}