using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System.Linq.Expressions;

internal class Program
{
    private static void Main(string[] args)
    {
        NewClass1 newClass = new NewClass1();
        newClass.Geeeeet();

        Console.WriteLine("Hello, World!");
        Console.ReadLine();
    }
    public class NewClass1
    {
        public async void Geeeeet()
        {
            AmazonS3Client s3Client;
            s3Client = new AmazonS3Client(RegionEndpoint.APSouth1);

            var response = await s3Client.SelectObjectContentAsync(new SelectObjectContentRequest()
            {
                Bucket = "awstacktempalte2",
                Key = "samplefile.json",
                ExpressionType = Amazon.S3.ExpressionType.SQL,
                Expression = "select s.Quantity,s.Instrument from S3Object[*].marketData[*] s",
                InputSerialization = new InputSerialization()
                {
                    JSON = new JSONInput()
                    {
                        JsonType = JsonType.Document
                    }
                },
                OutputSerialization = new OutputSerialization()
                {
                    JSON = new JSONOutput()
                }
            });
            foreach (var ev in response.Payload)
            {
                Console.WriteLine("Received {ev.GetType().Name}!");
                if (ev is RecordsEvent records)
                {
                    Console.WriteLine("The contents of the Records Event is...");
                    using (var reader = new StreamReader(records.Payload, System.Text.Encoding.UTF8))
                    {
                        Console.Write(reader.ReadToEnd());
                    }
                }
            }
        }
    }
}
