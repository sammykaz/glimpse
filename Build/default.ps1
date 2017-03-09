Include ".\helpers.ps1"

properties {
  	$cleanMessage = 'Executed Clean!'
	$testMessage = 'Executed Test!'
  
	$solutionDirectory = (Get-Item $solutionFile).DirectoryName
	$outputDirectory= "$solutionDirectory\.build"
	$temporaryOutputDirectory = "$outputDirectory\temp"

	# Find Test Projects
	$publishedMSTestTestsDirectory = "$temporaryOutputDirectory\_PublishedMSTestTests"
	$publishedNUnitTestsDirectory = "$temporaryOutputDirectory\_PublishedNUnitTests"

	$testResultsDirectory = "$outputDirectory\TestResults"

	#Place Test Results in this directory
	$MSTestTestResultsDirectory = "$testResultsDirectory\MSTest"
	$NUnitTestResultsDirectory = "$testResultsDirectory\NUnit"

	$buildConfiguration = "Debug"
	$buildPlatform = "Any CPU"

	$packagesPath = "$solutionDirectory\packages"
	
	#Using vsTest Runner
	$vsTestExe = (Get-ChildItem ("C:\Program Files (x86)\Microsoft Visual Studio*\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe")).FullName | Sort-Object $_ | select -last 1
	
	#Using Nunit Runner
	$NUnitExe = (Find-PackagePath $packagesPath "NUnit.ConsoleRunner" ) + "\tools\nunit3-console.exe"

}

FormatTaskName "`r`n`r`n-------- Executing {0} Task --------"

task default -depends Test

task Init `
  -description "Initialises the build by removing previous artifacts and creating output directories" `
  -requiredVariables outputDirectory, temporaryOutputDirectory `
{
	Assert ("Debug", "Release" -contains $buildConfiguration) `
		   "Invalid build configuration '$buildConfiguration'. Valid values are 'Debug' or 'Release'"

	Assert ("x86", "x64", "Any CPU" -contains $buildPlatform) `
		   "Invalid build platform '$buildPlatform'. Valid values are 'x86', 'x64' or 'Any CPU'"

	# Check that all tools are available
	Write-Host "Checking that all required tools are available"

	Write-Host "PATH IS $NUnitExe"

	Assert (Test-Path $vsTestExe) "VSTest Console could not be found"
	Assert (Test-Path $NUnitExe) "NUnit Console could not be found"

	# Remove previous build results
	if (Test-Path $outputDirectory) 
	{
		Write-Host "Removing output directory located at $outputDirectory"
		Remove-Item $outputDirectory -Force -Recurse
	}
	
	Write-Host "Creating output directory located at $outputDirectory"
	New-Item $outputDirectory -ItemType Directory | Out-Null

	Write-Host "Creating temporary directory located at $temporaryOutputDirectory"
	New-Item $temporaryOutputDirectory -ItemType Directory | Out-Null
}

task Compile `
	-depends Init `
	-description "Compile the code" `
	-requiredVariables solutionFile, buildConfiguration, buildPlatform, temporaryOutputDirectory `
{ 
  	Write-Host "Building solution $solutionFile" 
	Exec {
		msbuild $SolutionFile "/p:Configuration=$buildConfiguration;Platform=$buildPlatform;OutDir=$temporaryOutputDirectory"
	}
}

#NUnit Test Task
task TestNUnit `
	-depends Compile `
	-description "Run NUnit tests" `
	-precondition { return Test-Path $publishedNUnitTestsDirectory } `
	-requiredVariable publishedNUnitTestsDirectory, NUnitTestResultsDirectory `
{
	$testAssemblies = Prepare-Tests -testRunnerName "NUnit" `
									-publishedTestsDirectory $publishedNUnitTestsDirectory `
									-testResultsDirectory $NUnitTestResultsDirectory

	Exec { &$nunitExe $testAssemblies /xml:$NUnitTestResultsDirectory\NUnit.xml /nologo /noshadow }
}


#MSBuild Test Task depends on the compile task and will run the MSTest tests
task TestMSTest `
	-depends Compile `
	-description "Run MSTest tests" `
	-precondition { return Test-Path $publishedMSTestTestsDirectory } `
	-requiredVariable publishedMSTestTestsDirectory, MSTestTestResultsDirectory `
{
	$testAssemblies = Prepare-Tests -testRunnerName "MSTest" `
									-publishedTestsDirectory $publishedMSTestTestsDirectory `
									-testResultsDirectory $MSTestTestResultsDirectory

	# vstest console doesn't have any option to change the output directory
	# so we need to change the working directory
	Push-Location $MSTestTestResultsDirectory
	Exec { &$vsTestExe $testAssemblies /Logger:trx }
	Pop-Location

	# move the .trx file back to $MSTestTestResultsDirectory
	Move-Item -path $MSTestTestResultsDirectory\TestResults\*.trx -destination $MSTestTestResultsDirectory\MSTest.trx

	Remove-Item $MSTestTestResultsDirectory\TestResults
}

task Clean -description "Remove temporary files" 
{ 
  	Write-Host $cleanMessage
}
 
task Test `
	 -depends Compile, TestMSTest `
	 -description "Run unit tests" 
{ 
	Write-Host $testMessage
}