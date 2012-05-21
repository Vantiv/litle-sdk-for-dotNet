IF "%1"=="" (
	echo "Requires command line argument to be version to zip"
	exit /b
)

copy LitleSdkForNet\LitleSdkForNet\bin\Debug\LitleSdkForDotNet.dll .\
copy LitleSdkForNet\LitleSdkForNet\bin\Debug\LitleSdkForDotNet.dll.config .\
"c:\Program Files\WinRAR\winrar.exe" a LitleSdkForDotNet-%1.zip CHANGELOG LICENSE LitleSdkForDotNet.dll LitleSdkForDotNet.dll.config README.md
del LitleSdkForDotNet.dll
del LitleSdkForDotNet.dll.config
