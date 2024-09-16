using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Amazon.SQS;
using Amazon.SQS.Model;

internal class Program
{
    private static void Main(string[] args)
    {
        string qUrl = "https://sqs.ap-south-1.amazonaws.com/463646775279/TeslaQueue1";
        string messageBody = "Data from Program 1";
        AmazonSQSClient sqsClient = new AmazonSQSClient();
        var responseSendMsg = sqsClient.SendMessageAsync(qUrl, messageBody).Result;
        Console.WriteLine($"Message added to queue\n  {qUrl}");
        Console.WriteLine($"HttpStatusCode: {responseSendMsg.HttpStatusCode}");

        Console.WriteLine("Hello, World!");
    }
}