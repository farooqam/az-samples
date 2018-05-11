function Create-CosmosDbDatabase {

    #[CmdletBinding()]

    <#Param(
        [Parameter(Mandatory=$True, Position=0)]
        [string] $cosmosDbAccount,

        [Parameter(Mandatory=$True, Position=1)]
        [string] $cosmosDbKey,

        [Parameter(Mandatory=$True, Position=2)]
        [string] $databaseName,

        [Parameter(Mandatory=$True, Position=3)]
        [string] $resourceGroupName
    ) #>

    Add-Type -AssemblyName System.Web

    [string] $cosmosDbAccount = "dbeklbpc33o5p5k"
    [string] $cosmosDbKey = "UUSh6LypgXzIw9UWCXCG6lPMjynrahDRqefEvvTlQ9KuLJZ5itJQGmRyxJ4H9x34ZR4bzOOzr7s4x6d0i0J27g=="
    [string] $databaseName = "baseball"
    [string] $resourceGroupName = "baseball"

    [string]$resourceUri = "https://${cosmosDbAccount}.documents.azure.com/dbs"
    
    [hashtable]$body = @{
        id = $databaseName
    }

    $now = (get-date).ToUniversalTime().ToString("R")

    [hashtable]$headers = @{
        "x-ms-date" = $now;
        "x-ms-version" = "2015-08-06";
        "Accept" = "application/json"
    }

    # Generate the auth header
    [byte[]] $decodedKey = [System.Convert]::FromBase64String($cosmosDbKey)
    
    $hmac = New-Object System.Security.Cryptography.HMACSHA256 
    $hmac.Key = $decodedKey

    [string] $payLoad = "post`ndbs`ndbs`n${now}`n${[string]::Empty}`n"
    Write-Host $payLoad

    [byte[]]$hashPayLoad = $hmac.ComputeHash([System.Text.Encoding]::UTF8.GetBytes($payLoad))
    [string] $signature = [System.Convert]::ToBase64String($hashPayLoad)

    [string]$encodedUrl = [System.Web.HttpUtility]::UrlEncode("type=master&ver=1.0&sig=${signature}")

    $headers.Add("Authorization", $encodedUrl);

    Write-Host ($headers | Out-String)

    try
    {
        $result = Invoke-RestMethod -Method Post -Uri $resourceUri -Body $body -Headers $headers
    }
    catch 
    {
        $result = $_.Exception.Response.GetResponseStream()
        $reader = New-Object System.IO.StreamReader($result)
        $reader.BaseStream.Position = 0
        $reader.DiscardBufferedData()
        $responseBody = $reader.ReadToEnd();

        Write-Host $responseBody
    }

    

}