@echo off
echo === Copying DLL ===

set RD_BIN_SRC=.\Library\3rdparty\bin\Release
set BUD_BIN_DEST=.\Build\x64\Release

echo From:	%RD_BIN_SRC%
echo To:   %BUD_BIN_DEST%

if not exist "%BUD_BIN_DEST%" mkdir "%BUD_BIN_DEST%"

xcopy "%RD_BIN_SRC%\*.dll" "%BUD_BIN_DEST%\" /Y /I

echo All DLLs copied!