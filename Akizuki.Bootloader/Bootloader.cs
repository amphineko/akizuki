using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Xml.Serialization;
using Akizuki.Core;
using Akizuki.Core.Extensions;
using Akizuki.Core.Routing;
using NLog;

namespace Akizuki.Bootloader
{
    internal class Bootloader
    {
        private static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            logger.Info("Akizuki bootstrap started");

            var classRepo = new ClassRepository();
            logger.Info($"Loading extensions");
            classRepo.LoadDirectory(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                                                 throw new InvalidOperationException(), "Extensions"));

            var configPath = Path.ChangeExtension(Assembly.GetEntryAssembly().Location, "xml");
            logger.Info($"Loading XML {configPath}");
            var config = LoadConfigurationFile(configPath, classRepo.GetConfigurationTypes());

            var inRouter = new InboundRouter();
            var outRouter = new OutboundRouter();

            foreach (var instance in config.Extensions.Install(classRepo, inRouter, outRouter))
                instance.SetEnabled();

            logger.Info("Akizuki bootstrap completed");
            Thread.Sleep(Timeout.Infinite);
        }

        public static Configuration LoadConfigurationFile(string path, List<Type> extraTypes)
        {
            var serializer = new XmlSerializer(typeof(Configuration), extraTypes.ToArray());
            using (var stream = new FileStream(path, FileMode.Open))
            {
                return (Configuration) serializer.Deserialize(stream);
            }
        }
    }
}