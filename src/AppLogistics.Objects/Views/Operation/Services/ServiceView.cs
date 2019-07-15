namespace AppLogistics.Objects
{
    public class ServiceView : BaseView
    {
        public string RateClientName { get; set; }

        public string RateActivityName { get; set; }

        public string RateVehicleTypeName { get; set; }

        public string RateProductName { get; set; }

        public int Quantity { get; set; }

        public string CarrierName { get; set; }

        public string VehicleNumber { get; set; }

        public string Location { get; set; }

        public string CustomsInformation { get; set; }

        public decimal FullPrice { get; set; }

        public decimal HoldingPrice { get; set; }

        public string Comments { get; set; }
    }
}
