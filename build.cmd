@echo off
cls

.paket\paket.bootstrapper.exe
if errorlevel 1 (
  exit /b %errorlevel%
)

.paket\paket.exe restore
if errorlevel 1 (
  exit /b %errorlevel%
)

set edu_args=edu

IF NOT EXIST build.fsx (
  .paket\paket.exe update
  packages\FAKE\tools\FAKE.exe init.fsx
)
packages\FAKE\tools\FAKE.exe build.fsx %*

if EXIST cleanup.fsx ( 
packages\FAKE\tools\FAKE.exe cleanup.fsx
)
