@echo off
setlocal

set "ROOT=%~dp0"
set "URL=http://localhost:56846/Store/Index"
set "LOCALDB=C:\Program Files\Microsoft SQL Server\170\Tools\Binn\SqlLocalDB.exe"
set "IIS=%ProgramFiles%\IIS Express\iisexpress.exe"

if not exist "%LOCALDB%" (
    echo Cannot find SQL Server LocalDB at:
    echo %LOCALDB%
    pause
    exit /b 1
)

if not exist "%IIS%" (
    echo Cannot find IIS Express at:
    echo %IIS%
    pause
    exit /b 1
)

"%LOCALDB%" start MSSQLLocalDB
if errorlevel 1 (
    echo Could not start the existing MSSQLLocalDB instance.
    pause
    exit /b 1
)

curl.exe --silent --fail "%URL%" >nul 2>nul
if errorlevel 1 (
    start "SportStore B07 IIS Express" "%IIS%" /path:%ROOT% /port:56846 /clr:v4.0
)

for /l %%i in (1,1,15) do (
    curl.exe --silent --fail "%URL%" >nul 2>nul
    if not errorlevel 1 goto :open
    timeout /t 1 /nobreak >nul
)

echo SportStore did not return a page at %URL%
echo Keep this window open and check the SQL Server LocalDB instance.
pause
exit /b 1

:open
start "" "%URL%"
echo SportStore B07 is running at %URL%
endlocal
