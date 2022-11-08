# MSTestWinAppSDK11BugSolved

The solution can be found in the .csproj file near the top. It involves making sure you have
three MSBuild properties defined as being true within a <PropertyGroup> in the .csproj file:
- EnableMsixTooling
- EnablePreviewMsixTooling
- WindowsAppContainer

EnableMsixTooling replaced EnablePreviewMsixTooling starting in WindowsAppSDK 1.1 but there
does not seem to be any harm in having both set. You can probably eliminate EnablePreviewMsixTooling
but you should do thorough testing beforehand to ensure that its absence does not cause any problems
for you because of some unexpected or unknown dependency on it being set. 

WindowsAppContainer was defined by WindowsAppSDK 1.0 but is no longer defined by WindowsAppSDK starting
with version 1.1. This might be related to the additions and changes to the deployment options but I
could not find any published documentation about it. However there is some unofficial documentation
located in one of the .targets files in the WindowsAppSDK NuGet package.

In the NuGet package files for SDK 1.0.x, I found a comment stating that WindowsAppContainer
was being defined to enable the 'Project->Publish' menu in Visual Studio and the .appxmanifest
designer. This is followed by a conditional definition of it which is an indirect check for the
existence of a Package.appxmanifest file in the project. That comment and definition are missing in
the SDK 1.1.x version of the file. Instead, WinUI packaged projects now use a conditional MSBuild
property to enable the Package and Publish menu.

There is no exact definition given for WindowsAppContainer, however later in the file there is a
comment that state that it is used to differentiate between projects that target the Universal
device family and those that do not. It also includes language that seems to make it clear that
this is the modern Windows 10+ concept (i.e. a packaged application with a WinUI 3
Application-derived class as its entry point versus a classic desktop application (even if it uses
WindowsAppSDK features) such as Win32, WPF, and WinForms applications.

None of that is official documentation. You should take care to make sure that if you switch your
application to being unpackaged or have it startup as a desktop app that you remove the definition
for WindowsAppContainer. If some builds of your application will be packaged WinUI3 apps and other
builds will be desktop apps, you should use MSBuild conditions to make sure that you only define
it for WinUI3 packaged app build configurations.

Look for the XML comments near the top of the .csproj file to find an example usage of them.
