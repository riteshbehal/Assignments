using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

internal class Program
{
    private static void Main(string[] args)
    {
        RiteshDynamodb riteshDynamodb = new RiteshDynamodb();
        riteshDynamodb.CreateCret();
        Console.WriteLine("Hello, World!");
    }


    public class RiteshDynamodb
    {
        public void CreateCret()
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            string tableName = "ProductCatalog3";

            var request = new CreateTableRequest
            {
                TableName = tableName,
                AttributeDefinitions = new List<AttributeDefinition>()
  {
    new AttributeDefinition
    {
      AttributeName = "Id",
      AttributeType = "N"
    }
  },
                KeySchema = new List<KeySchemaElement>()
  {
    new KeySchemaElement
    {
      AttributeName = "Id",
      KeyType = "HASH"  //Partition key
    }
  },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 10,
                    WriteCapacityUnits = 5
                }
            };

            var response = client.CreateTableAsync(request).Result;

        }
    }

}