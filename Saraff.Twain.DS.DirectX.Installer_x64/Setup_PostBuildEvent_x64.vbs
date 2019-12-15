Option Explicit
rem -----------------------------------------------------------
rem Setup_PostBuildEvent_x64.vbs
rem 
rem Patch an msi with the 64bit version of InstallUtilLib.dll 
rem to allow x64 built managed CustomActions.
rem -----------------------------------------------------------    

Const msiOpenDatabaseModeTransact = 1
Const msiViewModifyAssign         = 3

rem path to the 64bit version of InstallUtilLib.dll
Const INSTALL_UTIL_LIB_PATH = "C:\Windows\Microsoft.NET\Framework64\v2.0.50727\InstallUtilLib.dll"

Dim installer : Set installer = Wscript.CreateObject("WindowsInstaller.Installer")

Dim sqlQuery : sqlQuery = "SELECT `Name`, `Data` FROM Binary"

Dim database
Set database = installer.OpenDatabase(Wscript.Arguments(0), msiOpenDatabaseModeTransact)
Dim view : Set view = database.OpenView(sqlQuery)

Dim record : Set record = installer.CreateRecord(2)
record.StringData(1) = "InstallUtil"
view.Execute record

record.SetStream 2, INSTALL_UTIL_LIB_PATH

view.Modify msiViewModifyAssign, record
database.Commit

Set view = Nothing
Set database = Nothing
