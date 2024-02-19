using DeliveryApp.Domain.Entity;
using Flunt.Validations;

namespace DeliveryApp.Domain.Validation
{
	internal class OrderValidator : Contract<Order>
	{
		public OrderValidator(Order order)
		{
			Requires()
				.IsGreaterOrEqualsThan(order.UserId, 1, nameof(order.UserId), "usuario não informado")
				.IsGreaterOrEqualsThan(order.RacePrice, 1, nameof(order.RacePrice), "Preço da corrida é obrigatorio");
		}
	}
}
