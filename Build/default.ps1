properties {
	$testMessage = 'Executed Test!'
	$cleanMessage = 'Executed Clean!'

	$solutionDirectory = (Get-Item $solutionFile).DirectoryName
	$outputDirectory= "$solutionDirectory\.build"
	$temporaryOutputDirectory = "$outputDirectory\temp"
	$buildConfiguration = "Release"
	$buildPlatform = "Any CPU"
}

FormatTaskName "`r`n`r`n-------- Executing {0} Task --------"

task default -depends Test

task Init `
	-description "Initializes the build by removing previous artifacts and creating output directories" `
    -requiredVariables outputDirectory, temporaryOutputDirectory `
{
	
	Assert ("Debug", "Release" -contains $buildConfiguration) `
			"Invalid build configuration '$buildConfiguration'. Enter a valid value such as 'Debug' or 'Release'"

	Assert("x86", "x64", "Any CPU" -contains $buildPlatform) `
			"Invalid build platform '$buildPlatform'. Enter a valid value such as 'x86', 'x64',  or 'Any CPU'"


	# Remove previous build results
	if(Test-Path $outputDirectory)
	{
		Write-Host "Removing output directory located at $outputDirectory"
		Remove-Item $outputDirectory -Force -Recurse
	}
	
	Write-Host "Creating output directory located at ..\.build"
	New-Item $outputDirectory -ItemType Directory | Out-Null

	Write-Host "Creating temporary directory located at $temporaryOutputDirectory"
	New-Item $temporaryOutputDirectory -ItemType Directory | Out-Null

}

task Compile `
	-depends Init `
	-description "Compile the code" `
	-requiredVariables solutionFile, buildConfiguration, buildPlatform, temporaryOutputDirectory `
{ 
  	Write-Host "Building solution $solutionFile"
	Exec { 
		msbuild $solutionFile "/p:Configuration=$buildConfiguration;Platform=$buildPlatform;OutDir=$temporaryOutputDirectory"
	}
}

task Clean -description "Remove temporary files" {
	Write-Host $cleanMessage
}

task Test -depends Compile, Clean -description "Run unit tests"  {
	Write-Host $testMessage
} 