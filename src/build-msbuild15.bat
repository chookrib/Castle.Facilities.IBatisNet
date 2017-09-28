rem ***********************************************************************************************
rem Download BuildTools from https://www.visualstudio.com/downloads/ and install first.
rem ***********************************************************************************************
rem x86 build command
rem "C:\Program Files (x86)\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin\MSBuild.exe" .\Castle.Facilities.IBatisNet.sln /t:Rebuild /p:Configuration=Release /p:WarningLevel=0
rem x64 build command
"C:\Program Files (x86)\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin\amd64\MSBuild.exe" .\Castle.Facilities.IBatisNet.sln /t:Rebuild /p:Configuration=Release /p:WarningLevel=0
pause