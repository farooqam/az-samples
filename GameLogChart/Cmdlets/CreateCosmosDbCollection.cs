using System;
using System.Management.Automation;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Cmdlets
{
    public class CreateCosmosDbCollection : CosmosDbCollectionCmdLet
    {
        protected override void ProcessRecord()
        {
            try
            {
                var databaseUri = UriFactory.CreateDatabaseUri(Database);
                var documentCollection = new DocumentCollection {Id = Name};
                var response = DocumentClient.CreateDocumentCollectionAsync(databaseUri, documentCollection).Result;
                WriteObject(new {id = Name, staus = response.StatusCode, requestCharge = response.RequestCharge});
            }
            catch (AggregateException aggregateException)
            {
                WriteError(
                    new ErrorRecord(
                        aggregateException.InnerException,
                        "CosmosDbCollectionCmdLetError",
                        ErrorCategory.InvalidResult,
                        DocumentClient));
            }
            finally
            {
                DocumentClient.Dispose();
            }
        }
    }
}