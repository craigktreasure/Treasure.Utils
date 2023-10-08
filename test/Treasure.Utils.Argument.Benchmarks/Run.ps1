#Requires -PSEdition Core

[CmdletBinding()]
Param (
    [string[]] $TFMs = @('net6.0', 'net6.0-windows', 'net7.0', 'net7.0-windows'),
    [string] $ArtifactOutputRoot = (Join-Path $PSScriptRoot '..' '..' '__benchmarks'),
    [string] $ResultOutputPath = (Join-Path $PSScriptRoot '..' '..' '__benchmarks' 'results.md')
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version 2

Push-Location $PSScriptRoot
try {
    foreach ($tfm in $TFMs) {
        $tfmBenchmarkOutput = Join-Path $ArtifactOutputRoot "$tfm.artifacts"
        dotnet run -c Release -f $tfm --filter "*" -- -a $tfmBenchmarkOutput
    }
}
finally {
    Pop-Location
}

$combineScriptPath = Join-Path $PSScriptRoot 'Combine.ps1'
. $combineScriptPath -TFMs $TFMs -ArtifactInputRoot $ArtifactOutputRoot -ResultOutputPath $ResultOutputPath
