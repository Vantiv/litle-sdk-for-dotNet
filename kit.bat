IF "%1"==[] (
	echo "Requires command line argument to be version to zip"
	exit /b
)

copy LitleSdkForNet\LitleSdkForNet\bin\Debug\LitleSdkForDotNet.dll .\
copy LitleSdkForNet\LitleSdkForNet\bin\Debug\LitleSdkForDotNet.dll.config .\
"C:\Program Files\7-Zip\7z.exe" a LitleSdkForDotNet-%1.zip CHANGELOG LICENSE LitleSdkForDotNet.dll LitleSdkForDotNet.dll.config README.md
del LitleSdkForDotNet.dll
del LitleSdkForDotNet.dll.config
