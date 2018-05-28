using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.Kinesis;
using Amazon.Kinesis.Model;
using Amazon.Runtime;
using Newtonsoft.Json;

namespace Log4Net.KinesisAppender.Kinesis
{
    public class KinesisClient : IKinesisClient
    {
        private readonly AmazonKinesisClient _client;

        public string Region { get; }
        public string StreamName { get; }
        public string AccessKey { get; }
        public string SecretKey { get; }
        public string AppName { get; }

        public KinesisClient(string region, string streamName, string accessKey, string secretKey, string appName)
        {
            Region = region;
            StreamName = streamName;
            AccessKey = accessKey;
            SecretKey = secretKey;
            AppName = appName;

            _client = new AmazonKinesisClient(new BasicAWSCredentials(AccessKey, SecretKey), RegionEndpoint.GetBySystemName(Region));
        }

        public void Send(Dictionary<string, object> logEvent)
        {
            var requestRecord = new PutRecordRequest
            {
                StreamName = StreamName,
                Data = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(logEvent))),
                PartitionKey = AppName
            };

            Task.Run(() => _client.PutRecordAsync(requestRecord));
        }
    }
}