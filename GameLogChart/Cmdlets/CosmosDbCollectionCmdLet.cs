using System.Management.Automation;

namespace Cmdlets
{
    public class CosmosDbCollectionCmdlet : CosmosDbCmdlet
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