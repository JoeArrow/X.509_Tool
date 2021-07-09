clear-host

${name} = 'Security.String.Extensions'
${Location} = "C:\Usage\_Source\CSharp\JoeWare\Main\Security\${name}"
 
${version} = '1.0.0'
${now} = [datetime]::now.ToString('yyyy-MM-dd_HH-mm')

${coverageXML} = "${Location}\${now}.coveragexml"
${runSettings} = "${Location}\Security.String.Extensions_UT\CodeCoverage.runsettings"

Set-Location ${Location}

${VSInstall} = "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise"
${CodeCoverage} = "${VSInstall}\Team Tools\Dynamic Code Coverage Tools\CodeCoverage.exe"
${testEngine} = "${VSInstall}\Common7\IDE\CommonExtensions\Microsoft\TestWindow\VSTest.console.exe"

${Solution} = "${Location}\${name}.sln"
${MSBuild} = "${VSInstall}\MSBuild\Current\Bin\MSBuild.exe"

${CoverageExclusions} = "${Location}\Security.String.Extensions_UT\**"

${testContainers} = @("${location}\Security.String.Extensions_UT\bin\Debug\Security.String.Extensions_UT.dll")

# =========================================================
# Start Listening

& SonarScanner.MSBuild.exe begin /k:${name} /n:${name} /v:${version} `
                                 /d:sonar.cs.vscoveragexml.reportsPaths=${coverageXML} `
                                 /d:sonar.coverage.exclusions=".xml,.xsd,.xsl,${CoverageExclusions}" `
                                 /d:sonar.exclusions="**/Web References/**" `
                                 /d:sonar.test.exclusions="**/*test*/**"

# =========================================================
# Build and Test, I am sure that we should NOT re-build...

Write-Host "`n`nBuilding: ${Solution}`n`n"

& ${MSBuild} ${Solution}

# ============================================
# Run All Tests and Save Results in a trx file

Write-Host "`n`nRunning Tests: ${testContainers}`n`n"

& ${testEngine} /Logger:trx ${testContainers} "/settings:${runSettings}" /Enablecodecoverage

# =====================================================
# Convert the Code Coverage Report from Binary into XML

${coverageFile} = (Get-ChildItem -Path ${Location}\TestResults -Filter *.coverage -Recurse | Select-Object -First 1).fullname
    
& ${CodeCoverage} analyze /output:${coverageXML} ${coverageFile}

& SonarScanner.MSBuild.exe end 

if($now){ Clear-Variable now }
if($name){ Clear-Variable name }
if($version){ Clear-Variable version }
if($MSBuild){ Clear-Variable MSBuild }
if($Solution){ Clear-Variable Solution }
if($Location){ Clear-Variable Location }
if($VSInstall){ Clear-Variable VSInstall }
if($coverageXML){ Clear-Variable coverageXML }
if($CodeCoverage){ Clear-Variable CodeCoverage }
if($CoverageExclusions){ Clear-Variable CoverageExclusions }