# VfxSettings
This PowerShell module allows you to manage various visual effects settings in Windows such as animations, transparency, font smoothing and more.
Most settings are managed by API calls to SystemParametersInfo: https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-systemparametersinfow
but some settings are also handled with simple registry key changes.

# Getting started
Install the module from the PowerShell gallery: `Install-Module VfxSettings`  
Then check the available commands in the module: `Get-Command -Module VfxSettings`  
Use Get-Help to get information about what each parameter value does.
For example: `Get-Help Enable-VfxSetting -Parameter Setting` will show all the effects that can be enabled.