#!/usr/bin/env pwsh

param(
    [parameter(Mandatory=$true)]
    [string]$Rid,
    
    [Parameter()]
    [string]$Configuration="Release");

$CURRENT_TFM="net6.0"

$baseDir = Join-Path -Resolve $PSScriptRoot ..

Push-Location $baseDir

$releaseBuildPath = Join-Path $basedir "ReleaseBuild" "$Configuration.$Rid"
$releaseDir = Join-Path $basedir "Release"
$publishDirEditor = Join-Path $baseDir "Drizzle.Editor" "bin" $Configuration $CURRENT_TFM $Rid "publish"
$publishDirConsole = Join-Path $baseDir "Drizzle.ConsoleApp" "bin" $Configuration $CURRENT_TFM $Rid "publish"
$baseDataDir = Join-Path $baseDir "Data"
$releaseDataDir = Join-Path $releaseBuildPath "Data"

# Create directory if not exist.
New-Item -Force $(Join-Path $basedir "Release") -ItemType Directory | Out-Null
New-Item -Force $releaseBuildPath -ItemType Directory | Out-Null

# Clear directories of previous stuff
Remove-Item -Recurse $(Join-Path $publishDirEditor "*")
Remove-Item -Recurse $(Join-Path $publishDirConsole "*")
Remove-Item -Recurse $(Join-Path $releaseBuildPath "*")

Write-Host "Starting build $Configuration"

# Build projects.
dotnet publish $(Join-Path "Drizzle.Editor" "Drizzle.Editor.csproj") -c $Configuration -r $Rid `
 --self-contained /p:FullRelease=true /p:BuiltInComInteropSupport=true /p:PublishTrimmed=true /p:TrimMode=CopyUsed /p:NoWarn=IL2026
dotnet publish $(Join-Path "Drizzle.ConsoleApp" "Drizzle.ConsoleApp.csproj") -c $Configuration -r $Rid `
 --self-contained /p:FullRelease=true /p:PublishTrimmed=true /p:TrimMode=CopyUsed /p:NoWarn=IL2026

# Copy to intermediate build directory.
Copy-Item $(Join-Path $publishDirConsole "*") $releaseBuildPath
Copy-Item $(Join-Path $publishDirEditor "*") $releaseBuildPath

# Create data directory folders/folders
New-Item -ItemType Directory $(Join-Path $releaseBuildPath "Data") | Out-Null
New-Item -ItemType Directory $(Join-Path $releaseBuildPath "Data" "Levels") | Out-Null
New-Item -ItemType File $(Join-Path $releaseBuildPath "Data" "rendered_levels_go_here.txt") | Out-Null

# Copy data files
Copy-Item -Recurse $(Join-Path $baseDataDir "Graphics") $releaseDataDir
Copy-Item -Recurse $(Join-Path $baseDataDir "Props") $releaseDataDir
Copy-Item -Recurse $(Join-Path $baseDataDir "Cast") $releaseDataDir

# Compress final zip
Compress-Archive $(Join-Path $releaseBuildPath "*") -DestinationPath $(Join-Path $releaseDir "Drizzle.base.$Configuration.$Rid.zip") -CompressionLevel Optimal -Force

Pop-Location