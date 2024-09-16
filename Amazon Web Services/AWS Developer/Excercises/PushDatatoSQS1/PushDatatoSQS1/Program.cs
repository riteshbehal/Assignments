using Amazon.SQS;
using Amazon.SQS.Model;

internal class Program
{
    private static void Main(string[] args)
    {
        string qUrl = "https://sqs.ap-south-1.amazonaws.com/463646775279/techqueue1";
        string messageBody = "Program1";
        AmazonSQSClient amazonSQSClient = new AmazonSQSClient();
        SendMessageResponse responseSendMsg = amazonSQSClient.SendMessageAsync(qUrl, messageBody).Result;
        Console.WriteLine($"Message added to queue\n  {qUrl}");
        Console.WriteLine($"HttpStatusCode: {responseSendMsg.HttpStatusCode}");

        Console.WriteLine("Hello, World!");
    }


}