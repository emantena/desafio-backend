namespace DeliveryApp.Domain.Validation
{
	public static class DocumentValidator
	{
		public static bool IsCnh(this string cnhNumber)
		{
			var isValid = false;

			if (cnhNumber.Length == 11 && cnhNumber != new string('1', 11))
			{
				var dsc = 0;
				var v = 0;

				for (int i = 0, j = 9; i < 9; i++, j--)
				{
					v += Convert.ToInt32(cnhNumber[i].ToString()) * j;
				}

				var vl1 = v % 11;
				if (vl1 >= 10)
				{
					vl1 = 0;
					dsc = 2;
				}

				v = 0;
				for (int i = 0, j = 1; i < 9; ++i, ++j)
				{
					v += Convert.ToInt32(cnhNumber[i].ToString()) * j;
				}

				var x = v % 11;
				var vl2 = (x >= 10) ? 0 : x - dsc;

				isValid = vl1.ToString() + vl2.ToString() == cnhNumber.Substring(cnhNumber.Length - 2, 2);
			}
			return isValid;
		}

		public static bool IsCnpj(this string cnpj)
		{
			var multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
			var multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
			int soma;
			int resto;
			string digito;
			string tempCnpj;
			cnpj = cnpj.Trim();
			cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

			if (cnpj.Length != 14)
			{
				return false;
			}

			tempCnpj = cnpj[..12];
			soma = 0;
			for (var i = 0; i < 12; i++)
			{
				soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
			}

			resto = soma % 11;

			resto = resto < 2 ? 0 : 11 - resto;

			digito = resto.ToString();
			tempCnpj += digito;
			soma = 0;

			for (var i = 0; i < 13; i++)
			{
				soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
			}

			resto = soma % 11;
			resto = resto < 2 ? 0 : 11 - resto;
			digito += resto.ToString();
			return cnpj.EndsWith(digito);
		}
	}
}