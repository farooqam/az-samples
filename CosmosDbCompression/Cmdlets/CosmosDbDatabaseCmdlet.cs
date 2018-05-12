using System;
using System.Management.Automation;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Cmdlets
{
    public abstract class CosmosDbDatabaseCmdlet : Cmdlet
    {
        protected DocumentClient DocumentClient { get; private set; }

        [Parameter(
            Mandatory = true,
            Position = 0,
            HelpMessage = "The database name.",
            ValueFromPipeline = false,
            ValueFromRemainingArguments = false)]
        public string Name { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 1,
            HelpMessage = "The Cosmos DB URI.",
            ValueFromPipeline = false,
            ValueFromRemainingArguments = false)]

        public Uri Uri { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 2,
            HelpMessage = "The Cosmos DB master key.",
            ValueFromPipeline = false,
            ValueFromRemainingArguments = false)]

        public string Key { get; set; }

        protected override void BeginProcessing()
        {
            DocumentClient = new DocumentClient(Uri, Key);
        }

        protected override void ProcessRecord()
        {
            try
            {
                var db = ResolveDatabase();
                var response = OnProcessRecord(db);
                WriteObject(new {id = Name, staus = response.StatusCode, requestCharge = response.RequestCharge});
            }
            catch (AggregateException aggregateException)
            {
                WriteError(
                    new ErrorRecord(
                        aggregateException.InnerException,
                        "CosmosDbDatabaseCmdletError",
                        ErrorCategory.InvalidResult,
                        DocumentClient));
            }
            finally
            {
                DocumentClient.Dispose();
            }
        }

        protected abstract object ResolveDatabase();

        protected abstract ResourceResponse<Database> OnProcessRecord(object database);
    }
}