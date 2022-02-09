# Rendszerjavaslat

Általános javaslatok:
- Konzolos alkalmazás.
- Bemenő paraméterben megadva az adatfolyam.
- A feldolgozás paraméterei egy xml paraméter fájlban vannak. Ez vezérli a feldolgozást.

Rendszer javaslat diagram:

![rendszer javaslat](useCase1.png)


A rendszer komponensei:
- [Entropy.exe](../SRC/Product/MAF.Entropy.Console): futtatható állomány. A felület valósítja meg.
- [MAF.Entropy.dll](../SRC/Library/MAF.Entropy): Üzleti logikát megvalósító könyvtár.

![rendszer komponens javaslat](rs1.png)

Adatfolyam javaslatok:
- A bemeneti adatfolyam egy megadott szöveges állomány.
- A kimeneti eredmények xml állományba kerülnek.

