[httpd]
1. ServerRoot
2. Listen 8001
3. DocumentRoot

4. d:\apache24\bin\httpd.exe -k install
5. 启动时使用管理员身份运行，才能执行某些exe.
6. User/Group

[php]
1. download thread safe version php.
2. path
3.
# php5 support
LoadModule php5_module D:/php/php5apache2_4.dll
AddType application/x-httpd-php .php .html .htm
# configure the path to php.ini
PHPIniDir "D:/php"

4.extension_dir = "ext"
 extension=php_mbstring.dll
extension=php_openssl.dll
extension=php_svn.dll
date.timezone = Asia/Shanghai


[Base]
php 和 httpd 都要是x86 或者 x64
php_svn.dll 我拿到的时一个x86版本。

