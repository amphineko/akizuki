using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using NLog;

namespace Akizuki.Core.Extensions
{
    public sealed class ClassRepository
    {
        // NOTE: move RNG in MakeAppDomainName to members when converted to IDisposable

        // TODO: move extensions to AppDomain

        private readonly Dictionary<string, Type> _classes = new Dictionary<string, Type>();
        private readonly List<Type> _configTypes = new List<Type>();
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public List<Type> GetConfigurationTypes()
        {
            return _configTypes;
        }

        public Type GetExtensionType(string fullName)
        {
            return _classes[fullName];
        }

        /// <summary>
        ///     Enumerates files in the directory and loads types which extend AbstractExtension
        /// </summary>
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
            _logger.Debug($"Try loading assembly file {path}");

            // single assembly may have multiple extensions

            foreach (var type in assembly.GetExportedTypes<AbstractExtension>())
                AddExtensionType(type);

            _configTypes.AddRange(assembly.GetExportedTypes<ExtensionConfiguration>());
        }

        private void AddExtensionType(Type type)
        {
            Debug.Assert(type.IsSubclassOf(typeof(AbstractExtension)));
            // register with class name
            _classes.Add(type.FullName ?? throw new InvalidOperationException("Unexpected null extension name"), type);
            _logger.Info($"Registered {type.FullName}");
        }
    }
}