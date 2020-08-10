const CSICrosswalkProvider = require("csicrosswalk.client/V2_1/CSICrosswalkProvider");
const AuthorizationHeaders = require("./AuthorizationHeaders");

let provider = new CSICrosswalkProvider(AuthorizationHeaders.v2_1);

function standards() {
    //List all the standards
    provider.standards((models) => {
        console.log("Standards:");
        for (let i = 0; i < models.length; i++) {
            let model = models[i];
            console.log("---" + model.name);
        }
        console.log("");
        standardOtherVersions();
    });
}

function standardOtherVersions() {
    //List other version options for MasterFormat
    provider.standard("MasterFormat", (model) => {
        console.log("\"" + model.name + "\"" + " " + model.publishdate.getFullYear() + " Other Versions:");
        for (let i = 0; i < model.otherversions.length; i++) {
            console.log("---" + model.otherversions[i].publishdate.getFullYear());
        }
        console.log("");
        standardTables();
    });
}

function standardTables() {
    //List tables for OmniClass
    provider.standard("OmniClass", (model) => {
        console.log("\"" + model.name + "\"" + " " + model.publishdate.getFullYear() + " Tables:");
        for (let i = 0; i < model.tables.length; i++) {
            console.log("---" + model.tables[i].number + ": " + model.tables[i].name);
        }
        console.log("");
        classificationsByStandard();
    });
}

function classificationsByStandard() {
    //List the root classifications for MasterFormat
    provider.classificationsByStandard("MasterFormat", (models) => {
        console.log("\"MasterFormat\" Root Classifications:");
        for (let i = 0; i < models.length; i++) {
            console.log("---" + models[i].number + ": " + models[i].title);
        }
        console.log("");
        classificationsByTable();
    });
}

function classificationsByTable() {
    //List the root classifications for OmniClass (table based)
    provider.classificationsByTable("OmniClass", 22, (models) => {
        console.log("\"OmniClass\" Root Classifications:");
        for (let i = 0; i < models.length; i++) {
            console.log("---" + models[i].number + ": " + models[i].title);
        }
        console.log("");
        classificationByStandard();
    });
}

function classificationByStandard() {
    //List the children of a specific classification
    provider.classificationByStandard("MasterFormat", "08 00 00", (model) => {
        console.log("\"MasterFormat\" \"" + model.title + "\" Child Classifications:");
        for (let i = 0; i < model.children.length; i++) {
            console.log("---" + model.children[i].number + ": " + model.children[i].title);
        }
        console.log("");
        classificationWithRelationsByStandard();
    });
}

function classificationWithRelationsByStandard() {
    //List the other versions of a specific classification
    provider.classificationWithRelationsByStandard("MasterFormat", "46 46 00", (model) => {
        console.log("\"MasterFormat\" \"" + model.title + "\" Other Versions:");
        for (let i = 0; i < model.otherversions.length; i++) {
            console.log("---" + model.otherversions[i].number + ": " + model.otherversions[i].title);
            for (let j = 0; j < model.otherversions[i].standards.length; j++) {
                console.log("------" + model.otherversions[i].standards[j].name + " " + model.otherversions[i].standards[j].version);
            }
        }
        console.log("");
        graphQLQuery();
    });
}

function graphQLQuery() {
    //Query with GraphQL
    let query = `{
    standards(id: "MasterFormat" version: "2018") {
        name
        classifications {
            id
            versionid
            publishdate
            title
            number
        }
    }
}`;
    provider.queryToModels(query, (models) => {
        let model = models[0];
        console.log("GraphQL Result Standard: " + model.name);
        console.log("GraphQL Result Classifications:");
        for (let i = 0; i < model.classifications.length; i++) {
            console.log("---" + model.classifications[i].number + ": " + model.classifications[i].title);
        }
        console.log("");
    });
}

module.exports = standards;