using DeliveryApp.Domain.Entity;
using Flunt.Validations;

namespace DeliveryApp.Domain.Validation
{
	public class DeliveryManValidator : Contract<DeliveryMan>
	{
		public DeliveryManValidator(DeliveryMan deliveryMan)
		{
			Requires()
				.IsNotNullOrEmpty(deliveryMan.Name, nameof(deliveryMan.Name), "O nome é obrigatório")
				.IsLowerOrEqualsThan(deliveryMan.Name.Length, 75, nameof(deliveryMan.Name), "O nome deve ter no máximo 75 caracteres")
				.IsNotNullOrEmpty(deliveryMan.CNH, nameof(deliveryMan.CNH), "A CNH é obrigatória")
				.IsTrue(deliveryMan.CNH.IsCnh(), nameof(deliveryMan.CNH), "CNH inválida")
				.IsLowerOrEqualsThan(deliveryMan.CNH.Length, 11, nameof(deliveryMan.CNH), "A CNH deve ter no máximo 11 caracteres")
				.IsNotNullOrEmpty(deliveryMan.CNPJ, nameof(deliveryMan.CNPJ), "O CNPJ é obrigatório")
				.IsTrue(deliveryMan.CNPJ.IsCnpj(), nameof(deliveryMan.CNPJ), "CNPJ inválido")
				.IsLowerOrEqualsThan(deliveryMan.CNPJ.Length, 14, nameof(deliveryMan.CNPJ), "O CNPJ deve ter no máximo 14 caracteres")
				.IsTrue(deliveryMan.BirthDate != DateTime.MinValue, nameof(deliveryMan.BirthDate), "Data de nascimento inválida")
				.IsTrue(deliveryMan.BirthDate.AddYears(18) <= DateTime.Now, nameof(deliveryMan.BirthDate), $"Data de nascimento deve ser menor que {DateTime.Now.AddYears(-18).ToShortDateString()}");
		}
	}
}
