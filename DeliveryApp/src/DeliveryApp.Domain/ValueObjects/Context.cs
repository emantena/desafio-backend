﻿namespace DeliveryApp.Domain.ValueObjects
{
	public static class Context
	{
		public static int UserId { get; set; }
		public static Guid CorrelationId { get; set; }
	}
}
