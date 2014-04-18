@echo off

.\src\.nuget\nuget.exe install src\.nuget\packages.config -source "https://nuget.org/api/v2/" -RequireConsent -o "packages"

powershell -NoProfile -ExecutionPolicy Bypass -Command "& '%~dp0\packages\psake.4.3.2\tools\psake.ps1' %*; if ($psake.build_success -eq $false) { write-host "Build Failed!" -fore RED; exit 1 } else { exit 0 }"
