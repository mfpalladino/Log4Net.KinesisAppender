#  Log4Net.KinesisAppender

**How to use it?**

- 1-install-package Log4Net.KinesisAppender

- 2-Create your Kinesis stream

- 3-Add Kinesis appender in "log4net.config" file

```
<?xml version="1.0" encoding="utf-8" ?>
<log4net>

  <appender name="KinesisAppender" type="Log4Net.KinesisAppender.KinesisAppender, Log4Net.KinesisAppender">
    <StreamName>nameOfYourKinesisStream</StreamName>
    <Region>yourRegion (ex: us-east-1)</Region>
    <AccessKey>yourAccessKey</AccessKey>
    <SecretKey>yourSecretKey</SecretKey>
    <AppName>nameOfYourApp</AppName>
    <Filters>
    </Filters>
  </appender>
  <root>
    <level value="ERROR" />
    <appender-ref ref="KinesisAppender" />
  </root>
</log4net>
```