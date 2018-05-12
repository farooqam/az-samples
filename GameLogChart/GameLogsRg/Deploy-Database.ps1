$name = "baseball"
$uri = "https://dbeklbpc33o5p5k.documents.azure.com:443/"
$key = "UUSh6LypgXzIw9UWCXCG6lPMjynrahDRqefEvvTlQ9KuLJZ5itJQGmRyxJ4H9x34ZR4bzOOzr7s4x6d0i0J27g==" 

Remove-CosmosDbDatbase -name $name -uri $uri -key $key
Add-CosmosDbDatbase -name $name -uri $uri -key $key