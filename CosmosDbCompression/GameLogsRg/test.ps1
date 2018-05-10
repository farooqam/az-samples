[CmdletBinding()]

Param(
    [Parameter(Mandatory=$True, Position=1)]
    [string] $cosmosDbEndpoint,

    [Parameter(Mandatory=$True, Position=2)]
    [string] $cosmosDbKey,

    [Parameter(Mandatory=$True, Position=3)]
    [string] $databaseName,

    [Parameter(Mandatory=$True, Position=4)]
    [string] $resourceGroupName,

    [Parameter(Mandatory=$True, Position=5)]
    [string] $dependentAssemblyFolder
)

Invoke-RestMethod -Method Post 

[System.Reflection.Assembly]::Load([System.IO.File]::ReadAllBytes([System.IO.Path]::Combine($dependentAssemblyFolder, "Newtonsoft.Json.dll"))) | Out-Null
[System.Reflection.Assembly]::Load([System.IO.File]::ReadAllBytes([System.IO.Path]::Combine($dependentAssemblyFolder, "Microsoft.Azure.DocumentDB.Core.dll"))) | Out-Null


$uri = New-Object System.Uri($cosmosDbEndpoint)
New-Object Microsoft.Azure.Documents.Client.DocumentClient($uri, $cosmosDbKey, $null, $null)