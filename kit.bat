IF "%1"==[] (
	echo "Requires command line argument to be version to zip"
	exit /b
)

copy LitleSdkForNet\LitleSdkForNet\bin\Release\LitleSdkForNet.dll .\
copy LitleSdkForNet\LitleSdkForNet\bin\Release\LitleSdkForNet.dll.config .\
"C:\Program Files\7-Zip\7z.exe" a LitleSdkForNet-%1.zip CHANGELOG LICENSE LitleSdkForNet.dll LitleSdkForNet.dll.config README.md
del LitleSdkForNet.dll
del LitleSdkForNet.dll.config
