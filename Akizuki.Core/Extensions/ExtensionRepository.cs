using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using NLog;

namespace moe.futa.akizuki.Core.Extensions
{
    public class ExtensionRepository
    {
        // NOTE: move RNG in MakeAppDomainName to members when converted to IDisposable

        // TODO: move extensions to AppDomain

        private Dictionary<string, Type> _classes = new Dictionary<string, Type>();

        private readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public void LoadDirectory(string path)
        {
            foreach (var file in Directory.GetFiles(path))
                try
                {
                    LoadFile(file);
                }
                catch (BadImageFormatException)
                {
                    _logger.Debug($"Skipped non-assembly file {file}");
                }
        }

        private void LoadFile(string path)
        {
            path = Path.GetFullPath(path);
            var assembly = Assembly.LoadFile(path);
            _logger.Info($"Try loading assembly file {path}");

            // single assembly may have multiple extensions
            var types = assembly.GetTypes().Where((type) => typeof(AbstractExtension).IsAssignableFrom(type));
            foreach (var clazz in types)
                LoadExtension(clazz);
        }

        private void LoadExtension(Type type)
        {
            Debug.Assert(typeof(AbstractExtension).IsAssignableFrom(type));
            // register with class name
            _classes.Add(type.FullName ?? throw new InvalidOperationException("Unexpected null extension name"), type);
            _logger.Info($"Registered {type.FullName}");
        }
    }
}