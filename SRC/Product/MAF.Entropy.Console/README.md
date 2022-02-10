# Entropy.exe - Entrópia számító program.

## Program információk

- Programfájl neve: Entropy.exe
- Program verziószáma: 1.0.0.123
- Program és forrás licenc: [MIT License](/LICENSE)
- Forrás: [SRC](/SRC)
- Program: https://github.com/malbertHE/1-TextAnalytics-Console/releases/tag/1
- Minimális követelmények: Windows operációs rendszer, .Net Framework 4.7.2., 
- Telepítés: a program telepítést nem igényel.

## Programmappa tartalma

- Entropy.exe: futtatható program fájl.
- MAF.Entropy.dll: entrópia logikát tartalmazó dinamikus csatolású könyvtár.
- Entropy.exe.config: a program konfigurációs fájlja. Felhasználó által változtatható paramétereket nem tartalmaz.
- README.md: jelen, programot leíró fájl.
- pelda1.bat: Entropy.exe hívásának bemutatása.
- pelda2.bat: Entropy.exe hívásának bemutatása.
- Logic\ecl.xml: a jelek meghatározását leíró logikai fájl.

:warning: FIGYELEM! A futtatható állományok MD5 ellenörző kódját a forráshelyen ellenőrizze: [Release.md5](https://github.com/malbertHE/1-TextAnalytics-Console/releases/tag/1)

:information_source: A program induláskor ellenőrzi a README.md fájl MD5 ellenőrző összegét. Csak sértetlen README.md fájl jelenlétében indul a program.

## Program rövid leírása

A program a paraméterben megadott UTF8 szöveges fájlból többféle entrópia számítást végez.  
A számítások közötti különbségeket a jel kiválasztásának módja adja.  
A program a számítások eredményét xml fájlba teszi, a program könyvtárban található Result mappába. A fájl neve a feldolgozandó fájlnévvel azonos néven, kiegészítve az aktuális dátum idő adataival, xml kiterjesztéssel, a következő formátumba: eredetiFájlNév_YYYY_MM_DD_hh_mm_ss.xml , ahol az 'YYYY' az aktuális év, az 'MM' az aktuális hónap, 'DD' az aktuális nap, 'hh' az aktuális óra, 'mm' az aktuális perc, 'ss' az aktuális másodperc. Az eredmény fájlban tárolt adatok magyarázata az 'Entrópia számítás eredménye' fejezetben olvasható.

## Jelek kiválasztásának módjai

A következő eseteket tekintjük jeleknek és ennek megfelelően számítjuk az entrópiát:
- minden karakter egy jel;
- minden karakter, kivéve a szóközök;
- csak az alfanumerikus karaktereket tekintjük jeleknek;
- csak a numerikus karaktereket tekintjük jeleknek;
- csak betűket tekintünk jeleknek;
- csak írásjeleket tekintünk jeleknek;
- a szavakat tekintjük jeleknek;
- a mondatokat tekintjük jeleknek.

Minden egyes entrópiaszámításnál csak az éppen jelnek tekintett karakterek vagy karaktersorozatok vesznek részt a számításban.
Az egyes jelek pontos meghatározását reguláris kifejezések szabályai biztosítják, melyek a program mappa \Logic almappájában található ecl.xml fájl tartalmaz. A fájl módosítása nem megengedett, módosítás esetén a program nem lesz üzemképes.

## Entrópia számítás eredménye

Az eredmény fájl egy xml fájl, mely a következő adatokat tartalmazza:
- ShannonEntropy: entrópia értéke;
- I: információ tartalom értéke;
- Hmax: az entrópia maximuma adott jelkészletnél;
- SignCount: a jelek darabszáma;
- DifferentSignsCount: a különböző jelek darabszáma;
- Logic: a jelkészelet meghatározásának logikája;
- ItemList: az egyes jelek és a hozzá tartozó adatok.

A jelkészletet meghatározó logikát leíró adatok:
- Name: a logika megnevezése;
- Regex: a jelkészletet meghatározó reguláris kifejezés adatai;
- Trim: a reguláris kifejezéssel meghatározott jel elejéről és végéről levágandó karakterlánc, a jel pontosítás céljából.
- NoEmpty: amennyiben a reguláris kifejezés üres karakterláncot is visszaadhat, akkor ennek segítségével kizárható a jel listából.

Reguláris logikát leíró adatok:
- Pattern: a reguláris kifejezés pattern része;
- Replace: szükség esetén a reguláris csere karakterlánc;
- RegexOptions: a reguláris keresést szabályozó opciók;
- ToLower: a megtalált jeleket mind kisbetűre konvertálja.

Az egyes jelek adatai:
- Value: a jel;
- Count: a jel előfordulásának száma a szövegben;
- P: relatív gyakoriság értéke;
- I: információ mennyiség.

## Program használata

- Mivel a README.md biztonsági szempontból is fontos információt tartalmaz (eredeti forrás helye), ezért csak akkor indul a program, ha ez a fájl sértetlen. Ha másod kézből kapta a binárisokat, kérem ellenőrizze az MD5 összeget a forrás oldalon. Amennyiben ezek nem azonosak, ne használja a programot, töltse le a forrás oldalról.
- A programot paraméterek nélkül indítva kiírja a README.md fájl tartalmát, ami jelen szöveget tartalmazza, vagyis a program leírását.
- Feldolgozáshoz a programot 1 paraméterrel kell indítani, ahol a paraméter a feldolgozandó adatfájlt, eléréssel együtt kell tartalmazza. Az elérés lehet relatív és abszolút elérés. Az adatfájl UTF8 szöveget kell tartalmazzon. Pl.:  
  ```console
  entropy.exe adatFájl.txt
  ```

## Kilépési kódok

- -1: Ismeretlen hiba.
- 0: Szabályos kilépés. A program a kérést végrehajtotta, hiba nem történt.
- 1: Az információs fájl (README.md) módosult vagy hiányzik. A program így nem működik.
- 2: Információs fájl kiírása közben hiba történt.
- 3: Entrópiaszámítás indítása közben hiba történt.
- 4: Entrópia számítás egy vagy több szála hibára futott.

## Példa

- A pelda1.bat fájl a súgó kiíratás példáját tartalmazza.
- A pelda2.bat fájl egy entrópiaszámítás példáját tartalmazza.
