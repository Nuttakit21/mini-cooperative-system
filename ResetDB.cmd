@echo off
echo ==============================
echo RESET DATABASE & MIGRATION
echo ==============================

echo.
echo [1] Remove Migrations folder
rmdir /s /q src\MiniCoop.Infrastructure\Migrations

echo.
echo [2] Drop database
dotnet ef database drop -f ^
 -p src\MiniCoop.Infrastructure ^
 -s src\MiniCoop.Api

echo.
echo [3] Add new migration
dotnet ef migrations add InitAllTables ^
 -p src\MiniCoop.Infrastructure ^
 -s src\MiniCoop.Api

echo.
echo ==============================
echo DONE - Press F5 to run API
echo ==============================
pause
