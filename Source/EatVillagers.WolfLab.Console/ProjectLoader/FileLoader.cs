using System;
using System.IO;
using System.Xml.Linq;
using EatVillagers.WolfLab.Xml;
using Ether.Outcomes;
using WolfLab.Core.Configuration;

namespace EatVillagers.WolfLab.Console.ProjectLoader
{
    public class FileLoader
    {
        /// <summary>
        /// Reads the configuration file. If there's a failure, the Outcome will encapsulate
        /// error messages.
        /// </summary>
        public IOutcome<ProjectModel> Read(string path)
        {
            if (!File.Exists(path))
                return Outcomes.Failure<ProjectModel>().WithMessage("File does not exist: " + path);

            try
            {
                var config = DoRead(path);

                return Outcomes.Success(config);
            }
            catch(Exception ex)
            {
                return Outcomes.Failure<ProjectModel>()
                    .WithMessage("There was a problem reading the config file.")
                    .FromException(ex);
            }
        }

        /// <summary>
        /// Orchestrate the work of loading the file and reading it into a ConfigModel.
        /// </summary>
        private ProjectModel DoRead(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                var xml = XDocument.Load(stream);
                var parser = new ProjectParser(xml);

                return parser.Parse();
            }
        }
    }
}
