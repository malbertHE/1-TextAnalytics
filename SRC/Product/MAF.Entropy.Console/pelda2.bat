chcp 852
echo Entr¢pia sz m¡t sok elv‚gz‚se a k”vetkez‹ f jlra: README.md.
Entropy.exe README.md
if errorlevel 0 (
   exit /b 0
)
echo Az entr¢pia sz m¡t s hib val le llt! Hibak¢d: %errorlevel%
exit /b %errorlevel%


