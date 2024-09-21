$resourceGroup = "demo-RG"
$networkName = "app-network"
$subnetName = "SubnetA"
$networkInterfaceName = "app-interface"
$location = "North Europe"  

$VirtualNetwork = Get-AzVirtualNetwork -Name $networkName -ResourceGroupName $resourceGroup

$subnet = Get-AzVirtualNetworkSubnetConfig -VirtualNetwork $VirtualNetwork -Name $subnetName

New-AzNetworkInterface -Name $networkInterfaceName -ResourceGroupName $resourceGroup -Location $location `
    -SubnetId $subnet.Id -IpConfigurationName "IpConfig"
