IF "%1"==[] (
	echo "Requires command line argument to be version to zip"
	exit /b
)

copy LitleSdkForNet\LitleSdkForNet\bin\Release\LitleSdkForNet.dll .\
copy LitleSdkForNet\LitleSdkForNet\bin\Release\LitleSdkForNet.dll.config .\
copy LitleSdkForNet\LitleSdkForNet\bin\Release\DiffieHellman.dll .\
copy LitleSdkForNet\LitleSdkForNet\bin\Release\Tamir.SharpSSH.dll .\
copy LitleSdkForNet\LitleSdkForNet\bin\Release\Org.Mentalis.Security.dll .\
"C:\Program Files\7-Zip\7z.exe" a LitleSdkForNet-%1.zip CHANGELOG LICENSE LitleSdkForNet.dll LitleSdkForNet.dll.config README.md DiffieHellman.dll Tamir.SharpSSH.dll Org.Mentalis.Security.dll
del LitleSdkForNet.dll
del LitleSdkForNet.dll.config
del DiffieHellman.dll
del Tamir.SharpSSH.dll
del Org.Mentalis.Security.dll
