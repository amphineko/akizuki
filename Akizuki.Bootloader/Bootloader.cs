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
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     Interlocked variable for discarding multiple shutdown events
        /// </summary>
        private static int _exited;

        private static void Main(string[] args)
        {
            Logger.Info("Akizuki bootstrap started");

            // load extensions from assembly files in ./Extensions/
            var classRepo = new ClassRepository();
            Logger.Info($"Loading extensions");
            classRepo.LoadDirectory(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                                                 throw new InvalidOperationException(), "Extensions"));

            // load and parse configuration XML document
            var configPath = Path.ChangeExtension(Assembly.GetEntryAssembly().Location, "xml");
            Logger.Info($"Loading XML {configPath}");
            var config = LoadConfigurationFile(configPath, classRepo.GetConfigurationTypes());

            // create routers
            var inRouter = new InboundRouter();
            var outRouter = new OutboundRouter();

            // create extension instances
            var instances = config.Extensions.Install(classRepo, inRouter, outRouter);
            foreach (var instance in instances)
                instance.SetEnabled();

            // register shutdown events
            // TODO: complete missing shutdown events
            AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) => Shutdown(instances);
            Console.CancelKeyPress += (sender, eventArgs) => Shutdown(instances);

            // sleep the bootloader thread when bootstrap jobs are done
            Logger.Info("Akizuki bootstrap completed");
            Thread.Sleep(Timeout.Infinite);
        }

        private static Configuration LoadConfigurationFile(string path, List<Type> extraTypes)
        {
            // extraTypes are used to parse Extensions' configuration classes
            var serializer = new XmlSerializer(typeof(Configuration), extraTypes.ToArray());
            using (var stream = new FileStream(path, FileMode.Open))
            {
                return (Configuration) serializer.Deserialize(stream);
            }
        }

        private static void Shutdown(IList<AbstractExtension> instances)
        {
            // multiple shutdown processes are not allowed
            if (Interlocked.CompareExchange(ref _exited, 1, 0) == 1)
            {
                Logger.Error("Triggered multiple shutdown events");
                return;
            }

            Logger.Info("Akizuki bootstrap stopping");

            // disable active extension instances
            foreach (var instance in instances)
                instance.SetDisabled();

            // disabled extensions may have resources to dispose
            foreach (var instance in instances)
                instance.Dispose();

            Logger.Info("Akizuki bootstrap stopped");
        }
    }
}