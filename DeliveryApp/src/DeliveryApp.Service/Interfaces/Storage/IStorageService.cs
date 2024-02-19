namespace DeliveryApp.Service.Interfaces.Storage
{
	public interface IStorageService
	{
		/// <summary>
		/// Retorna a url do arquivo que foi feito upload
		/// </summary>
		/// <param name="fileBytes"></param>
		/// <param name="fileName"></param>
		/// <returns></returns>
		Task<string> UploadFileAsync(byte[] fileBytes, string fileName);
	}
}
