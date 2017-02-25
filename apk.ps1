Param(
  [string]$projectPath="..\glimpse\Glimpse.Droid\GlimpseUser.Droid.csproj",
  [string]$packageName="glimpsePackage",
  [string]$configurationDirName="glimpseConfigurationDirName",
  [string]$keyAlias="keyAlias"
)
# Parameters are defined at first line of powershell script.
# Thanks to http://docs.xamarin.com/guides/android/deployment%2C_testing%2C_and_metrics/publishing_an_application/part_1_-_preparing_an_application_for_release for this script

# Parameters :
# - "projectPath" : project .csproj full path
# - "packageName" : package name for apk file name and package display name on Google play store.
# - "configurationDirName" : user friendly directory name, where to : find the keystore, archive the aligned APK.

# Prerequisites :
# - msbuild.exe directory, such as "C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\", should be added to system path
# - %JAVA_HOME% should be defined and "%JAVA_HOME%\bin" added to system path too.

# First clean the Release target.
msbuild.exe $projectPath /p:Configuration=Release /t:Clean

# project directory, reused later
$apkdir = split-path $projectPath

# Build the project, using the Release target.
msbuild.exe $projectPath /p:Configuration=Release /t:PackageForAndroid

# At this point there is only the unsigned APK - sign it.
# The script will pause here as jarsigner prompts for the password.
# It is possible to provide they keystore password for jarsigner.exe by adding an extra command line parameter -storepass, for example
#    -storepass <MY_SECRET_PASSWORD>
# If this script is to be checked in to source code control then it is not recommended to include the password as part of this script.

$apkdir +="\bin\Release\"

if(!(Test-Path($apkdir+${packageName}+".apk")))
	{
	echo("Android.Manifest file should define this package name : ${packageName}")
	return
	}

& jarsigner.exe -verbose -sigalg MD5withRSA -digestalg SHA1  -keystore "D:\SomePath\${configurationDirName}\${packageName}.keystore" -signedjar "${apkdir}${packageName}-signed.apk" "${apkdir}${packageName}.apk" ${keyAlias}

# Now zipalign it.  The -v parameter tells zipalign to verify the APK afterwards.
$zipalign = $env:LOCALAPPDATA
$zipalign += "\Android\android-sdk\tools\zipalign.exe"
& "${zipalign}" -f -v 4 "${apkdir}${packageName}-signed.apk" "${apkdir}${packageName}-Aligned.apk"

# Now archive apk files but not an intermediate one
& xcopy ${apkdir}${packageName}.apk D:\Developpement\Configuration\Android\${configurationDirName}\ /Y
& xcopy ${apkdir}${packageName}-Aligned.apk D:\Developpement\Configuration\Android\${configurationDirName}\ /Y