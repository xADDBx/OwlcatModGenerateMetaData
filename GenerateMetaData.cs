using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Newtonsoft.Json.Linq;

namespace GenerateMetaData {
    public class GenerateMetaData : Task {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Version { get; set; }

        [Required]
        public string OutputPath { get; set; }

        public string AssemblyName { get; set; }
        public string EntryMethod { get; set; }
        public string DisplayName { get; set; }
        public string Author { get; set; }
        public ITaskItem[] Requirements { get; set; }
        public ITaskItem[] LoadAfter { get; set; }
        public string GameVersion { get; set; }
        public string ManagerVersion { get; set; }
        public string HomePage { get; set; }
        public string Repository { get; set; }
        public string RepositoryOutputPath { get; set; }

        public override bool Execute()
        {
            if (!OutputPath.ToLower().EndsWith("info.json") && Directory.Exists(OutputPath))
            {
                OutputPath = Path.Combine(Path.GetFullPath(OutputPath), "Info.json");
            }
            else
            {
                Log.LogError("OutputPath is not 'Info.json' path or directory");
                return false;
            }

            var requirements = Requirements?.Select(ti => ti.ItemSpec)?.ToArray();
            var loadAfter = LoadAfter?.Select(ti => ti.ItemSpec)?.ToArray();
            JObject o = JObject.FromObject(new {
                Id,
                Version,
                AssemblyName,
                EntryMethod,
                DisplayName,
                Author,
                GameVersion,
                ManagerVersion,
                HomePage,
                Repository,
                Requirements = requirements,
                LoadAfter = loadAfter
            });
            Log.LogMessage(MessageImportance.High, $"Created Info.json with path: {OutputPath}");
            File.WriteAllText(OutputPath, o.ToString());
            if (Repository != null) {
                o = JObject.FromObject(new {
                    Releases = new[] {
                        new { Id, Version }
                    }
                });
                string outFile;
                if (RepositoryOutputPath != null) {
                    outFile = Path.Combine(RepositoryOutputPath, "Repository.json");
                } else {
                    outFile = Path.Combine(Directory.GetCurrentDirectory(), "Repository.json");
                }
                Log.LogMessage(MessageImportance.High, $"Created Repository.json: {outFile}");
                File.WriteAllText(outFile, o.ToString());
            }

            return true;
        }
    }
}