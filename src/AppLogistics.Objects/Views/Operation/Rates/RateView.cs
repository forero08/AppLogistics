namespace AppLogistics.Objects
{
    public class RateView : BaseView
    {
        public string Name { get; set; }

        public string ClientName { get; set; }

        public string ActivityName { get; set; }

        public string VehicleTypeName { get; set; }

        public decimal Price { get; set; }

        public float EmployeePercentage { get; set; }

        public bool SplitFare { get; set; }
    }
}
