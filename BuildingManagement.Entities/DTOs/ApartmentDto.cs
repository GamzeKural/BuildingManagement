namespace BuildingManagement.Entities.DTOs
{
    public class ApartmentDto
    {
        public int Id { get; set; }
        public string Block { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int FloorLocation { get; set; }
        public int Number { get; set; }
        public int UserId { get; set; }
        public string? ApartmentInfo { get; set; }
    }
}
