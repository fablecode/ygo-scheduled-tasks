$serviceName = 'YgoCardInformation'

If (Get-Service $serviceName -ErrorAction SilentlyContinue) {

    If ((Get-Service $serviceName).Status -eq 'Running') {

        Stop-Service $serviceName
        Write-Host "Stopping $serviceName"

    } Else {

        Write-Host "$serviceName found, but it is not running."

    }

} Else {

    Write-Host "$serviceName not found"

}