@echo off
echo ========================================
echo Cleaning Port 3000 and Starting Dev Server
echo ========================================
echo.

echo Killing processes on port 3000...
for /f "tokens=5" %%a in ('netstat -ano ^| findstr :3000 ^| findstr LISTENING') do (
    echo Killing process %%a
    taskkill /F /PID %%a >nul 2>&1
)

timeout /t 2 /nobreak >nul

echo Cleaning .next directory...
rmdir /s /q .next >nul 2>&1

echo.
echo Starting development server...
echo ========================================
npm run dev
