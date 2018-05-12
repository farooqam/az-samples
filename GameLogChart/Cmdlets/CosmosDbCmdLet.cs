using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;

namespace Cmdlets
{
    public class CosmosDbCmdLet : Cmdlet
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
