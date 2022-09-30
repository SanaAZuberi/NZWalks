namespace NZWalks.API.Models.DTOs
{
    public class UpdateRegionRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }
        public double Lattitude { get; set; }
        public double Longitude { get; set; }
        public long Population { get; set; }

    }
}
