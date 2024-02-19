using DeliveryApp.Domain.Entity;
using Flunt.Validations;
using System.Text.RegularExpressions;

namespace DeliveryApp.Domain.Validation
{
	internal class VehicleValidator : Contract<Vehicle>
	{
		public VehicleValidator(Vehicle vehicle)
		{
			Requires()
				.IsLowerThan(vehicle.YearManufacture, DateTime.Now.Year, nameof(vehicle.YearManufacture), "O ano de fabricação não pode ser maior que o ano atual.")
				.IsGreaterThan(vehicle.YearManufacture, 2000, nameof(vehicle.YearManufacture), "O ano de fabricação não pode ser menor que o ano de 2000.")
				.IsNotNullOrEmpty(vehicle.Plate, nameof(vehicle.Plate), "A placa é obrigatória.")
				.IsTrue(ValidatePlateFormat(vehicle.Plate), nameof(vehicle.Plate), "A placa deve estar no formato do Mercosul ou do Brasil.");
		}

		private static bool ValidatePlateFormat(string plate)
		{
			var plateRegex = "^(?:[A-Z]{3}-\\d{4}|[A-Z]{3}\\d{1}[A-Z]{1}\\d{2})$";
			return Regex.IsMatch(plate, plateRegex);
		}
	}
}
