# Construction Standards Institute (CSI) Crosswalk
These libraries provide strongly typed calls to easily communicate with CSI's Crosswalk API platform.
The following languages are current included and documented below.
- C#
- NodeJS

# C# Library

## Requirements
- .NetStandard 2.0 Compatible Project
- API Key

## Setup
- Use NuGet to add CSI Crosswalk to your solution.
- Create a new instance of CSICrosswalk.Client.V1.CSICrosswalkProvider with your API Key.\
var provider = new CSICrosswalkProvider("API Key")
- Make sure your API key is stored and accessed securely within your environment.

## GraphQL Examples

## List Classifications for Standard "Master Format"
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

## Use the Raw GraphQL JSON Yourself
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

## List Children of a Classification
CSIClassification[] parentClassification = await provider.ClassificationByStandard("MasterFormat", "08 00 00");\
foreach (CSIClassification child in parentClassification.Children)\
&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(child.Title);

# NodeJS Library
(under construction)

