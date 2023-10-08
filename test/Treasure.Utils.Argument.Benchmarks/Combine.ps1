#Requires -PSEdition Core

[CmdletBinding()]
Param (
    [string[]] $TFMs = @('net6.0', 'net6.0-windows', 'net7.0', 'net7.0-windows'),
    [string[]] $Columns = @( 'Runtime', 'TFM', 'LibraryTfm', 'Method', 'Mean', 'Error', 'StdDev', 'Code Size'),
    [string] $ArtifactInputRoot = (Join-Path $PSScriptRoot '..' '..' '__benchmarks'),
    [string] $ResultOutputPath = (Join-Path $PSScriptRoot '..' '..' '__benchmarks' 'results.md'),
    [string[]] $MethodSortOrder = @(
        'ArgumentNotNull',
        'ArgumentNullExceptionThrowIfNull',
        'IfValueIsNullThrowArgumentNullException',
        'ArgumentNotNullOrEmpty',
        'ArgumentExceptionThrowIfNullOrEmpty',
        'IfValueIsNullOrEmptyThrowArgumentException',
        'ArgumentNotNullOrWhiteSpace',
        'ArgumentExceptionThrowIfNullOrWhiteSpace',
        'IfValueIsNullOrWhiteSpaceThrowArgumentException')
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version 2

function BuildTableRowFromData ($rowData) {
    $row = '|'

    foreach ($data in $rowData) {
        $row += " $data |"
    }

    return $row
}

function BuildTableRowFromColumnsAndData ($columns, $data) {
    BuildTableRowFromData ($columns | ForEach-Object { $data.$_ })
}

function GetLibraryTfmName ($tfm) {
    if ($tfm -eq 'net6.0') {
        return 'net6.0'
    }

    if ($tfm -eq 'net7.0') {
        return 'net7.0'
    }

    if ($tfm.Contains('-')) {
        return 'netstandard2.1'
    }

    throw "Unknown TFM '$tfm'."
}

function GetLibraryTfmValue ($data, $libraryTfm) {
    if ($data.Method.Contains('Exception')) {
        return 'NA'
    }

    return $libraryTfm
}

if (!(Test-Path $ArtifactInputRoot)) {
    Write-Error "Artifact input root path '$ArtifactInputRoot' does not exist."
}

$csvData = @()
$TFMs
    | ForEach-Object {
        [PSCustomObject] @{
            libraryTfm = (GetLibraryTfmName $_)
            tfm = $_
            path = (Join-Path $ArtifactInputRoot "$_.artifacts/results/Tests-report.csv")
        }
    }
    | Where-Object { Test-Path $_.path }
    | ForEach-Object {
        $libraryTfm = $_.libraryTfm
        $tfm = $_.tfm
        Import-Csv -Path $_.path
            | ForEach-Object {
                $_ | Add-Member -NotePropertyName 'LibraryTfm' -NotePropertyValue (GetLibraryTfmValue $_ $libraryTfm) -PassThru
                   | Add-Member -NotePropertyName 'TFM' -NotePropertyValue $tfm -PassThru
            }
    }
    | ForEach-Object { $csvData += $_ }
$csvData = $csvData | Sort-Object { $MethodSortOrder.IndexOf($_.Method) }, Runtime, LibraryTfm, TFM

# Write header to markdown file
Set-Content -Path $ResultOutputPath -Value '# Results'
Add-Content -Path $ResultOutputPath -Value ''
Add-Content -Path $ResultOutputPath -Value (BuildTableRowFromData $Columns)
Add-Content -Path $ResultOutputPath -Value (BuildTableRowFromData ($Columns | ForEach-Object { '-'*$_.Length }))

# Write to markdown file
$emptyRow = BuildTableRowFromData ($Columns | ForEach-Object { '' } )
$lastMethod = $csvData[0].Method
$csvData | ForEach-Object {
    $method = $_.Method

    if ($method -ne $lastMethod) {
        Add-Content -Path $ResultOutputPath -Value $emptyRow
        $lastMethod = $method
    }

    $mdLine = BuildTableRowFromColumnsAndData $Columns $_
    Add-Content -Path $ResultOutputPath -Value $mdLine
}
