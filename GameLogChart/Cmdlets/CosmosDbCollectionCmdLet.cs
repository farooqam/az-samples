using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Cmdlets
{
    public class CosmosDbCollectionCmdLet : CosmosDbCmdLet
    {
        [Parameter(
            Mandatory = true,
            Position = 2,
            HelpMessage = "The collection name.",
            ValueFromPipeline = false,
            ValueFromRemainingArguments = false)]
        public string Name { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 3,
            HelpMessage = "The Cosmos DB database name.",
            ValueFromPipeline = false,
            ValueFromRemainingArguments = false)]

        public string Database { get; set; }
    }
}
