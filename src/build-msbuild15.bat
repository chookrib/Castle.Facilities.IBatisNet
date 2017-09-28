@echo If not install Visual Studio 2017, download BuildTools from https://www.visualstudio.com/downloads/ and install first.
nuget-4.3.0.4406.exe restore
SET PATH=%PATH%;C:\Program Files (x86)\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin\;C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin;C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin;C:\Program Files (x86)\Microsoft Visual Studio\2017\Visual Studio Community\MSBuild\15.0\Bin;
MSBuild.exe .\Castle.Facilities.IBatisNet.sln /t:Rebuild /p:Configuration=Release /p:WarningLevel=0
pause