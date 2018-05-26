using System.IO;
using System.Reflection;
using System.Threading;
using moe.futa.akizuki.Core;
using moe.futa.akizuki.Core.Extensions;
using NLog;

namespace Akizuki.Bootloader
{
    internal class Bootloader
    {
        private static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            logger.Info("Akizuki bootstrap started");

            var configPath = Path.ChangeExtension(Assembly.GetEntryAssembly().Location, "xml");
            logger.Info($"Loading XML {configPath}");
            var config = Configuration.LoadFile(configPath);

            var extRepo = new ExtensionRepository();
            logger.Info($"Loading extensions from {config.Extensions.SearchPath}");
            extRepo.LoadDirectory(config.Extensions.SearchPath);

            // TODO: remove infinate sleep
            while (true)
                Thread.Sleep(0);
        }
    }
}