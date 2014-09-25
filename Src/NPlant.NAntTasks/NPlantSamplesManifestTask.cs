using System.IO;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using NPlant.Generation;

namespace NPlant.NAntTasks
{
    [TaskName("nplant-samples-manifest-gen")]
    public class NPlantSamplesManifestTask : Task
    {
        [TaskAttribute("assembly", Required = false)]
        public string SamplesAssembly { get; set; }

        [TaskAttribute("src", Required = true)]
        public string SourceDirectory { get; set; }

        [TaskAttribute("out", Required = true)]
        public string OutputPath { get; set; }

        protected override void ExecuteTask()
        {
            const string baseName = "NPlant.Samples";
            string baseDir = Path.Combine(SourceDirectory, baseName);

            var buffer = new StringBuilder();
            buffer.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            buffer.AppendLine("<samples>");

            string samplesAssemblyPath = this.SamplesAssembly.IsNullOrEmpty() ? "NPlant.Samples.dll" : this.SamplesAssembly;

            var loader = new NPlantAssemblyLoader();
            var samplesAssembly = loader.Load(samplesAssemblyPath);
            var sampleTypes = samplesAssembly.GetExportedTypes();

            foreach (var sampleType in sampleTypes)
            {
                if (typeof(ClassDiagram).IsAssignableFrom(sampleType))
                {
                    var sample = sampleType.GetAttributeOf<SampleAttribute>();

                    string id = sampleType.FullName;

                    string sourcePath = "{0}\\{1}".FormatWith(baseDir, "{0}.cs".FormatWith(id.Substring(baseName.Length + 1).Replace(".", "\\")));

                    if (File.Exists(sourcePath))
                    {
                        string name = sampleType.Name;
                        string description = name;
                        int start = baseName.Length + 1;
                        string group = id.Substring(start, (id.Length - start) - (name.Length + 1));

                        if (sample != null)
                        {
                            name = sample.Name.IsNullOrEmpty() ? name : sample.Name;
                            description = sample.Description.IsNullOrEmpty() ? description : sample.Description;
                        }

                        buffer.AppendLine("  <sample id=\"{0}\" name=\"{1}\" description=\"{2}\" group=\"{3}\">".FormatWith(id, name, description, group));
                        buffer.AppendLine("  <![CDATA[{0}".FormatWith(File.ReadAllText(sourcePath, Encoding.UTF8)));
                        buffer.AppendLine("  ]]>");
                        buffer.AppendLine("  </sample>");
                    }
                }
            }

            buffer.AppendLine("</samples>");

            File.WriteAllText(this.OutputPath, buffer.ToString());
            
        }
    }
}