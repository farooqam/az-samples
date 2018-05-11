SET resourceGroupName=""
SET name=""
SET databaseName=""
SET collectionName=""
SET key=""
SET endpoint=""

CALL az cosmosdb database delete --db-name %databaseName% --key %key% --name %name% --resource-group-name %resourceGroupName% --url-connection %endpoint%
CALL az cosmosdb database create --name %name% --db-name %databaseName% --resource-group %resourceGroupName%
CALL az cosmosdb collection create --collection-name %collectionName% --name %name% --db-name %databaseName% --resource-group %resourceGroupName%

	