chcp 852
echo Entropy program s�g�j�nak ki�rat�sa.
Entropy.exe | more
if errorlevel 0 (
   exit /b 0
)
echo A s�g� kiirat��sa nem siker�lt! Hibak�d: %errorlevel%
exit /b %errorlevel%


