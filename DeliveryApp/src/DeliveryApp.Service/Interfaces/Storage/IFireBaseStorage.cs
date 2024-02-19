namespace DeliveryApp.Service.Interfaces.Storage
{
	public interface IFireBaseStorage : IStorageService
	{
		string FirebaseStorageBucket { get; }

		IFireBaseStorage AddBucket(string bucketName);
	}
}
