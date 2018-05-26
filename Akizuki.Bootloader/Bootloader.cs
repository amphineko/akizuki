using System.IO;
using System.Reflection;
using System.Threading;
using System.Xml.Serialization;
using moe.futa.akizuki.Core;
using moe.futa.akizuki.Core.Extensions;
using moe.futa.akizuki.Core.Routing;
using NLog;

namespace moe.futa.akizuki.Bootloader
{
    internal class Bootloader
    {
        private static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            logger.Info("Akizuki bootstrap started");

            var configPath = Path.ChangeExtension(Assembly.GetEntryAssembly().Location, "xml");
            logger.Info($"Loading XML {configPath}");
            var config = LoadConfigurationFile(configPath);

            var extRepo = new ExtensionRepository();
            logger.Info($"Loading extensions from {config.ExtensionRepositoryConfiguration.SearchPath}");
            extRepo.LoadDirectory(config.ExtensionRepositoryConfiguration.SearchPath);

            var inRouter = new InboundRouter();
            var outRouter = new OutboundRouter();

            config.Extensions.Install(extRepo, inRouter, outRouter);

            // TODO: remove infinate sleep
            while (true)
                Thread.Sleep(0);
        }

        public static Configuration LoadConfigurationFile(string path)
        {
            var serializer = new XmlSerializer(typeof(Configuration));
            using (var stream = new FileStream(path, FileMode.Open))
            {
                return (Configuration) serializer.Deserialize(stream);
            }
        }
    }
}