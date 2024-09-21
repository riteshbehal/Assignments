$resourceGroup="demo-RG"
$location="North Europe"
$accountSKU="Standard_LRS"
$storageAccountName="demostorage4422886603"
$storageAccountKind="StorageV2"

New-AzStorageAccount -ResourceGroupName $resourceGroup -Name $storageAccountName `
-Location $location -Kind $storageAccountKind -SkuName $accountSKU