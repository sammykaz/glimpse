You must have:
-Windows 8 or later installed
-.NET Framework 4.5+ installed

Please proceed in order of steps, it is important to start from step 1, because Android SDK relies on JDK
and will give you different tools for your JDK version. 

1. Install Java JDK 8 101, 64 Bits
http://www.oracle.com/technetwork/java/javase/downloads/jdk8-downloads-2133151.html

2. Install Android SDK Tools, installer_r24.4.1-windows.exe
https://developer.android.com/studio/index.html

3. Install Xamarin for Visual Studio by going to Control Panel -> Modify -> Xamarin 4.1.1

4. Update Xamarin to latest version 4.2.0.703

5. In Visual Studio -> SDK Manager, make sure you have all Android API 21-24 (Do not install Android Wear and Android TV)

6. Download Visual Studio Emulator for Android

7. In Visual Studio -> Tools -> Options -> Xamarin 
   Make sure you have Java SDK, Android SDK, Android NDK pointing to the right path.
