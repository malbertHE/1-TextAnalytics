# Programozási környezetek és technológiák bemutatása

Ebben a dolgozatban programozási környezetek és technológiák bemutatására törekedtem, entrópia számítás algoritmusokon keresztül, időrendben bemutatva az egyes alkalmazásokat, egy fiktív cég segítségével. A dolgozat egy program lehetséges életútját járja be.
Cél a technológiák és környezetek egy szélesebb körének feltérképezése, program tervezési szempontokat figyelembe véve.

## Megoldandó probléma

Annak bemutatása, hogyan lehetséges a lehető legkevesebb anyagi ráfordítással, a már megírt kódok újrafelhasználásával egy választott kezdeti programozási környezetet továbbfejleszteni, az újonnan megjelenő technológiákat figyelembe véve. Másképpen fogalmazva, egy fejlesztő cég, hogyan képes a piacon maradni hosszú távon, figyelembe véve, hogy ezen a területen a technológiai fejlődés követése kisebb fejlesztő cégek esetében igen nehéz. Itt több probléma is felmerül. Az egyik fontos probléma a fejlesztők szakmai tudásának gyors elavulása, ami továbbképzéssel orvosolható. Egy másik probléma az eszközpark gyors elavulása. Ez a két probléma végső soron anyagi terhet jelent a cégeknek. Itt rossz döntés lehet a cég szempontjából, ha ezeken ésszerűtlenül próbál spórolni. A legnagyobb problémát mégsem ezek jelentik. A legnagyobb gond a felgyorsult világban az azonnali reagálás lehetősége. Ma már minden szoftverfejlesztő cég igyekszik minél jobban elmozdulni az agilis fejlesztés irányába. Ez pontosan annak a következménye, hogy szükséges a gyors reakció a piacon maradás esélyének növeléséhez. A dolgozat pontosan ezt próbálja körbe járni, miközben igyekszik bemutatni a felmerülő problémákra olyan megoldásokat, melyeket a gyakorlatban is lehet használni, éppen ezért a dolgozat gerincét a teszt vezérelt fejlesztés fogja adni.

## A probléma részletezése

Ahhoz, hogy be tudjam mutatni viszonylag élethűen technológiák és környezetek egy szélesebb körét, program tervezési szempontokat figyelembe véve, létrehozok egy életszerű történetet egy fiktív cégről. A cég neve a történet szempontjából lényegtelen, ezért legyen a neve: Inessential Kft.<sup class="footnote-ref"><a href="#fn1" id="fnref1">[1]</a></sup>

A cégünk története induljon a 90-es évek elejétől, mégpedig egy garázscégből. Mivel a helyes megoldások bemutatására törekszem, ezért az Inessential Kft. fejlesztői tisztában vannak a szükséges programfejlesztési technológiákkal, így a projekt a szükséges igény leírásokkal és tervekkel készül el.

> **Megjegyzés:** A projektek nem minden esetben készülnek el a megfelelő minőségben. Mivel egy program tervezőnek azzal is tisztában kell lennie, hogyan lehet egy félresiklott projektet megpróbálni visszarántani a helyes útra ezért ezt két külön példaprogramban elemezzük. Ez egy fontos probléma! Nagyon sok futó projekt van, ami refaktorálásra szorul és az ilyen projekteket rendbe rakni a legtöbb esetben igen nagy kihívást jelent. Ezeket a problémákat járjuk körben az adott példaprogramokban.<sup class="footnote-ref"><a href="#fn2" id="fnref2">[2]</a></sup>

### A probléma kialakulása

A 90-es évek elején az Inessential Kft. készített egy sikeres szoftvert, amit értékesített. Az internet hajnalán a szoftverek értékesítése jellemzően CD lemezeken történt. Tételezzük fel továbbá azt, hogy a szoftver elkészülte után a fejlesztők többségét költségcsökkentés címén elbocsátották és csak annyi fejlesztőt hagytak meg amennyi az aktuálisan felmerülő problémák, jellemzően hibák, megoldásához éppen elegendő. Ez a példa nem is annyira kirívó.

Mi történik a továbbiakban?

Amennyiben a szoftver valóban sikeres, akkor új hozzá hasonló szoftverek fognak megjelenni a piacon, de mivel ők későbben kezdtek bele a fejlesztésekbe, ezért új technológiákra építhetnek. Az Inessential Kft. szoftvere csakhamar elavulttá válik és a konkurenciák kiszoríthatják a piacról.

Ez a példa megfelel a problémák életszerűbbé tételére.

### A szolgáltatás

A példa szempontjából a szolgáltatás tartalma nem fontos, ám mivel működő alkalmazásokon szeretném bemutatni a példát, ezért felhasználom az entrópia számító programomat, melyet 2017.11.23.-án készítettem a „Bevezetés az informatikába” című tárgy keretein belül, szorgalmi feladatként. A szolgáltatás lényege, hogy egy program számára átadunk egy szöveges fájlt, ami a kapott szövegen entrópia elemzéseket végez, majd az eredményeket kiírja egy fájlba vagy megjeleníti.

Első lépésként tekintsük meg a szolgáltatásunk magját adó entrópia számítást:

**Definíció:** Az _A1, A2, ... ,An_ jeleket rendre _p1, p2, ... ,pn_ valószínűséggekkel kibocsátó adó (rendszer), ahol
> p1 + p2 + ... + pn = 1 és 0 <= pi <= 1 (i=1, 2, ..., n)
átlagos infromációját a valószínűségekkel súlyozott középértékekkel jellemezhetjük, vagyis
> H(p1, p2, ... ,pn) = Sum(n, i=1) pi log pi
amit a rendszer bizonytalanságának, határozatlanságának vagy entrópiájának is nevezünk.<sup class="footnote-ref"><a href="#fn3" id="fnref3">[3]</a></sup>

A példa nem túl bonyolult, így a bonyolultsága nem vonja el a fókuszt a vázolandó problémákról.

## A példa történeti áttekintője

 - 90-es évek eleje. Az első sikeres alkalmazás piacra dobása. Első verzió: Product\MAF.Entropy.Console
 - 2000-es évek eleje. Felhasználói igény merül fel cserélhető logikára. Második verzió: Product\MAF.Entropy.Console2
 - 2000-es évek közepe. Piaci igény merül fel a cserélhető felületre. Harmadik verzió: Product\MAF.Entropy.WPF
 - 2000-es évek második fele. Piaci igény merül fel szolgáltatás orientációra. Negyedik verzió: Product\MAF.Entropy.
 
## Megoldások

Az egyes megoldásokat és a hozzájuk tartozó magyarázatokat, technológiai ellemzéseket lásd a fent felsorolt verziók hivatkozásainál.

## Konklúzió

Sokféle utat járhattunk volna be, nem csak a fent felvázoltat. A lényeg az, hogy a problémákat időben észlelni kell és azokra a megfelelő lépéseket meg kell lépni, de ami a legfontosabb:
> Az újonnan megjelenő technológiákat figyelemmel kell kísérni és megfelelően kell reagálni a megjelenésükre a lehető leghamarabb!
Ma már akkor reagáltunk időben, egy új minket érintő technológiára, ha még a megjelenése előtt el kezdünk vele foglalkozni.
Egy program tervező akkor képes jól tervezni, ha tisztába van a technológiai lehetőségeivel és korlátaival, sőt képes következtetni a technológiai fejlődés irányvonalát illetően.

### Miért alakult ki a kezdeti probléma?
Tehetnék fel a kérdést! Miért nem előztük meg még időben?
A fenti példákkal sikerült rávilágítani a technológiák és környezetek fontosságára. A problémák hosszú távon sok esetben pont abból adódnak, hogy technológiai hátrányba kerülünk, nem figyeljük az új technológiákat, nem tartunk lépést a világgal. Ilyen esetekben szinte törvényszerű a lemaradás. Sikeres lehetnék-e én ma, ha megterveznék egy színes katódsugaras tévét és elkezdeném gyártani? A vállalkozásom kudarcra lenne ítélve. Pedig ha 50-60 évvel ezelőtt tettem volna meg ugyanezt, akkor a vállalkozásom biztosan sikeres lett volna.

Ez a szoftver tervezésre is vonatkozik. Ha elavult rendszerekre tervezünk, akkor a szolgáltatásunk hosszú távon biztosan nem lesz sikeres.

<hr class="footnotes-sep">
<section class="footnotes">
<ol class="footnotes-list">
  <li id="fn1"  class="footnote-item"><p>A választott cég név az Igazságügyi Minisztérium keresője szerint nem létezik. Kereső: https://www.e-cegjegyzek.hu/?cegkereses . Adott válasz az Inessential cégnévre: "2020. január 23. napján 13 óra 52 perckor a megadott feltételekkel a cégnyilvántartásban nincs nyilvántartott adat."<a href="#fnref1" class="footnote-backref"> ^</a></p></li>
 <li id="fn2" class="footnote-item"><p>A két példaprogram:</p><p> - https://github.com/malbertHE/EKECodingDodjo/blob/master/Principle/SRP/Hanoi/Docs/Description.md</p><p> - https://github.com/malbertHE/EKECodingDodjo/tree/master/Principle/OCP/Shapes<a href="#fnref2" class="footnote-backref"> ^</a></p></li>   
 <li id="fn3"  class="footnote-item"><p>Definíció forrása: http://aries.ektf.hu/~birocs/docs/bevinf.pdf. <a href="#fnref3" class="footnote-backref"> ^</a></p></li>
</ol>
</section>

## Irodalomjegyzék
|1| Dr. Kovács Emőd, Bíró Csaba, Dr. Perge Imre: Bevezetés az informatikába, Eger, 2013, Eszterházy Károly Főiskola-Matematikai és Informatikai Intézet
