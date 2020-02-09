# Programozási környezetek és technológiák bemutatása

Ebben a dolgozatban programozási környezetek és technológiák bemutatására törekedtem, entrópia számítás algoritmusokon keresztül, időrendben bemutatva az egyes alkalmazásokat, egy fiktív cég segítségével. A dolgozat egy program lehetséges életútját járja be.

Cél a technológiák és környezetek egy szélesebb körének feltérképezése, program tervezési szempontokat figyelembe véve.

## Bevezető megjegyzések

A dolgozatban visszatekintünk régebbi technológiákra is. Tesszük ezt azért, mert példaként vizsgálhatjuk meg őket és segítségükkel következtetéseket vonhatunk le, akár úgy is, hogy párhuzamot vonunk egyes mai technológiákkal. Fontos látnunk a múltból legalább azokat a részeket, amik elvezettek a jelenhez, hogy minél pontosabb következtetéseket vonhassunk le a jövőt illetően. 

A dolgozatnak viszont nem célja teljes és pontos képet festeni egyes technológiákról és azok életútjáról, csupán annyi szerepük van, hogy felhívják a figyelmet arra, hogy esetenként milyen fontos lehet ezek ismerete is. Természetesen itt szoftveres példák kerülnek bemutatásra, de a hardveres technológia fejlődése egy-egy nagyobb projket megtervezésekor szintén fontos tényező.

A példák azért C# nyelven keresztül vannak bemutatva, mert ez egy nagyon könnyen tanulható, aktuális technológia. 

>Megjegyzés: Egyszerű elemi algoritmusok bemutatására, amikor a cél csupán az algoritmus működésének vizsgálata és megértése, az egyik legcélszerűbb nyelv a Pascal, ami gyakorlatilag angol nyelvű pszeudokód.

A C# azért is alkalmas a példaprogramok bemutatására, mert segítségével könnyen megérthető technológiai problémákat tudunk felvázolni és ezt a tapasztalatot felhasználva képesek lehetünk más területeken is kamatoztatni.

## Megoldandó probléma

Annak bemutatása, hogyan lehetséges a lehető legkevesebb anyagi ráfordítással, a már megírt kódok újrafelhasználásával egy választott kezdeti programozási környezetet továbbfejleszteni, az újonnan megjelenő technológiákat figyelembe véve. Másképpen fogalmazva, egy fejlesztő cég, hogyan képes a piacon maradni hosszú távon, tudva, hogy ezen a területen a technológiai fejlődés követése kisebb fejlesztő cégek esetében igen nehéz. Itt több probléma is felmerül. Az egyik fontos probléma a fejlesztők szakmai tudásának gyors elavulása, ami továbbképzéssel orvosolható. Egy másik probléma az eszközpark gyors elavulása. Ez a két probléma végső soron anyagi terhet jelent a cégeknek. Itt rossz döntés lehet a cég szempontjából, ha ezeken ésszerűtlenül próbál spórolni. A legnagyobb problémát mégsem ezek jelentik. A legnagyobb gond a felgyorsult világban az azonnali reagálás lehetősége. Ma már minden szoftverfejlesztő cég igyekszik minél jobban elmozdulni az agilis fejlesztés irányába. Ez pontosan annak a következménye, hogy szükséges a gyors reakció a piacon maradás esélyének növeléséhez. A dolgozat pontosan ezt próbálja körbe járni, miközben igyekszik bemutatni a felmerülő problémákra olyan megoldásokat, melyeket a gyakorlatban is lehet használni, éppen ezért a dolgozat gerincét a teszt vezérelt fejlesztés fogja adni.

## A probléma részletezése

Ahhoz, hogy be tudjam mutatni viszonylag élethűen technológiák és környezetek egy szélesebb körét, program tervezési szempontokat figyelembe véve, létrehozok egy életszerű történetet egy fiktív cégről. A cég neve a történet szempontjából lényegtelen, ezért legyen a neve: Inessential Kft.<sup class="footnote-ref"><a href="#fn1" id="fnref1">[1]</a></sup>

A cégünk története induljon a 2000-es évek elejétől, mégpedig egy garázscégből. Mivel a helyes megoldások bemutatására törekszem, ezért az Inessential Kft. fejlesztői tisztában vannak a szükséges programfejlesztési technológiákkal, így a projekt a szükséges igény leírásokkal és tervekkel készül el.

> **Megjegyzés:** A projektek nem minden esetben készülnek el a megfelelő minőségben. Mivel egy program tervezőnek azzal is tisztában kell lennie, hogyan lehet egy félresiklott projektet megpróbálni visszarántani a helyes útra ezért ezt két külön példaprogramban elemezzük. Ez egy fontos probléma! Nagyon sok futó projekt van, ami refaktorálásra szorul és az ilyen projekteket rendbe rakni a legtöbb esetben igen nagy kihívást jelent. Ezeket a problémákat járjuk körben az adott példaprogramokban.<sup class="footnote-ref"><a href="#fn2" id="fnref2">[2]</a></sup>

### A probléma kialakulása

A 80-as évek végén és a 90-es évek elején több programozási nyelv közül is választhattunk volna, egy új projekt elkezdésekor. Két nagy irányvonal létezett ekkor, a C és a Pascal. Ha a C vonalat választjuk, akkor leginkább a C, C++, Java jöhetett volna szóba. A C és a C++ nyelvek erőforrás igényes számításokhoz kitűnő választás. A számításigényes játékok ezért készültek C vagy C++ motorral. A Pascal viszont egy nagyon könnyen tanulható nyelv. Nem erőforrás igényes alkalmazásokhoz ideális választásnak tűnt. Ha a platformfüggetlenség cél, akkor egyértelmű választásnak tűnhetett a Java.

Amennyiben a hosszú távú következményeit nézzük annak, hogy egy projekt milyen nyelven készült el, akkor a nem megfelelő döntéseknek hosszú távon igen komoly következményei lehetnek.

Tegyük fel, hogy gyorsan szeretnénk lefejleszteni egy alkalmazást a 80-as évek végén, DOS operációs rendszerre. Nem erőforrás igényes és egyáltalán nem cél a több platform. Ebben az időben az volt a jellemző, hogy adott operációs rendszerre készítettek valamilyen cél szoftvert. A platform függetlenség így nem igazán jön szóba. Az eszköz függetelenség ekkor még főként azt jelenti, hogy milyen PC-n akarjuk futtatni a programot. Nincsenek pda-k, tabletek, okostelefonok, okostévék stb..  Kiválasztjuk a Pascalt. Ha van pénzünk, akkor a Borland Pascal mellett döntünk. A Win95 megjelenése a PC világában egyértelműen új irányvonalat adott az informatika egyes területeinek, mint például az irodai szoftveres eszközkészletek. A parancssoros programok átköltöztetése ablakos környezetbe felhasználóbarátabbá tették a programokat. 1995-ben ha egy Pascalban megírt parancssoros vagy grafikus alkalmazást az új technológiai irányvonalnak megfelelően ablakos környezetbe akartunk volna költöztetni, akkor egyértelmű választásnak tűnhetett az 1995-ben megjelent Borland Delphi. Ekkor a Microsoft még nem rendelkezett a Visula-Studio - C# párossal. Logikus döntésnek tűnhetett a Borland Delphi. Fontos észrevenni, hogy 1995-ben ez tűnhetett egy logikus döntésnek, de 2000 júliusától már minimum megfontolandó lett volna a C# nyelv választása. Persze ekkor a szoftver teljes újraírására lett volna szükség, de sok esetben az ablakos, esemény vezérelt szemlélet miatt a Pascal programot is a legtöbbször 0-ról írták újra. Amennyiben a kód még spagetti kód is volt, akkor amúgy se volt más választás, mint a teljes újraírás. Van olyan jellegű tapasztalatom, hogy a 90-es évek közepén megírt Pascal programot 97-ben az akkor megjelent Borland Delphi 3-as verziójába teljesen újraírták. Én 2000-ben csatlakoztam a fejlesztő csapathoz és ekkor még mindig voltak olyan utómunkák, amikor a régi Pascal kódból a paradox és egyéb fájl alapú adatbázisok üzleti logikáit kellett átemelni.

Egy programtervező fejlesztőnek fontos látnia, hogy hosszú távon egy-egy döntés mit okoz vagy okozhat. A 2000-es évek első felében egyértelművé vált, hogy a C# egyre több területen felülmúlja a Delphi képességeit (pl. a Delphi 2009-ig nem támogatta az UTF8-at, a C# 2001-től igen). Ezen kívül Redmondba nem csak azzal voltak tisztában, hogy az éppen aktuális operációs rendszernek milyen képességei vannak, de azzal is tisztában voltak, hogy milyen irányban folynak a fejlesztések. Így a Delphi 2000-től, annak ellenére, hogy egy kitűnő Pascal alapú nyelv volt, amiben nagyon könnyen és gyorsan lehetett ablakos szoftvereket fejleszteni Windows alá és rengeteg támogatással rendelkezett, mégis egyre nagyobb hátrányba került a Microsoft C# nyelvéhez képest. Ezt a Borland is észrevette és a kétezres évek közepétől már kétségessé vált a folytatás, míg végül 2006-ban bejelentették, hogy leállnak a Delphi fejlesztésével. Nagyon fontos! 2006-ban aki új projekthez a Delphi-t választotta, annak vagy nagyon alapos oka volt rá, vagy nagyon nem vette figyelembe a technológiai irányvonalakat és ezzel rossz útra terelt egy projektet. Ennek ellenére természetesen a Delphi ma is él és virul, hiszen a kétezres évek közepére már annyi Pascal alapú szoftver készült el, hogy a Delphi fejlesztését tovább kellett vinni. Jelenleg az Embarcadero tulajdonában és gondozásában van a Delphi.

Ezeket a problémákat, amikor még nem látható egy technológiáról, hogy hosszú távon érdemes-e használni, csak azzal lehet részben kivédeni, hogy figyelemmel kísérjük a technológiai fejlődéseket, irányvonalakat (egyes fejlesztéseknél a hardveres technológiai fejlődés figyelemmel kísérése is rendkívül fontos) és megpróbálunk helyes következtetéseket levonni a kialakult helyzetekről, figyelembe véve az összes aktuális és múltbeli valós tényeket az adott technológiákkal kapcsolatban.

Lássunk egy konkrét példát, ami egyértelműen óriási, nehezen vagy egyáltalán nem előrelátható problémát okozhat egy adott technológia hosszú távú használata esetén:
- [Microsoft Silverlight](https://www.microsoft.com/silverlight/). Egy technológia ami ígéretesnek indult, sok projekt fejlesztése kezdődött el benne, de az ötödik verzió után a Microsoft leállította a fejlesztését. Ha kiválasztunk egy technológiát és abban elkészítünk egy szoftvert, akkor a legkevésbé szeretnénk azt látni, amit jelenleg a Silverlight hivatalos oldalán lehet olvasni: "Prepare for Silverlight 5 end of support after October 2021.". Ha egy kiválasztott technológia támogatása megszűnik, akkor hosszú távon egyre nagyobb problémát jelent azon szoftverek fejlesztése melyek az adott technológia felhasználásával készültek el. A technológiát a HTML5 megjelenése kényszerítette térdre. A HTML5 első bejelentett vázlata (2007) jóval a Silverlight megjelenése elött volt (2011). 2014 előtt egy Silverlight projekt elindításakor minimum átgondolandó volt a HTML5 megjelenésének bevárása, 2014 után, ahogy azt most már utólag tudjuk is, nem egy jó döntés volt a Silverlight választása. Egy programtervezőnek ilyen esetekben a jövőbe kell látnia a jelent és a múltat tanulmányozva. Az ilyen rossz irányba elindított projektek éppen úgy növelik a szoftver válságot, mint a nem jól megírt, nehezen továbbfejleszthető, sok karbantartást igénylő szoftverek.
 
Nézzünk meg egy sokkal aktuálisabb példát:
- Ha én egy szerver-kliens alapú szolgáltatás orientált projektet szeretnék elkészíteni Windows alapokon és nem követem a technológiai irányvonalakat és aktuális szakmai híreket az adott témában, akkor elképzelhető, hogy WCF technológiát választok. Ha viszont követem, akkor tisztában vagyok azzal, hogy 2019 május 6.án [Scott Hunter](https://devblogs.microsoft.com/dotnet/author/scott-h/) a Visula-Studio és a .NET programmenedzsment igazgatója arra hívta fel a figyelmet, hogy a jövőbeni WCF rendszerek helyett az ASP.NET Core Web API-kat vagy a gRPC-t javasolja, mert a .NET Core rendszerre már nem portolják a teljes WCF rendszert.<sup class="footnote-ref"><a href="#fn3" id="fnref3">[3]</a></sup> Ilyen körülmények között a WCF választása meggondolandó. Ha valaki ilyen projektet szeretne elindítani, akkor mindenképpen tájékozódnia kell, hogy meghozhassa a megfelelő hosszú távra is alkalmas döntést.

A fentieket figyelembe véve, most már látható az, hogy miért tűnhetett a 2000-es évek elején úgy, hogy Windows rendszerre fejlesztett célalkalmazáshoz C# nyelvet válasszunk. Figyelembe kell vegyük, hogy ekkor még nem voltak a C# nyelvel kapcsolatban olyan tapasztalatok, mint később a Silverlight. Ezeket fontos kiemelni és fontos építeni ilyen tudásokra új projektek bevezetésekor. Természetesen ez nem C# probléma, ez csak egy példa. Ilyen probléma volt előtte is, ahogy a Delphi esetén láthattuk. Az én meglátásom az, hogy a szoftver válság egyik oka pont az, hogy ilyen problémák túl sűrűn fordulnak elő. Ezt a problémát csak erősíti, hogy zárt forráskódú, rendszerekre építkezik úgy sok projekt, hogy annak támogatottsága a zárt forráskód miatt csak az adott forráskód tulajdonosán múlik. Ugyanakkor azt is látni kell, hogy egy technológiát nem érdemes fenttartani akkor, ha az elavulttá válik. Ezeket a problémákat a nyílt forráskód se tudja orvosolni, de még a szabványosítás se.

>Megjegyzés: Tudni kell azt, hogy egyes programozási nyelvek mire alkalmasak és mire nem. A C# létjogosultsága egyáltalán nincs megkérdőjelezve. A C# és .NET keretrendszernek jelenleg nincs igazán komoly ellenfele például Windows rendszerekben irodai, vagy bármilyen más nem erőforrásigényes ablakos alkalmazások fejlesztésére, hosszú távon is. Számos olyan fejlesztésben vettem és veszek részt, melyek kifejezetten Windows rendszerre készülő célalkalmazások. Ezeknél a projekteknél rendszerint a C# és .NET tűnik a legjobb választásnak. Ugyanakkor más területeken, mint például egy webáruház, jelenleg nem valószínű, hogy C# alapú technológia a legjobb választás. Ez persze köztudott, hogy a problémához választunk eszközt (programozási nyelvet és egyéb technológiákat) és nem az eszközökhöz keresünk problémákat, ha csak nem az a cél, hogy feltérképezzük az adott technológia határait.

Térjünk vissza a történetünkhöz.

A 2000-es évek elején az Inessential Kft. készített egy sikeres szoftvert, amit értékesített. Az internet hajnalán a szoftverek értékesítése jellemzően CD lemezeken történt. Tételezzük fel továbbá azt, hogy a szoftver elkészülte után a fejlesztők többségét költségcsökkentés címén elbocsátották és csak annyi fejlesztőt hagytak meg amennyi az aktuálisan felmerülő problémák, jellemzően hibák, megoldásához éppen elegendő. Ez a példa nem is annyira kirívó.

Mi történik a továbbiakban?

Amennyiben a szoftver valóban sikeres, akkor új hozzá hasonló szoftverek fognak megjelenni a piacon, de mivel ők későbben kezdtek bele a fejlesztésekbe, ezért új technológiákra építhetnek. Az Inessential Kft. szoftvere csakhamar elavulttá válik és a konkurenciák kiszoríthatják a piacról.

Ez a példa megfelel a problémák életszerűbbé tételére.

### A szolgáltatás

A példa szempontjából a szolgáltatás tartalma nem fontos, ám mivel működő alkalmazásokon szeretném bemutatni a példát, ezért felhasználom az entrópia számító programomat, melyet 2017.11.23.-án készítettem a „Bevezetés az informatikába” című tárgy keretein belül, szorgalmi feladatként. A szolgáltatás lényege, hogy egy program számára átadunk egy szöveges fájlt, ami a kapott szövegen entrópia elemzéseket végez, majd az eredményeket kiírja egy fájlba vagy megjeleníti.

Első lépésként tekintsük meg a szolgáltatásunk magját adó entrópia számítást:

**Definíció:** Az _A1, A2, ... ,An_ jeleket rendre _p1, p2, ... ,pn_ valószínűségekkel kibocsátó adó (rendszer), ahol
> p1 + p2 + ... + pn = 1 és 0 <= pi <= 1 (i=1, 2, ..., n)
átlagos infromációját a valószínűségekkel súlyozott középértékekkel jellemezhetjük, vagyis
> H(p1, p2, ... ,pn) = Sum(n, i=1) pi log pi
amit a rendszer bizonytalanságának, határozatlanságának vagy entrópiájának is nevezünk.<sup class="footnote-ref"><a href="#fn4" id="fnref4">[4]</a></sup>

A példa nem túl bonyolult, így a bonyolultsága nem vonja el a fókuszt a vázolandó problémákról.

## A példa történeti áttekintője

 - 2000-es évek eleje. Az első sikeres alkalmazás piacra dobása. Első verzió: Product\MAF.Entropy.Console
 - Felhasználói igény merül fel cserélhető logikára. Második verzió: Product\MAF.Entropy.Console2
 - 2000-es évek közepe. Piaci igény merül fel a cserélhető felületre. Harmadik verzió: Product\MAF.Entropy.WPF
 - 2000-es évek második fele. Piaci igény merül fel szolgáltatás orientációra. Negyedik verzió: Product\MAF.Entropy.

## Felhasznált eszközök

Bármilyen fejlesztésbe is fogjunk bele, az első dolgunk az lesz, hogy eldöntjük, milyen eszközöket akarunk felhasználni. A felhasználni kívánt programozási nyelv vagy nyelvek és az azokhoz választott fejlesztőeszközökön kívül szükségünk lehet további eszközökre is. Ebben a fejezetben áttekintjük, hogy milyen eszközöket fogunk felhasználni ezeknél a projekteknél.

### Verziókövető

A forrásokat és egyéb erőforrás fájlokat már évtizedek óta verziókövető rendszerekben tartunk. Ennek fontosságára nem térek ki, ma már minden fejlesztő tudja, hogy e nélkül nem szabad elkezdeni fejleszteni. Az, hogy milyen verziókövető rendszereket használunk az szintén egy eldöntendő kérdés.

>Megjegyzés: Természetesen a GitHub verziókezelőt fogom használni, hiszen itt vannak a projektek bemutatva, de fontos lehet egy fejlesztőnek legalább azt tudnia, hogy régebbi projektek esetén milyen verziókezelőkkel találkozhatnak még.

Lássuk, hogy a 2000-es évek elején milyen lehetőségeink voltak, a teljesség igénye nélkül.

#### [Microsoft Visual SourceSafe](https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-6.0/ms950420(v=msdn.10)?redirectedfrom=MSDN)

A Microsoft verziókövető rendszere volt. Ma már nem támogatják. Eredetileg nem saját fejlesztés volt, hanem egy Észak-karolinai cég által fejlesztett verziókövető rendszer volt, melyet a Microsoft felvásárolt. A felvásárlás után a Microsoft leállította a saját verziókövető rendszerének fejlesztését, melynek neve [Microsoft Delta](https://winworldpc.com/product/microsoft-delta/10).

A 2002 és 2006 között volt alkalmam a Visual SourceSafe verziókezelőt használni a napi munkám során. Delphi kódok verziókezelésére használtuk. 

Mivel a támogatása megszűnt, így ezzel az eszközzel nem foglalkozunk tovább. 

>Megjegyzés: Ismét láthattunk két példát olyan fejlesztésekre, melyeket ma már nem támogatnak. Egy verziókövető rendszerről átállni egy másikra nem okoz olyan nagy problémát, de a fejlesztésükbe beleölt idő valamint az eszközöket használó sok ezer cég átállásba fektetett ideje összességében már nem kevés. Nagyon sok ilyen projekt volt, van és lesz. Ezeknek a munkaóráknak a száma még akkor is a szoftverválságot erősíti, ha maguk a projektek valójában nyereségesek voltak.

#### Concurrent Versions System [(CVS)](http://savannah.nongnu.org/project/memberlist.php?detailed=1&group=cvs)

Egy a 90-es években népszerű verziókezelő volt. Ma már nem fejlesztik, legutóbbi stabil kiadása 2005-ben volt, ennek ellenére még 2011-ben is javítottak hibát benne. A CVS fontos mérföldkő volt a verziókezelők történetében, számos más verziókezelő alapjaként szolgált, mint például az SVN. 

Mivel nem fejlesztik jelenleg, így őt is elavult technológiának tekinthetjük, ráadásul számos hibája volt, melyeket az SVN-ben javítottak.

#### Apache Subversion [(SVN)](http://subversion.apache.org/)

A 2000-es években kezdte el fejleszteni a CollabNet. Nagy népszerűségnek örvendett, számos kliens készült hozzá, mint például a [TortoiseSVN](https://tortoisesvn.net/). A mai napig is sokan használják, én is több mint 10 évig használtam. A GitHub megjelenése óta egyre kevesebb projekthez használják verziókezelőként.

#### [Git](https://git-scm.com/)

A verziókezelőt [Linus Torvalds](https://en.wikipedia.org/wiki/Linus_Torvalds) hozta létre 2005-ben. Linux kernelek fejlesztéséhez használták. A Git ingyenes és nyílt forráskódú rendszer. Számos verziókezelőt építettek Git alapokra, többek között a GitHub verziókezelőt is.

#### [GitHub](https://github.com/)

A GitHub egy Git alapú verziókezelő rendszer. A verziókezelésen kívül más szolgáltatásokat is nyújt, mint például projekt kezelés, ahol megajánlott vagy magunk által készített egyedi kanbantáblát készíthetünk a projekt kezelésére. 

A GitHub 2008 februárjában indult el. 2018-ban a Microsoft felvásárolta. Jelenleg a világ legnagyobb kódgyűjteményét tartalmazza.

A projektek elkészítéséhez nem csak a verziókezelő szoltáltatást fogom használni. Igyekszem kihasználni és bemutatni a GihHub jelenlegi szolgáltatásait. Ezt egy külön mellékletben teszem meg.

## Megoldások

Az egyes megoldásokat és a hozzájuk tartozó magyarázatokat, technológiai elemzéseket lásd a fent felsorolt verziók hivatkozásainál.

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
 <li id="fn2" class="footnote-item"><p>Refaktorálás példaprogramok:</p><p> - https://github.com/malbertHE/EKECodingDodjo/blob/master/Principle/SRP/Hanoi/Docs/Description.md</p><p> - https://github.com/malbertHE/EKECodingDodjo/tree/master/Principle/OCP/Shapes<a href="#fnref2" class="footnote-backref"> ^</a></p></li>
 <li id="fn3"  class="footnote-item"><p>Scott Hunter 2019 május 6.-ai bejelentése: https://devblogs.microsoft.com/dotnet/net-core-is-the-future-of-net/<a href="#fnref3" class="footnote-backref"> ^</a></p></li>
 <li id="fn4"  class="footnote-item"><p>Definíció forrása: http://aries.ektf.hu/~birocs/docs/bevinf.pdf. <a href="#fnref4" class="footnote-backref"> ^</a></p></li>
</ol>
</section>

## Irodalomjegyzék
|1| Dr. Kovács Emőd, Bíró Csaba, Dr. Perge Imre: Bevezetés az informatikába, Eger, 2013, Eszterházy Károly Főiskola-Matematikai és Informatikai Intézet
