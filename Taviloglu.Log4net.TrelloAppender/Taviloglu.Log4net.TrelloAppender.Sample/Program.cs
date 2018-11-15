using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;

namespace Taviloglu.log4net.TrelloAppender.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository,new FileInfo("log4net.config"));
            var logger = LogManager.GetLogger(typeof(Program));
            

            var a = 0;
            var b = 100;

            try
            {
                var c = b / a;
            }
            catch (Exception ex)
            {
                logger.Error("Fatal Error", ex);
            }
        }
    }
}
