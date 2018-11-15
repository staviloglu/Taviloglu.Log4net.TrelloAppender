# Taviloglu.log4net.TrelloAppender
Custom appender for log4net which adds logs as new card in provided Trello list.

# Sample Configuration
```xml
<log4net>
  <appender name="TrelloAppender" type="Taviloglu.log4net.TrelloAppender, Taviloglu.log4net.TrelloAppender" >
    <threshold value="Error" />
    <!-- Keys Required For Trello Cards API https://developers.trello.com/v1.0/reference#cards-2 -->
    <Token value="your-token"/>
    <Key value="your-api-key"/>    
    <ListId value="ListId"/>
    <Position value="top"/>
    <Members value="memberId01,memberId02"/>    
    <!-------------------------------------->
    <layout type="log4net.Layout.PatternLayout">
      <conversionPattern value="%-5level %date [%-5.5thread] %-40.40logger - %message%newline" />
    </layout>
  </appender>
  <root>
    <appender-ref ref="TrelloAppender" />
    <level value="ALL"></level>
  </root>
</log4net>
```
