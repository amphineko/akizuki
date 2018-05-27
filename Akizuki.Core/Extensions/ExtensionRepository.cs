using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using NLog;

namespace moe.futa.akizuki.Core.Extensions
{
    public sealed class ExtensionRepository
    {
        // NOTE: move RNG in MakeAppDomainName to members when converted to IDisposable

        // TODO: move extensions to AppDomain

        private readonly Dictionary<string, Type> _classes = new Dictionary<string, Type>();
        private readonly List<Type> _configTypes = new List<Type>();
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public Type GetExtensionType(string fullName)
        {
            return _classes[fullName];
        }

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
            var types = assembly.GetTypes()
                .Where(type => typeof(AbstractExtension<ExtensionConfiguration>).IsAssignableFrom(type));
            foreach (var clazz in types)
                LoadExtension(clazz);

            _configTypes.AddRange(assembly.GetTypes()
                .Where(type => typeof(ExtensionConfiguration).IsAssignableFrom(type)));
        }

        private void LoadExtension(Type type)
        {
            Debug.Assert(typeof(AbstractExtension<ExtensionConfiguration>).IsAssignableFrom(type));
            // register with class name
            _classes.Add(type.FullName ?? throw new InvalidOperationException("Unexpected null extension name"), type);
            _logger.Info($"Registered {type.FullName}");
        }

        public List<Type> GetConfigTypes()
        {
            return _configTypes;
        }
    }
}