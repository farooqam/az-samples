using System;
using System.Management.Automation;
using Microsoft.Azure.Documents.Client;

namespace Cmdlets
{
    public class CosmosDbCmdlet : Cmdlet
    {
        protected DocumentClient DocumentClient { get; private set; }

        [Parameter(
            Mandatory = true,
            Position = 0,
            HelpMessage = "The Cosmos DB URI.",
            ValueFromPipeline = false,
            ValueFromRemainingArguments = false)]
        public Uri Uri { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 1,
            HelpMessage = "The Cosmos DB master key.",
            ValueFromPipeline = false,
            ValueFromRemainingArguments = false)]

        public string Key { get; set; }

        protected override void BeginProcessing()
        {
            DocumentClient = new DocumentClient(Uri, Key);
        }
    }
}