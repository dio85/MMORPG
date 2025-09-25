# MMORPG Projekt Bevezetés
Ez a projekt egy gyakorlóprojekt online MMORPG játékokhoz (csak egy demó). A szerver C#-t, a kliens pedig Unity-t használ. A szinkronizációs módszer az állapotszinkronizáció. A kliens elküldi a koordinátákat a szervernek, a szerver pedig elmenti azokat, és továbbítja azokat a közelben lévő többi játékosnak.

## Projekt felépítése és lebonyolítása

Először is, mivel a szerver MySQL-t használ az adatok tárolására, a szervert futtató rendszeren először telepíteni kell a MySQL szolgáltatást. A telepítési módszer online megtalálható.

Az adatbázis alapértelmezett fiókja és jelszava egyaránt a root.

A build.bat fájlt a SERVER/Common/Data könyvtárban kell futtatni a JSON adatok létrehozásához a kliens és a szerver futtató könyvtárában. Ha bármilyen változás történik az Excel-táblázatban, akkor is futtatni kell ezt a bat fájlt.

A SERVER megoldás alatt található GameServer a szerverprojekt. A szervert úgy indíthatod el, hogy felépíted és futtatod. Automatikusan létrehozza az MMORPG adatbázist, és egy alapértelmezett rendszergazdai fiókot fogsz kapni (a fiók root, a jelszó pedig 1234567890).

Az MMORPG egy Unity kliensprojekt, és az Unity6000.0.56f1 verzióval nyitható meg. (Ha újabb verziót használsz, az OdinInspector bővítmény hibát jelezhet, és neked kell frissítened a bővítményt.)

## Projekt konfiguráció

Az adatbázis konfigurációja a SERVER\GameServer\Db\DbConfig.cs fájlban található, az alapértelmezett fiók és jelszó pedig egyaránt a root.

A hálózati konfiguráció a SERVER\Common\Network\NetConfig.cs fájlban található. Ha távoli szerverre szeretné telepíteni, akkor módosítania kell a ServerIpAddress értéket, és újra kell építenie a Common osztálykönyvtárat.

## SERVER - C# Szerver projekt

Használj C# hálózati API-t a keretrendszer nulláról történő felépítéséhez, használj protobuf-ot a szerver és a kliens közötti kommunikációhoz, használj MySQL-t az adatok tárolására, és használj Serilog-ot naplózó könyvtárként.

Használj Excel táblázatokat a térképadatok, a karakterattribútumok, a szörnyek eloszlása ​​a térképen, a leesési attribútumok stb. konfigurálásához, majd konvertáld ezeket JSON adatokká szerver és kliens elemzéshez.

Az AOI algoritmus segítségével optimalizálhatja az entitások közötti interakciót és észlelési logikát, és csökkentheti a szinkronizálás hálózati sávszélesség-használatát.

### Common Osztály könyvtára

Ez a könyvtár az osztálykönyvtár, amelyet a szerver és a kliens megosztott.

- A Data mappa Excel-táblázatokat tárol, amelyek tartalmazzák a készségdefiníciókat, elemdefiníciókat stb., valamint a hozzájuk tartozó konvertált JSON és generált CS fájlokat.
- Inventory néhány gyakori kódot tartalmaz a hátizsákhoz.
- Network Általános hálózati keretrendszer.
- A proto protobuf és generált cs kód.
- Tool néhány gyakori eszköz API.

## MMORPG - Unity Kliens projekt

QFramework keretrendszer, MVC architektúra használata。

Az entitásokat állapotgépek segítségével kezelik.

A játékos egy egyéni állapotszerkesztőt használ a különféle viselkedések és a viselkedések közötti átmenetek kezelésére (hasonlóan egy viselkedésfa egyszerűsített változatához).

### Tippek a játékhoz
1. A bal egérgombbal kattintás egy normál támadás, a görgővel nagyítható a látómező, a jobb gombbal lenyomva és az egér húzásával pedig elforgatható a látómező.
2. Nyomd meg a billentyűzeten a „V” billentyűt a tárgyak felvételéhez, nyomd meg az „I” billentyűt a hátizsák kinyitásához, és kattints jobb gombbal a tárgyra a használatához.
3. A "Q", "E", "R" és "T" billentyűzeten található billentyűparancsok rendre az 1., 2., 3. és 4. képességszinthez tartoznak.
4. Használd a --/ előtagot csalókódok beírásához a csevegősávba (a fióknak rendszergazdai jogosultságokkal kell rendelkeznie). Jelenleg csak egy csalókód létezik a "Level Up" funkcióhoz, például: "--/Level Up".

## Értesítés

1. Ha szerverre szeretnél telepíteni, ne felejts el .net környezetet telepíteni a szerverre.
2. Ne kattints többször a kliens gombjaira, például a bejelentkezés gombra, a játékba lépés gombra stb. Amikor először kattintasz rá, az már a szerverhez fordul. Időbeli okok miatt nincs betöltőablak vagy hasonló. Több kattintás több kérést küld, ami problémákat okozhat.
3. A kliensnek még nincs regisztrációs felhasználói felülete, és jelenleg csak az adatbázis közvetlen módosításával (az mmorpg adatbázis felhasználói táblázatában) lehet fiókokat hozzáadni.
4. Bár a feladatrendszer keretrendszere szinte teljesen elkészült a szerveren, a kliens még nem csatlakozott, így a feladatok fogadása az NPC-től valójában haszontalan.
5.Az ügyfélnek szánt anyagok kizárólag kommunikációs és tanulási célokat szolgálnak.

## Videó bemutatkozás

https://www.bilibili.com/video/BV1uNtrefEbn

## Kérdés

Ha bármilyen problémád adódik a fordítással és futtatással, vagy projekthibák merülnek fel, kérdéseket tehetsz fel a repository problémái részben.

QQ csoport：777411956

Ha nem érted a kódot, vagy bármilyen más kérdésed van, felveheted a QQ csoportot, hogy feltehesd a kérdéseidet.

## Fejlesztők

- Az Kliensért felelős fő személy：fuyouawa
- A szerverért felelős fő személy：yuyuqwq
