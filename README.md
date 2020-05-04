# Construction Standards Institute (CSI) Crosswalk
These libraries provide strongly typed calls to easily communicate with CSI's Crosswalk API platform.
The following languages are current included and documented below.
- C# (Install-Package CSICrosswalk.Client -Version 1.0.0-CI-20200429-234512)
- NodeJS (npm-add 'csicrosswalk.client')

# C# Library

## Requirements
- .NetStandard 2.0 Compatible Project
- API Key

## Setup
- Use NuGet to add CSI Crosswalk to your solution.\
Install-Package CSICrosswalk.Client -Version 1.0.0-CI-20200429-234512
- Create a new instance of CSICrosswalk.Client.V1.CSICrosswalkProvider with your API Key.\
var provider = new CSICrosswalkProvider("API Key")
- Make sure your API key is stored and accessed securely within your environment.

## GraphQL Examples

### List Classifications for Standard "Master Format"
string query = @"{\
&nbsp;&nbsp;&nbsp;&nbsp;standards(id: ""MasterFormat"" version: ""2018"") {\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;name\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;classifications {\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;id\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;versionid\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;publishdate\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;title\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;number\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}\
&nbsp;&nbsp;&nbsp;&nbsp;}\
}";\
\
CSIStandard[] standards = await provider.QueryToModels(query);\
foreach (CSIClassification classification in standards[0].Classifications)\
&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(classification.Title);

### Use the Raw GraphQL JSON Yourself
string json = await provider.QueryToJson(query);

## REST Examples

### List All Standards
CSIStandard[] standards = await provider.Standards();\
foreach (CSIStandard standard in standards)\
&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(standard.Name)

### List Other Versions of Standard "Master Format"
CSIStandard[] masterFormatWithVersions = await provider.Standard("MasterFormat");\
foreach (CSIStandard otherVersion in masterFormatWithVersions.OtherVersions)\
&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine("otherVersion.PublishDate.Value.Year");

### List Tables of Standard "Omni Class"
CSIStandard onmiClassWithTables = await provider.Standard("OmniClass");\
foreach (CSITable table in onmiClassWithTables.Tables)\
&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(table.Number);

### List Classifications of Standard "Master Format"
CSIClassification[] masterFormatClassifications = await provider.ClassificationsByStandard("MasterFormat");\
foreach (CSIClassification classification in masterFormatClassifications)\
&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(classification.Number);

### List Classifications of a Table in Standard "Omni Class"
CSIClassification[] onmiClassClassifications = await provider.ClassificationsByTable("OmniClass", "22");\
foreach (CSIClassification classification in onmiClassClassifications)\
&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(classification.Title);

### List Children of a Classification
CSIClassification[] parentClassification = await provider.ClassificationByStandard("MasterFormat", "08 00 00");\
foreach (CSIClassification child in parentClassification.Children)\
&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(child.Title);

# NodeJS Library

## Requirements
- NodeJS Project
- API Key

## Setup
- Use NPM to add CSI Crosswalk to your solution.\
npm-add 'csicrosswalk.client'\
- Create a new instance of CSICrosswalkProvider with your API Key.\
const CSICrosswalkProvider = require("csicrosswalk.client/V1/CSICrosswalkProvider");\
let provider = new CSICrosswalkProvider(AuthorizationHeaders.v1);
- Make sure your API key is stored and accessed securely within your environment.

## GraphQL Examples

### List Classifications for Standard "Master Format"
let query = '{\
&nbsp;&nbsp;&nbsp;&nbsp;standards(id: "MasterFormat" version: "2018") {\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;name\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;classifications {\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;id\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;versionid\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;publishdate\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;title\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;number\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}\
&nbsp;&nbsp;&nbsp;&nbsp;}\
};'\
\
provider.queryToModels(query, (models) => {\
&nbsp;&nbsp;&nbsp;&nbsp;let model = models[0];\
&nbsp;&nbsp;&nbsp;&nbsp;for (let i = 0; i < model.classifications.length; i++) {\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;console.log("---" + model.classifications[i].number + ": " + model.classifications[i].title);\
&nbsp;&nbsp;&nbsp;&nbsp;}\
});

## REST Examples

### List All Standards
provider.standards((models) => {\
&nbsp;&nbsp;&nbsp;&nbsp;for (let i = 0; i < models.length; i++) {\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;let model = models[i];\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;console.log(model.name);\
&nbsp;&nbsp;&nbsp;&nbsp;}\
});

### List Other Versions of Standard "Master Format"
provider.standard("MasterFormat", (model) => {\
&nbsp;&nbsp;&nbsp;&nbsp;for (let i = 0; i < model.otherversions.length; i++) {\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;console.log(model.otherversions[i].publishdate.getFullYear());\
&nbsp;&nbsp;&nbsp;&nbsp;}\
});

### List Tables of Standard "Omni Class"
provider.standard("OmniClass", (model) => {\
&nbsp;&nbsp;&nbsp;&nbsp;for (let i = 0; i < model.tables.length; i++) {\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;console.log(model.tables[i].name);\
&nbsp;&nbsp;&nbsp;&nbsp;}\
});

### List Classifications of Standard "Master Format"
provider.classificationsByStandard("MasterFormat", (models) => {\
&nbsp;&nbsp;&nbsp;&nbsp;for (let i = 0; i < models.length; i++) {\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;console.log(models[i].title);\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}\
});

### List Classifications of a Table in Standard "Omni Class"
provider.classificationsByTable("OmniClass", 22, (models) => {\
&nbsp;&nbsp;&nbsp;&nbsp;for (let i = 0; i < models.length; i++) {\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;console.log(models[i].title);\
&nbsp;&nbsp;&nbsp;&nbsp;}\
});

### List Children of a Classification
provider.classificationByStandard("MasterFormat", "08 00 00", (model) => {\
&nbsp;&nbsp;&nbsp;&nbsp;for (let i = 0; i < model.children.length; i++) {\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;console.log(model.children[i].title);\
&nbsp;&nbsp;&nbsp;&nbsp;}\
});


