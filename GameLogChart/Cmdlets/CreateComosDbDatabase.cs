using System.Management.Automation;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Cmdlets
{
    [Cmdlet(VerbsCommon.Add, "CosmosDbDatabase")]
    public class CreateComosDbDatabase : CosmosDbDatabaseCmdlet
    {
        protected override object ResolveDatabase()
        {
            return new Database {Id = Name};
        }

        protected override ResourceResponse<Database> OnProcessRecord(object database)
        {
            return DocumentClient.CreateDatabaseAsync((Database)database).Result;
        }
    }
}