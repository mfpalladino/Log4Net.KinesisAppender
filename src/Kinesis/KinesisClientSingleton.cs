namespace Log4Net.KinesisAppender.Kinesis
{
    public static class KinesisClientSingleton
    {
        private static KinesisClient _instance;

        private static readonly object SyncLock = new object();

        public static KinesisClient GetKinesisClient(string region, string streamName, string accessKey, string secretKey, string appName)
        {
            if (_instance == null)
            {
                lock (SyncLock)
                {
                    if (_instance == null)
                        _instance = new KinesisClient(region, streamName, accessKey, secretKey, appName);
                }
            }

            return _instance;
        }
    }
}