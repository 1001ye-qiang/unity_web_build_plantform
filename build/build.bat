@echo off

Set nowdate=%date:~,10%
Set nowtime=%time%
Set path_date=%nowdate:/=-%
Set path_time=%nowtime::=-%
Set path_dir=%path_date%_%path_time%

Md "%path_dir%"


svn checkout "https://192.168.0.154/svn/ShuShan/trunk/SkyCity4.5.2" "%path_dir%"

"D:\Program Files (x86)\Unity-4.5.2\Editor\Unity.exe" -quit -batchmode -logFile "./%path_dir%.log" -projectPath %cd%/%path_dir% -executeMethod CommandBuild.BuildAndroid

@echo %cd%/%path_dir%