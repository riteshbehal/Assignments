using Amazon;
using Amazon.Kinesis;
using Amazon.Kinesis.Model;
using Kinesis_ReceiveData;
using Newtonsoft.Json;
using System.Net;
using System.Text;

var kinesisClient = new AmazonKinesisClient(RegionEndpoint.APSouth1);

var describeRequest = new DescribeStreamRequest()
{
    StreamName = "demostream"
};

var describeResponse = await kinesisClient.DescribeStreamAsync(describeRequest);
List<Shard> shards = describeResponse.StreamDescription.Shards;

foreach (var shard in shards)
{
    var iteratorRequest = new GetShardIteratorRequest()
    {
        StreamName = "demostream",
        ShardId = shard.ShardId,
        ShardIteratorType = ShardIteratorType.TRIM_HORIZON
    };

    var iteratorResponse = await kinesisClient.GetShardIteratorAsync(iteratorRequest);
    string shardIterator = iteratorResponse.ShardIterator;

    while (shardIterator != null)
    {
        var getRecords = new GetRecordsRequest()
        {
            Limit = 10,
            ShardIterator = shardIterator
        };

        var getData = await kinesisClient.GetRecordsAsync(getRecords);
        var records = getData.Records;

        if (records.Count > 0)
        {
            foreach (var record in records)
            {
                string data=Encoding.UTF8.GetString((record.Data.ToArray()));
                Logdata logData = JsonConvert.DeserializeObject<Logdata>(data);
                Console.Write(logData.logId);
                Console.WriteLine(logData.logDetails);
            }
        }
        shardIterator = getData.NextShardIterator;
    }

    }