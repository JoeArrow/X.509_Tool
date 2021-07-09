# ---------------------------------------------------------
# The purpose of this Caller Function is to make a call to
# Run MSTest.ps1 with specific inputs.

Clear-Host

$path = 'C:\Usage\Source\Tools\Security.String.Extensions\Security.String.Extensions_UT\'

$specificArguments = ''

$runDate = Get-Date -format "yyyy_MM_dd_HH_mm"

$outputFile = "C:\Usage\TestOutput\Security\Security.String.Extensions_$runDate.txt"

$testRunner = "C:\Usage\Scripting\Powershell\UnitTesting\Functional\Run_VSTest.ps1"

$testContainer = "${path}\bin\Debug\Security.String.Extensions_UT.dll"

# -------------------
# Run the Test Engine

Write-Host "`nTest Engine:`t`t$testRunner" 
Write-Host "`nTest Container:`t$testContainer" 
Write-Host "`nOutput File:`t`t$outputFile" 
Write-Host "`nArguments:`t`t`t$specificArguments`n`n"

. $testRunner $path $testContainer $outputFile $specificArguments

# --------
# Clean up

if($path) { Clear-Variable path }
if($outputFile) { Clear-Variable outputFile }
if($testRunner) { Clear-Variable testRunner }
if($testContainer) { Clear-Variable testContainer }
if($specificArguments) { Clear-Variable specificArguments }