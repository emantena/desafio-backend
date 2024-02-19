namespace DeliveryApp.Service.ViewModels.Request
{
    public class CreateRentVehicleRequest
    {
        public int DeliveryManId { get; set; }
        public int PlanVersionId { get; set; }
        public DateTime LeaseEndDate { get; set; }
    }
}