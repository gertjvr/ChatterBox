﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using ChatterBox.Core.Infrastructure;
using ChatterBox.Core.Infrastructure.Entities;
using ChatterBox.Core.Infrastructure.Facts;
using Newtonsoft.Json;
using ThirdDrawer.Extensions.CollectionExtensionMethods;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace ChatterBox.Core.Persistence.Disk
{
    public class DiskFactStore : IFactStore
    {
        private readonly string _factStoreDirectoryPath;
        private readonly ITypesProvider _typesProvider;
        private readonly DirectoryInfo _factDirectoryBase;
        private readonly JsonSerializerSettings _jsonSerializeSettings;

        private const string _filenameSuffix = ".json";

        public DiskFactStore(string factStoreDirectoryPath, ITypesProvider typesProvider)
        {
            if (factStoreDirectoryPath == null) 
                throw new ArgumentNullException("factStoreDirectoryPath");

            if (typesProvider == null) 
                throw new ArgumentNullException("typesProvider");

            _factStoreDirectoryPath = factStoreDirectoryPath;
            _typesProvider = typesProvider;

            _factDirectoryBase = CreateFactDirectoryBase();

            _jsonSerializeSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Full
            };
        }

        public void AppendAtomically(IFact[] facts)
        {
            if (facts == null) 
                throw new ArgumentNullException("facts");

            if (facts.None()) return;

            var factsAndFilenames = facts
                .Select(f => new Tuple<IFact, string>(f, ConstructFullyQualifiedFileName(f)))
                .ToArray();

            try
            {
                factsAndFilenames
                    .Do(kvp => WriteFactToDisk(kvp.Item2, kvp.Item1))
                    .Done();
            }
            catch (Exception)
            {
                factsAndFilenames
                    .Do(kvp =>
                    {
                        try
                        {
                            File.Delete(kvp.Item2);
                        }
                        // ReSharper disable EmptyGeneralCatchClause
                        catch (Exception)
                        // ReSharper restore EmptyGeneralCatchClause
                        {
                        }
                    })
                    .Done();
                throw;
            }
        }

        public IEnumerable<FactAbout<T>> GetStream<T>(Guid id) where T : IAggregateRoot
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "id");

            return GetStream(id, typeof(T)).Cast<FactAbout<T>>();
        }

        private IEnumerable<IFact> GetStream(Guid id, Type aggregateType)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "id");
            
            if (aggregateType == null) 
                throw new ArgumentNullException("aggregateType");

            return LoadFactsFrom(GetFactDirectoryFor(aggregateType.Name, id)) //FIXME hack - use StreamName property
                .OrderBy(f => f.UnitOfWorkProperties.FactTimestamp)
                .ThenBy(f => f.UnitOfWorkProperties.UnitOfWorkId)
                .ThenBy(f => f.UnitOfWorkProperties.SequenceNumber);
        }

        public IEnumerable<Guid> GetAllStreamIds<T>() where T : IAggregateRoot
        {
            return GetAllStreamIds(typeof(T));
        }

        private IEnumerable<Guid> GetAllStreamIds(Type aggregateType)
        {
            if (aggregateType == null) 
                throw new ArgumentNullException("aggregateType");

            var baseDirectory = GetFactDirectoryFor(aggregateType.Name);
            foreach (var streamDirectory in baseDirectory.GetDirectories())
            {
                Guid result;
                if (Guid.TryParse(streamDirectory.Name, out result)) yield return result;
            }
        }

        public IEnumerable<IGrouping<Guid, IFact>> GetAllFactsGroupedByUnitOfWork()
        {
            var allFacts = from t in _typesProvider.AggregateTypes
                           from streamId in GetAllStreamIds(t)
                           from fact in GetStream(streamId, t)
                           select fact;

            return allFacts.GroupBy(f => f.UnitOfWorkProperties.UnitOfWorkId);
        }

        public void ImportFrom(IEnumerable<IFact> facts)
        {
            if (facts == null) 
                throw new ArgumentNullException("facts");

            AppendAtomically(facts.ToArray()); //FIXME this will run out of memory once we have lots of facts.
        }

        public bool HasFacts
        {
            get { return GetAllFactsGroupedByUnitOfWork().Any(); }
        }

        private DirectoryInfo CreateFactDirectoryBase()
        {
            var baseDirectory = new DirectoryInfo(_factStoreDirectoryPath);
            if (!baseDirectory.Exists) baseDirectory.Create();
            return baseDirectory;
        }

        private void WriteFactToDisk(string fullyQualifiedFilename, IFact fact)
        {
            if (fullyQualifiedFilename == null) 
                throw new ArgumentNullException("fullyQualifiedFilename");
            
            if (fact == null) 
                throw new ArgumentNullException("fact");

            using (var stream = File.OpenWrite(fullyQualifiedFilename))
            {
                using (var writer = new StreamWriter(stream))
                {
                    var serialize = JsonConvert.SerializeObject(fact, _jsonSerializeSettings);
                    writer.Write(serialize);
                }
            }
        }

        private IFact ReadFactFromDisk(FileInfo fileInfo)
        {
            if (fileInfo == null) 
                throw new ArgumentNullException("fileInfo");

            using (var stream = fileInfo.OpenRead())
            {
                using (var reader = new StreamReader(stream))
                {
                    var serialze = reader.ReadToEnd();
                    var instance = JsonConvert.DeserializeObject(serialze, _jsonSerializeSettings);
                    return (IFact)instance;
                }
            }
        }

        private IEnumerable<IFact> LoadFactsFrom(DirectoryInfo directory)
        {
            if (directory == null) 
                throw new ArgumentNullException("directory");

            return directory
                .GetFiles("*" + _filenameSuffix)
                .AsParallel()
                .Select(ReadFactFromDisk);
        }

        private string ConstructFullyQualifiedFileName(IFact fact)
        {
            if (fact == null) 
                throw new ArgumentNullException("fact");

            var directory = GetFactDirectoryFor(fact.StreamName, fact.AggregateRootId);
            var filename = ConstructFilenameFor(fact);
            var fullyQualifiedFileName = directory.FullName + "\\" + filename;
            return fullyQualifiedFileName;
        }

        private static string ConstructFilenameFor(IFact fact)
        {
            if (fact == null) 
                throw new ArgumentNullException("fact");

            var filename = "{0}.{1}.{2}.{3}{4}".FormatWith(fact.UnitOfWorkProperties.FactTimestamp.Ticks,
                                                           fact.UnitOfWorkProperties.UnitOfWorkId,
                                                           fact.UnitOfWorkProperties.SequenceNumber,
                                                           fact.GetType().Name,
                                                           _filenameSuffix);
            return filename;
        }

        private DirectoryInfo GetFactDirectoryFor(string streamName)
        {
            if (streamName == null) 
                throw new ArgumentNullException("streamName");

            var path = Path.Combine(_factDirectoryBase.FullName, streamName);
            var directory = new DirectoryInfo(path);
            if (!directory.Exists) directory.Create();
            return directory;
        }

        private DirectoryInfo GetFactDirectoryFor(string streamName, Guid id)
        {
            if (streamName == null) 
                throw new ArgumentNullException("streamName");

            if (id == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty.", "id");

            var path = Path.Combine(GetFactDirectoryFor(streamName).FullName, id.ToString());
            var directory = new DirectoryInfo(path);
            if (!directory.Exists) directory.Create();
            return directory;
        }
    }
}