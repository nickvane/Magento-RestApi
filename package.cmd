%windir%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe Magento.RestApi.sln /t:Clean,Rebuild /p:Configuration=Release /fileLogger

if not exist NuGet\lib mkdir NuGet\lib
if not exist NuGet\lib\net45 mkdir NuGet\lib\net45

copy bin\Magento.RestApi.dll NuGet\lib\net45
copy bin\Magento.RestApi.xml NuGet\lib\net45

tools\nuget.exe update -self
tools\nuget.exe pack NuGet\Magento.RestApi.nuspec -BasePath NuGet -Output NuGet