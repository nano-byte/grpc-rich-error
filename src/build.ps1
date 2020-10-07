﻿Param ($Version = "1.0-dev")
$ErrorActionPreference = "Stop"
pushd $PSScriptRoot

function Find-MSBuild {
    if (Test-Path "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe") {
        $vsDir = . "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe" -products * -property installationPath -format value -version 16.5
        if ($vsDir) {
            if (Test-Path "$vsDir\MSBuild\Current") { return "$vsDir\MSBuild\Current\Bin\amd64\MSBuild.exe" } else { return "$vsDir\MSBuild\15.0\Bin\amd64\MSBuild.exe" }
        }
    }
}

function Run-DotNet {
    if (Get-Command dotnet -ErrorAction SilentlyContinue) {
        dotnet @args
    } else {
        ..\0install.ps1 run --batch --version 3.1..!3.2 https://apps.0install.net/dotnet/core-sdk.xml @args
    }
    if ($LASTEXITCODE -ne 0) {throw "Exit Code: $LASTEXITCODE"}
}

function Run-MSBuild {
    $msbuild = Find-MSBuild
    if ($msbuild) {
        . $msbuild @args
        if ($LASTEXITCODE -ne 0) {throw "Exit Code: $LASTEXITCODE"}
    } else {
        Run-DotNet msbuild @args
    }
}

# Build
if ($env:CI) { $ci = "/p:ContinuousIntegrationBuild=True" }
Run-MSBuild /v:Quiet /t:Restore /t:Build $ci /p:Configuration=Release /p:Version=$Version

popd
