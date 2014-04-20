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
    $fixieRunner = resolve-path '.\packages\Fixie.0.0.1.158\lib\net45\Fixie.Console.exe'
    exec { & $fixieRunner $src\Tests\$project.Domain.Tests\bin\$configuration\$project.Domain.Tests.dll $src\Tests\$project.ChatServer.Tests\bin\$configuration\$project.ChatServer.Tests.dll $src\Tests\$project.ChatClient.Tests\bin\$configuration\$project.ChatClient.Tests.dll --fixie:NUnitXml TestResult.xml }
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