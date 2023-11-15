import-module servermanager
add-windowsfeature web-server -includeallsubfeature
set-content -path "c:\inetpub\wwwroot\Default.html" -Value "This is server $($env:computername) !"
