"use strict";

console.log("");

let stdin = process.openStdin();
stdin.addListener("data", function (text) {
    process.stdin.destroy();
});

const Version1Samples = require("./Version1Samples");

Version1Samples();