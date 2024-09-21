$resourceGroup = "demo-RG"
$networkName = "app-network"
$location = "North Europe"
$AddressPrefix = "10.0.0.0/16"
$subnetName = "SubnetA"
$subnetAddressPrefix = "10.0.0.0/24"

# Create Virtual Network
$subnet = New-AzVirtualNetworkSubnetConfig -Name $subnetName -AddressPrefix $subnetAddressPrefix
New-AzVirtualNetwork -Name $networkName -ResourceGroupName $resourceGroup -Location $location -AddressPrefix $AddressPrefix -Subnet $subnet

# Create Public IP Address
$publicIPAddressName = "app-ip"
$publicIPAddress = New-AzPublicIpAddress -Name $publicIPAddressName -ResourceGroupName $resourceGroup `
    -Location $location -AllocationMethod Static


# Create Network Interface
$networkInterfaceName = "app-interface"
$VirtualNetwork = Get-AzVirtualNetwork -Name $networkName -ResourceGroupName $resourceGroup
$subnet = Get-AzVirtualNetworkSubnetConfig -VirtualNetwork $VirtualNetwork -Name $subnetName
$networkInterface = New-AzNetworkInterface -Name $networkInterfaceName -ResourceGroupName $resourceGroup -Location $location -SubnetId $subnet.Id -PublicIpAddressId $publicIPAddress.Id -IpConfigurationName "IpConfig"

# Create Network Security Group (NSG)
$networkSecurityGroupName = "app-nsg"
$nsgRule1 = New-AzNetworkSecurityRuleConfig -Name "Allow-RDP" -Access Allow -Protocol Tcp -Direction Inbound -Priority 120 -SourceAddressPrefix Internet -SourcePortRange * -DestinationAddressPrefix "10.0.0.0/24" -DestinationPortRange 3389
$nsgRule2 = New-AzNetworkSecurityRuleConfig -Name "Allow-HTTP" -Access Allow -Protocol Tcp -Direction Inbound -Priority 130 -SourceAddressPrefix Internet -SourcePortRange * -DestinationAddressPrefix "10.0.0.0/24" -DestinationPortRange 80
$networkSecurityGroup = New-AzNetworkSecurityGroup -Name $networkSecurityGroupName -ResourceGroupName $resourceGroup -Location $location -SecurityRules $nsgRule1,$nsgRule2

# Attach the NSG to the subnet
Set-AzVirtualNetworkSubnetConfig -Name $subnetName -VirtualNetwork $VirtualNetwork -NetworkSecurityGroup $networkSecurityGroup -AddressPrefix $subnetAddressPrefix
$VirtualNetwork | Set-AzVirtualNetwork

# Create Virtual Machine
$vmName = "appvm"
$VMSize = "Standard_DS2_v2"
$Credential = Get-Credential
$vmConfig = New-AzVMConfig -Name $vmName -VMSize $VMSize
Set-AzVMOperatingSystem -VM $vmConfig -ComputerName $vmName -Credential $Credential -Windows
Set-AzVMSourceImage -VM $vmConfig -PublisherName "MicrosoftWindowsServer" -Offer "WindowsServer" -Skus "2022-Datacenter" -Version "latest"
$networkInterfaceName = "app-interface"
$networkInterface = Get-AzNetworkInterface -Name $networkInterfaceName -ResourceGroupName $resourceGroup
$Vm = Add-AzVMNetworkInterface -VM $vmConfig -Id $networkInterface.Id
Set-AzVMBootDiagnostic -Disable -VM $Vm
New-AzVM -ResourceGroupName $resourceGroup -Location $location -VM $Vm
