$resourceGroup="New-RG"
$location="North Europe"
$appServiceName="demoplan12541258"
$webAppName="webapp12365789451"

New-AzAppServicePlan -ResourceGroupName $resourceGroup -Location $location `
-Name $appServiceName -Tier "F1"

New-AzWebApp -ResourceGroupName $resourceGroup -Name $webAppName `
-Location $location -AppServicePlan $appServiceName