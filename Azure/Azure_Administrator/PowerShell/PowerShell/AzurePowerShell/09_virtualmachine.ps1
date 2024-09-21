# Define variables
$resourceGroup = "demo-RG"  # Replace with your resource group name
$vmName = "NewVM"
$VMSize = "Standard_DS2_v2"
$location = "North Europe"
$networkInterfaceName = "app-interface"  # Replace with your network interface name

# Retrieve credentials for the VM
$Credential = Get-Credential

# Create VM configuration
$vmConfig = New-AzVMConfig -Name $vmName -VMSize $VMSize
Set-AzVMOperatingSystem -VM $vmConfig -ComputerName $vmName -Credential $Credential -Windows
Set-AzVMSourceImage -VM $vmConfig -PublisherName "MicrosoftWindowsServer" -Offer "WindowsServer" -Skus "2022-Datacenter" -Version "latest"

# Retrieve the network interface from the specified resource group
$networkInterface = Get-AzNetworkInterface -Name $networkInterfaceName -ResourceGroupName $resourceGroup

# Check if the network interface was found
if ($networkInterface) {
    # Add the network interface to the VM configuration
    $Vm = Add-AzVMNetworkInterface -VM $vmConfig -Id $networkInterface.Id

    # Disable boot diagnostics for the VM
    Set-AzVMBootDiagnostic -Disable -VM $Vm

    # Create the VM using the specified resource group and location
    New-AzVM -ResourceGroupName $resourceGroup -Location $location -VM $Vm
} else {
    Write-Host "Network interface not found in the specified resource group."
}
