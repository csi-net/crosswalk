"use strict";

console.log("");

let stdin = process.openStdin();
stdin.addListener("data", function (text) {
    process.stdin.destroy();
});

const Version3Samples = require("./Version3Samples");

Version3Samples();