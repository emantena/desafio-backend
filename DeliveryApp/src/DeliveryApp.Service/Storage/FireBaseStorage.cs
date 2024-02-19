using DeliveryApp.Service.Interfaces.Storage;
using Firebase.Storage;

namespace DeliveryApp.Service.Storage
{
	public class FireBaseStorage : IFireBaseStorage
	{
		public string FirebaseStorageBucket { get; }

		protected string BucketName { get; private set; }

		public FireBaseStorage(string firebaseStorageBucket)
		{
			FirebaseStorageBucket = firebaseStorageBucket;
		}

		public async Task<string> UploadFileAsync(byte[] fileBytes, string fileName)
		{
			var firebaseStorage = new FirebaseStorage(FirebaseStorageBucket);

			return await firebaseStorage.Child($"{BucketName}/{fileName}")
				.PutAsync(new MemoryStream(fileBytes));
		}

		public IFireBaseStorage AddBucket(string bucketName)
		{
			BucketName = bucketName;
			return this;
		}
	}
}
