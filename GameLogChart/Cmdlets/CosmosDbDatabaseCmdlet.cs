using System;
using System.Management.Automation;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Cmdlets
{
    public abstract class CosmosDbDatabaseCmdlet : CosmosDbCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 2,
            HelpMessage = "The database name.",
            ValueFromPipeline = false,
            ValueFromRemainingArguments = false)]
        public string Name { get; set; }
        
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