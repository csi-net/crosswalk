using CSICrosswalk.Client.V3;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CSICrosswalk.Client.Samples
{
    public static class Version3Samples
    {
        //Version 3
        public static async Task Run()
        {
            var provider = new CSICrosswalkProvider(AuthorizationHeaders.AuthKey);
            Console.WriteLine($"Version 3 - ServiceUrl: {provider.ServiceUrl}");
            Console.WriteLine();

            //List all the standards
            Console.WriteLine("Standards:");
            var standards = await provider.Standards();
            foreach (var standard in standards)
                Console.WriteLine($"---{standard.Name}");
            Console.WriteLine();

            //Isolate examples
            var masterFormatStandard = standards.First(x => x.Name == "MasterFormat"); //Regular Standard
            var omniClassStandard = standards.First(x => x.Name == "OmniClass"); //Table-style standard

            //List other version options for MasterFormat
            Console.WriteLine($"\"{masterFormatStandard.Name}\" {masterFormatStandard.PublishDate.Value.Year} Other Versions:");
            var masterFormatWithVersions = await provider.Standard(masterFormatStandard.Name);
            foreach (var otherVersion in masterFormatWithVersions.OtherVersions)
                Console.WriteLine($"---{otherVersion.PublishDate.Value.Year}");
            Console.WriteLine();

            //List tables for OmniClass
            Console.WriteLine($"\"{omniClassStandard.Name}\" Tables:");
            var onmiClassWithTables = await provider.Standard(omniClassStandard.Name);
            foreach (var table in onmiClassWithTables.Tables)
                Console.WriteLine($"---{table.Number}: {table.Name}");
            Console.WriteLine();

            //List the root classifications for MasterFormat
            Console.WriteLine($"\"{masterFormatStandard.Name}\" Root Classifications:");
            var masterFormatClassifications = await provider.ClassificationsByStandard(masterFormatStandard.Name);
            foreach (var classification in masterFormatClassifications)
                Console.WriteLine($"---{classification.Number}: {classification.Title}");
            Console.WriteLine();

            //List the root classifications for OmniClass (table based)
            var selectedTable = onmiClassWithTables.Tables[0];
            Console.WriteLine($"\"{omniClassStandard.Name}\" Table \"{selectedTable.Number}\" Root Classifications:");
            var onmiClassClassifications = await provider.ClassificationsByTable(onmiClassWithTables.Name, selectedTable.Number);
            foreach (var classification in onmiClassClassifications)
                Console.WriteLine($"---{classification.Number}: {classification.Title}");
            Console.WriteLine();

            //List the children of a specific classification
            var selectedClassification = masterFormatClassifications[2];
            Console.WriteLine($"\"{masterFormatStandard.Name}\" \"{selectedClassification.Title}\" Child Classifications:");
            var parentClassification = await provider.ClassificationByStandard(masterFormatStandard.Name, selectedClassification.Number);
            foreach (var child in parentClassification.Children)
                Console.WriteLine($"---{child.Number}: {child.Title}");
            Console.WriteLine();

            //List other versions of a specific classification
            Console.WriteLine($"\"{masterFormatStandard.Name}\" \"{selectedClassification.Title}\" Other Versions:");
            var primaryClassification = await provider.ClassificationWithRelationsByStandard(masterFormatStandard.Name, selectedClassification.Number);
            foreach (var child in primaryClassification.OtherVersions)
            {
                Console.WriteLine($"---{child.Number}: {child.Title}");
                foreach(var childStandard in child.Standards)
                Console.WriteLine($"------{childStandard.Name}: {childStandard.Version}");
            }
            Console.WriteLine();

            //Query with GraphQL
            var query = @"
{
    standards(id: ""MasterFormat"" version: ""2020"") {
        name
        classifications {
            id
            versionid
            publishdate
            title
            number
        }
    }
}
";

            Console.WriteLine("GraphQL QueryToModels");
            var graphQLData = await provider.QueryToModels(query);
            var graphQLStandard = graphQLData.Single();
            Console.WriteLine($"GraphQL Result Standard: {graphQLStandard.Name}");
            Console.WriteLine($"GraphQL Result Classifications:");
            foreach (var classification in graphQLStandard.Classifications)
                Console.WriteLine($"---{classification.Number}: {classification.Title}");
            Console.WriteLine();
        }
    }
}
