# Hmax
Password Cryptographic Proxy

To use Hmax with a self-signed certificate, you use Makecert.exe from the Windows Kit or .Net SDK as follows
C:\Program Files (x86)\Windows Kits\8.1\bin\x86>makecert -r -pe -ss my -sky exchange -n "CN=SubjectName" d:\certName.cer

you can replace SubjectName with any name of your choice, usually it is a domain name or an email address.

The certificate will be automatically imported to your Windows Certificate Store accessable through run-->certmgr.msc

You will find the certificate under Personal Certificates folder.

