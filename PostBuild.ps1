﻿#Requires -Version 5.1 -Modules Microsoft.PowerShell.PlatyPS
param
(
    [Parameter(Mandatory)]
    [string]
    $OutputFile
)

$ModuleName = [System.IO.Path]::GetFileNameWithoutExtension($OutputFile)
$OutputDir = Split-Path -LiteralPath $OutputFile

$ModuleInfo = Import-Module -Name $OutputFile -PassThru -ErrorAction Stop
$CmdletsToExport = $ModuleInfo.ExportedCmdlets.Keys.ForEach({"'$_'"}) -join ','
Remove-Module -ModuleInfo $ModuleInfo

if (Test-Path -LiteralPath "$PSScriptRoot\Docs")
{
    $null = New-Item -Path "$OutputDir\en-US" -ItemType Directory -Force
    Get-ChildItem -LiteralPath "$PSScriptRoot\Docs" -Recurse -File -Filter *.md |
        Import-MarkdownCommandHelp |
        Export-MamlCommandHelp -OutputFolder $env:TEMP -Force |
        Move-Item -Destination "$OutputDir\en-US" -Force
}

#region Update module manifest
$null = New-Item -Path "$OutputDir\$ModuleName.psd1" -ItemType File -Force

$FileList = (Get-ChildItem -LiteralPath $OutputDir -File -Recurse | ForEach-Object -Process {
    "'$($_.FullName.Replace("$OutputDir\", ''))'"
}) -join ','

$ReleaseNotes = Get-Content -LiteralPath "$PSScriptRoot\Release notes.txt" -Raw

$Version = $ReleaseNotes.Split(':')[0]

((Get-Content -LiteralPath "$PSScriptRoot\ModuleManifest.psd1" -Raw) -replace '{(?=[^\d])','{{' -replace '(?<!\d)}','}}') -f @(
    "'$Version'"
    $CmdletsToExport
    $FileList
    $ReleaseNotes
) | Set-Content -LiteralPath "$OutputDir\$ModuleName.psd1" -Force
#endregion

$ReleaseDir = Join-Path -Path $PSScriptRoot -ChildPath "Releases\$ModuleName\$Version"
Get-Item -LiteralPath $ReleaseDir -Force -ErrorAction Ignore | Remove-Item -Recurse -Force -ErrorAction Stop
$null = New-Item -Path $ReleaseDir -ItemType Directory -Force
Get-ChildItem -LiteralPath $OutputDir | Copy-Item -Destination $ReleaseDir -Force -Recurse -Container -ErrorAction Stop