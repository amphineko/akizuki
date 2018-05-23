using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using moe.futa.akizuki.Core;
using moe.futa.akizuki.Core.Extensions;
using moe.futa.akizuki.Core.Logging;

namespace Akizuki.Bootloader
{
    class Bootloader
    {
        static void Main(string[] args)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
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
