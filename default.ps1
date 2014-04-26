Framework '4.0'

properties {
    $project = "ChatterBox"
    $birthYear = 2014
    $maintainers = "Gert Jansen van Rensburg"
    $description = "Chat Service using NimbusAPI"

    $configuration = 'Release'
    $src = resolve-path '.\src'
    $build = if ($env:build_number -ne $NULL) { $env:build_number } else { '0' }
    $version = [IO.File]::ReadAllText('.\VERSION.txt') + '.' + $build
}

task default -depends Test

task Package -depends Test {
    rd .\package -recurse -force -ErrorAction SilentlyContinue | out-null
    mkdir .\package -ErrorAction SilentlyContinue | out-null
    exec { & $src\.nuget\NuGet.exe pack $src\$project\$project.csproj -Symbols -Prop Configuration=$configuration -OutputDirectory .\package }

    write-host
    write-host "To publish these packages, issue the following command:"
    write-host "   nuget push .\package\$project.$version.nupkg"
}

task Test -depends Compile {
    $nunitRunner = resolve-path '.\packages\NUnit.Runners.2.6.3\tools\nunit-console.exe'
    exec { & $nunitRunner $src\Tests\ChatterBox.ChatServer.Tests\bin\Release\ChatterBox.ChatServer.Tests.dll $src\Tests\ChatterBox.ChatClient.Tests\bin\Release\ChatterBox.ChatClient.Tests.dll $src\Tests\ChatterBox.Domain.Tests\bin\Release\ChatterBox.Domain.Tests.dll $src\Tests\ChatterBox.Core.Tests\bin\Release\ChatterBox.Core.Tests.dll }
}

task Compile -depends CommonAssemblyInfo {
  rd .\build -recurse -force  -ErrorAction SilentlyContinue | out-null
  exec { msbuild /t:clean /v:q /nologo /p:Configuration=$configuration $src\$project.sln }
  exec { msbuild /t:build /v:q /nologo /p:Configuration=$configuration $src\$project.sln }
}

task CommonAssemblyInfo {
    $date = Get-Date
    $year = $date.Year
    $copyrightSpan = if ($year -eq $birthYear) { $year } else { "$birthYear-$year" }
    $copyright = "Copyright (c) $copyrightSpan $maintainers"

"using System.Reflection;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]
[assembly: AssemblyProduct(""$project"")]
[assembly: AssemblyVersion(""$version"")]
[assembly: AssemblyFileVersion(""$version"")]
[assembly: AssemblyCopyright(""$copyright"")]
[assembly: AssemblyCompany(""$maintainers"")]
[assembly: AssemblyDescription(""$description"")]
[assembly: AssemblyConfiguration(""$configuration"")]" | out-file "$src\CommonAssemblyInfo.cs" -encoding "ASCII"
}