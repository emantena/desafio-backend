﻿using System.Diagnostics.CodeAnalysis;

namespace DeliveryApp.IoC.Helpers
{
	[ExcludeFromCodeCoverage]
	public static class EnvironmentHelper
	{
		public static string GetEnvironment()
		{
			return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
		}
	}
}
