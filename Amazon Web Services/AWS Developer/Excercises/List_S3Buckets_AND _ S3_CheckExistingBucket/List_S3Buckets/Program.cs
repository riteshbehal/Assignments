using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using static Program;

internal class Program
{
    private static void Main(string[] args)
    {
        AClient aClient = new AClient();
        aClient.IsBucketExists();
        aClient.GetBuckets();
        Console.WriteLine("Hello, World!");
        Console.ReadLine();
    }

    public class AClient
    {
        public void GetBuckets()
        {
            // Create a client
            AmazonS3Client client = new AmazonS3Client();

            // Issue call
            ListBucketsResponse response = client.ListBucketsAsync().Result;

            // View response data
            Console.WriteLine("Buckets owner - {0}", response.Owner.DisplayName);
            foreach (S3Bucket bucket in response.Buckets)
            {
                Console.WriteLine("Bucket {0}, Created on {1}", bucket.BucketName, bucket.CreationDate);
            }
        }

        public async void IsBucketExists()
        {
            AmazonS3Client client = new AmazonS3Client();
            bool exists = false;
            // Check if a bucket already exists in AWS
            exists = AmazonS3Util.DoesS3BucketExistV2Async(client, "awskeywordbucket1637556").Result;
            if (exists)
            {
                // DoesS3BucketExistV2Async returns true if the bucket exists, but that does not
                // necessarily mean it belongs to your account as the method catches AccessDenied
                // and other exceptions. 
                Console.WriteLine("This bucket already exists in your, or someone else's, account.");
            }
            else
            { 
                Console.WriteLine("The bucket does not exist."); 
            }
        }
    }
}