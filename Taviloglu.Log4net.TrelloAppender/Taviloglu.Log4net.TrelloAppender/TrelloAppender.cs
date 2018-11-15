using log4net.Appender;
using log4net.Core;
using System.Collections.Specialized;
using System.Net;

namespace Taviloglu.log4net
{
    public class TrelloAppender : AppenderSkeleton
    {
        protected override bool RequiresLayout => true;

        /// <summary>
        /// Card will be added to this list
        /// </summary>
        public string ListId { get; set; }
        /// <summary>
        /// Auth Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Api Key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Comma-separated list of member IDs to add to the card
        /// </summary>
        public string Members { get; set; }
        /// <summary>
        /// The position of the new card.top, bottom, or a positive float
        /// </summary>
        public string Position { get; set; }

        protected override void Append(LoggingEvent loggingEvent)
        {
            try
            {
                SendToTrello(loggingEvent);
            }
            catch (System.Exception ex)
            {
                ErrorHandler.Error("TrelloAppender error", ex);
            }
        }

        private void SendToTrello(LoggingEvent loggingEvent)
        {
            string name = $"[{loggingEvent.Level.DisplayName}] ";

            if (loggingEvent.ExceptionObject != null)
            {
                name += loggingEvent.ExceptionObject.Message;
            }
            else if (loggingEvent.MessageObject != null)
            {
                name += loggingEvent.MessageObject.ToString();
            }
            else
            {
                return;
            }

            var description = RenderLoggingEvent(loggingEvent);

            using (var webClient = new WebClient())
            {
                var values = new NameValueCollection(5);
                values.Add("name", name);
                values.Add("desc", description);
                values.Add("idList", ListId);
                values.Add("token", Token);
                values.Add("key", Key);

                if (!string.IsNullOrWhiteSpace(Members))
                {
                    values.Add("idMembers", Members);
                }

                if (!string.IsNullOrWhiteSpace(Position))
                {
                    values.Add("pos", Position);
                }

                webClient.UploadValues("https://api.trello.com/1/cards", values);
            }
        }
    }
}
