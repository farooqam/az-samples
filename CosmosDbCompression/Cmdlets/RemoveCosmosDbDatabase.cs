using System;
using System.Management.Automation;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Cmdlets
{
    [Cmdlet(VerbsCommon.Remove, "CosmosDbDatabase")]
    public class RemoveComosDbDatabase : CosmosDbDatabaseCmdlet
    {
        protected override object ResolveDatabase()
        {
            return UriFactory.CreateDatabaseUri(Name);
        }

        protected override ResourceResponse<Database> OnProcessRecord(object database)
        {
            return DocumentClient.DeleteDatabaseAsync((Uri) database).Result;
        }
    }
}