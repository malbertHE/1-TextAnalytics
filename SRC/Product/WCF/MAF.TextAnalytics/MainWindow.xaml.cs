/*! \page project3 Szolgáltatás orientált programozás előadás beadandó: WCF szöveg ellemző alkalmazás.
 * 
 * Oktató: Dr. Király Sándor
 * 
 * \version   1.0
 * \date      2018.12.05
 * 
 * \section wcf1 Dokumentáció részei
 *    - \subpage wcfUserDocumentation "Felhasználói dokumentáció"
 *    - \subpage wcfOperatorDocumentation "Üzemeltetői dokumentáció"
 *    - \subpage wcfDevelopmentDocumentation "Fejlesztői dokumentáció"
 *
 */

//-----------------------------------------------------------

/*! \page wcfUserDocumentation Felhasználói dokumentáció
 * 
 * A program segítségével szöveges dokumentumokról lehet statisztikai adatokat lekérdezni. Ilyen statisztikai információk például, hogy hány darab karakter, betű, szó, mondat szerepel a dokumentumban.
 * 
 * \bug	A program szöveg feldolgozás közben nem aktív!
 * \bug	Jelenleg maximum csak 47104 bájlt méretű szöveges fáljl feldolgozására képes a program!
 * 
 * \section wcf2 A program fő ablaka
 * \image html wcf0.png "Kezdő képernyő."
 * \image latex wcf0.png "Kezdő képernyő."
 * 
 * A program 3 funkciót tartalmaz:
 *	- Bejelentkezés, ill. bejelentekzett felhasználó esetén kijelentkezés.
 *	- Ellenőrzés
 *	- Feldolgozás
 *	
 * \section wcf3 Bejelentkezés/Kijelentkezés
 * A bejelentkezés gomb aktiválása után a következő képernyő jelenik meg:
 * \image html wcf1.png "Bejelentkezési képernyő."
 * \image latex wcf1.png "Bejelentkezési képernyő."
 *	Itt a felhasznló login nevét és jelszavát megadva van lehetőségünk a rendszerbe bejelentkezni.
 *	Amennyiben még nincs regisztrált felhaszhnálónk, akkor a Regisztrálok feliratra kattintva lehet a regisztrációt elkezdeni.
 *	A gombra kattintva megjelenik a regisztrációs ablak, ahol megadhatjuk a szükséges adatokat.
 * \image html wcf2.png "Regisztrációs képernyő."
 * \image latex wcf2.png "Regisztrációs képernyő."
 * Sikeres regisztráció esetén visszakerülünk a bejelnetkezési ablakra, sikertelen regisztráció esetén a megfelelő hibaüzenet jelenik meg, például:
 * \image html wcf3.png "Jelszó nem azonos hibaüzenet."
 * \image latex wcf3.png "Jelszó nem azonos hibaüzenet."
 * 
 * Sikeres bejelentkezés esetén a következő üdvözlő ablak jelenik meg:
 * \image html wcf4.png "Jelszó nem azonos hibaüzenet."
 * \image latex wcf4.png "Jelszó nem azonos hibaüzenet."
 * Ezen kívül a bejelentkezés gomb átvált kijelentkezésre, amivel a kijelentkezést végezhetjük el.
 * A program bezárása esetén a kijelentkeztetés automatikus.
 * 
 * \section wcf4 Ellenőrzés
 * 
 * Az ellenőrzés funkció arra szolgál, hogy leelenőrizzük, hogy egy adott szöveges állományt már feldolgoztak-e vagy sem.
 * A funkció aktiválásakor megjelenő ablak:
 * \image html wcf5.png "Ellenőrzés indítása képernyő."
 * \image latex wcf5.png "Ellenőrzés indítása képernyő."
 * Ha a megadott fájl nem létezik vagy még nem dolgozták fel, akkor ennek megfelelő üzenet jelenik meg az ablakon.
 * Amennyiben már egyszer valaki feldolgozta, akkor betöltődik a feldolgozás eredménye:
 * \image html wcf6.png "Eredmény képernyő."
 * \image latex wcf6.png "Eredmény képernyő."
 * 
 * Itt lehetőségünk van kiválasztani egy rekordot és azon kétszer kattintva annak tételei jelennek meg:
 * \image html wcf7.png "Eredmény részletek képernyő."
 * \image latex wcf7.png "Eredmény részletek képernyő."
 * A vissza gomb segítségével visszajutunk az előbbi képernyőre.
 * 
 * A továbbiakban az ellenőrzés gomb ezt az ablakot fogja megjeleníteni, mindaddig amíg az Új ellenőrzés gombra nem kattintunk. Ekkor ismét feljön az ablak, ahol megadhatjuk az ellenőrizendő fájlt.
 * 
 * Ehhez a funkcióhoz nem szükséges bejelentkezve lennünk.
 * 
 * \section wcf5 Feldolgozás
 * Feldolgozás funkciónál lehetőségünk van megadni a feldolgozandó fájlt. Ekkor a fájl megléte és mérete kerül ellenőrzésre. Ha megfelelnek, akkor a rendszer megnézi, hogy a felhasználó bejelentkezett-e már. Amennyiben igen, akkor a feldolgozás végrehajtásra kerül és a következő üzenet jelenik meg:
 * \image html wcf8.png "Sikeres feldolgozás képernyő."
 * \image latex wcf8.png "Sikeres feldolgozás képernyő."
 *
*/

//-----------------------------------------------------------

/*! \page wcfOperatorDocumentation Üzemeltetői dokumentáció
 * 
 * A program a következő részekből épül fel:
 *  - Szerver oldali kiszolgáló - WCFTextAnalyticsService.dll
 *  - Szerver oldali host alkalmazás - TextAnalzticsServiceHost.exe
 *  - Kliens oldali alkalmazás - TextAnalytics.exe
 *  
 * \section wcf6 Szerver oldali kiszolgáló
 * 
 * Az alkalmazás a következő részekből épül fel:
 *	- MAF.Collection.dll - Közös gyűjteménykód.
 *	- MAF.Entropy.dll - Entrópia és egyéb szövegelemzést végző kód.
 *	- MAF.TextAnalytics.Server.dll - Kiszolgáló szerver és adatbázis szervert kapcsolatot megvalósító kód.
 *		- MAF.TextAnalytics.Server.dll.config - Konfigurációs állomány.
 *	- MAF.TextAnalyticsSQLite.dll - SQLite adatbázis kapcsolatot megvalósító kód.
 *		- MAF.TextAnalyticsSQLite.dll.config - Konfigurációs állomány.
 *	- MAF.TextAnalyticsMySQL.dll - MySQL adatbázis kapcsolatot megvalósító kód.
 *		- MAF.TextAnalyticsMySQL.dll.config - Konfigurációs állomány.
 *	- WCFTextAnalyticsService.dll - Kliens kiszolgáló.
 *	- System.Data.SQLite.dll - SQLite adatbázis motor.
 *		- System.Data.SQLite.xml - Konfigurációs állomány.
 *	- MySQL.Data.dll - MySQL szerver kapcsolatot megvalósító kód.
 *	- Logic mappa - a feldolgozások logikai leíróinak fájljait tartalmazza.
 *		- ecl.xml - feldolgozás logikai leíró.
 *	- TextAnalzticsServiceHost.exe - A host alkalmazás.
 *		- TextAnalzticsServiceHost.exe.config - Konfigurációs állomány.
 *		
 * Az alkalmazást vezérlő fontosabb paraméterek:
 *	- Adatbázis típusának megadása a DBType paraméterrel lehetséges, melyet a MAF.TextAnalytics.Server.dll.config fájlban adhatunk meg. Megadható értékek:
 *		- NA - XML fál. Alapértelmezett. Csak teszteléshez ajánlott.
 *		- SQLite - Egyszerű, telepítést nem igénylő SQL alapú adatbázis, arra az esetre, ha nincs elérhető SQL szerver.
 *		- MySQL - MySQL szerver elérés esetére.
 *		- Oracle - Oracle szerver elérés esetére. (Fejlesztés alatt!)
 *		
 * A TextAnalzticsServiceHost.exe.config konfigurációs állományban megadható többek között a szolgáltatást nyújtó szerver címe és portja.
 *
 * \pre       A program Windows 10 operációs rendszerre készült.
 * További függőségek:
 *        - .NET Framework 4.6.1
 *        
 * \copyright MAF - GNU Public License.
 * 
*/

//-----------------------------------------------------------

/*! \page wcfDevelopmentDocumentation Fejlesztői dokumentáció
 * 
 * Fejlesztés alatt ...
 * 
*/


using MAF.Collection;
using System;
using System.IO;
using System.ServiceModel;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;
using MAF.TextAnalytics.TextAnalyticsServiceReference;

namespace MAF.TextAnalytics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			if (!Directory.Exists(c_ResultFilePath))
				Directory.CreateDirectory(c_ResultFilePath);
		}

		const string c_logout = "Kijelentkezés";
		const string c_ResultFilePath = "ResultFiles";

		UserRegistrationPage urp = null;
		PageLogin pl = null;
        CheckPage cp = null;
		CheckPage cp2 = null;
		InfoPage ipError;
		InfoPage ipMessage;
		ITextAnalyticsService client;
		ItemsPage items = null;
		DataPage dp = null;
		object MainFrameContent;
		string token = string.Empty;

		void Window_Loaded(object sender, RoutedEventArgs e)
		{
			ipError = new InfoPage(InfoErrorOK);
			ipMessage = new InfoPage(InfoMessageOK);
			ipMessage.SetBackground();
			try
			{
				client = new TextAnalyticsServiceClient();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Hiba!");
				Environment.Exit(1);
			}
			bLogin.IsEnabled = true;
			bCheck.IsEnabled = true;
		}


        #region Felhasználó login és regisztráció

        private void bLogin_Click(object sender, RoutedEventArgs e)
		{
			if (bLogin.Content.ToString() == c_logout)
			{
				lblUserStatus.Text = string.Empty;
                try
                {
                    client.LogOut(token);
                    token = string.Empty;
					WriteInfoMessage("Sikeres kijelentkezés!");
				}
                catch(Exception ex)
                {
					WriteInfoError($"Kijelentkezés közben a következő hiba történt:{ex.Message}");
                }
                bLogin.Content = "Bejelentkezés";
				return;
			}

			if (pl == null)
				pl = new PageLogin(LoginOK, LoginCancel, UserRegistration);
			else
				pl.Visibility = Visibility.Visible;
            MainFrame.Visibility = Visibility.Visible;
            MainFrame.Content = pl;
		}

		void LoginOK(string pLoginName, string pPassword)
		{
			try
			{
				token = client.SignIn(pLoginName, pPassword);
			}
			catch (Exception ex)
			{
				WriteInfoError($"Bejelentkezés közben a következő hiba történt:{ex.Message}");
				return;
			}
			if (token == string.Empty)
			{
				WriteInfoError("Sikertelen bejelentkezés!");
				return;
			}
			WriteInfoMessage("Sikeres bejelentkezés!");			
			lblUserStatus.Text = $"Bejelentkezett felhasználó: {pLoginName}.";
			bLogin.Content = c_logout;
		}

		void LoginCancel()
        {
            MainFrame.Visibility = Visibility.Hidden;
        }

        void UserRegistration()
		{
            if (urp == null)
                urp = new UserRegistrationPage(RegsitrationOK, RegistrationCancel);
            else
                urp.Visibility = Visibility.Visible;
            MainFrame.Visibility = Visibility.Visible;
            MainFrame.Content = urp;    
		}

        void RegistrationCancel()
        {
            MainFrame.Content = pl;
            MainFrame.Visibility = Visibility.Visible;
        }

        void RegsitrationOK(string pLoginName, string pName, string pPassword, string pPassword2)
        {
			if (pPassword != pPassword2)
			{
				WriteInfoError("A két jelszó nem egyezik meg.");
				return;
			}
            try
            {
                client.SignUp(pLoginName, pName, pPassword);
                MainFrame.Content = pl;
                MainFrame.Visibility = Visibility.Visible;
            }
            catch (FaultException<FaultTextAnalytics> ex)
            {
				WriteInfoError($"Regisztráció nem lehetséges. {ex.Detail.ErrorText}");
            }
            catch (Exception ex)
            {
				WriteInfoError($"Regisztráció közben a következő hiba történt:{ex.Message}");
            }
        }

		#endregion


		#region Fájl ellenőrés

		void bCheck_Click(object sender, RoutedEventArgs e)
        {
			if (dp != null)
			{
				MainFrame.Visibility = Visibility.Visible;
				MainFrame.Content = dp;
				return;
			}

			if (cp == null)
                cp = new CheckPage(CheckOK, CheckCancel);
            else
                cp.Visibility = Visibility.Visible;
            MainFrame.Visibility = Visibility.Visible;
            MainFrame.Content = cp;
        }

        void CheckOK(string pFile)
        {
			try
			{
				string downloadedFile = DownloadFile(pFile);
				ViewData(downloadedFile);
			}
			catch (Exception ex)
			{
				WriteInfoError($"Ellenőrzés közben a következő hiba történt: {ex.Message}");
			}
		}

		void CheckCancel()
		{
			MainFrame.Visibility = Visibility.Hidden;
		}

		void ViewData(string pDownloadedFile)
		{
			if (pDownloadedFile == string.Empty)
				return;

			if (dp == null)
				dp = new DataPage(pDownloadedFile, NewCheck, ViewItem);

			MainFrame.Visibility = Visibility.Visible;
			MainFrame.Content = dp;
		}

		void ViewItem(XElement pItem)
		{
			if (items == null)
				items = new ItemsPage(pItem, ViewItemOKAction);
			else
				items.Refresh(pItem);

			MainFrame.Visibility = Visibility.Visible;
			MainFrame.Content = items;
		}

		void ViewItemOKAction()
		{
			MainFrame.Visibility = Visibility.Visible;
			MainFrame.Content = dp;
		}

		void NewCheck()
		{
			MainFrame.Visibility = Visibility.Visible;
			MainFrame.Content = cp;
		}

		#endregion


		#region Fájl feldolgozás.

		void bRunAnalitics_Click(object sender, RoutedEventArgs e)
		{
			if (cp2 == null)
				cp2 = new CheckPage(RunCalculationOK, CheckCancel);
			else
				cp2.Visibility = Visibility.Visible;
			MainFrame.Visibility = Visibility.Visible;
			MainFrame.Content = cp2;
		}

		void RunCalculationOK(string pFile)
		{
			try
			{
				if (RunCalculation(pFile))
					WriteInfoMessage("A feldolgozás sikeresen megtörént!");
			}
			catch (Exception ex)
			{
				WriteInfoError($"Feldolgozás közben a következő hiba történt: {ex.Message}");
			}
		}

		bool RunCalculation(string pFile)
		{
			if (pFile == string.Empty)
			{
				WriteInfoError("Kötelező megadni fájlt!");
				return false;
			}

			if (!File.Exists(pFile))
			{
				WriteInfoError("A megadott fájl nem létezik!");
				return false;
			}

			const long maxFileSize = 1024 * 46;//1024 * 10;

			FileInfo fi = new FileInfo(pFile);
			if (fi.Length > maxFileSize)
			{
				WriteInfoError(string.Concat("A fájl túl nagy! Megengedett maximális fájlméret: ", maxFileSize, ".",
					Environment.NewLine, "Megadott fájl mérete: ", fi.Length, ".", Environment.NewLine,
					"Mert béna vagyok és egyenlőre még nem tudom miért nem tud nagyobb méretet átküldeni.", Environment.NewLine,
					"Azt mondják, a tudatlanság boldoggá tesz. Engem ku...ra idegesít!", Environment.NewLine,
					"Ez nem jött be:", Environment.NewLine,
					"maxBufferPoolSize=2147483647 maxBufferSize=2147483647 maxReceivedMessageSize=2147483647"));
				return false;
			}

			FileStream fs = null;
			FileDownloadReturnMessage fdrm;
			try
			{
				fs = File.Open(pFile, FileMode.Open);
				FileUploadMessage fum = new FileUploadMessage(Path.GetFileName(pFile), token, fs);
				try
				{
					WriteInfoMessage("A feldolgozás folyamatban ...!");
					fdrm = client.RunCalculation(fum); //token
					return true;
				}
				catch (FaultException<FaultTextAnalytics> ex)
				{
					WriteInfoError($"A fájl feldolgozása a következő hibával állt le: { ex.Detail.ErrorText }.");
					return false;
				}
			}
			finally
			{
				if (fs != null)
					fs.Close();
			}
		}

		#endregion


		#region Fájl letöltések.

		private string DownloadFile(string pFile)
		{
			if (pFile == string.Empty)
			{
				WriteInfoError("Kötelező megadni fájlt!");
				return string.Empty;
			}

			if (!File.Exists(pFile))
			{
				WriteInfoError("A megadott fájl nem létezik!");
				return string.Empty;
			}

			string md5 = Cryptography.FileMD5Calculator(pFile);
			if (!client.SourceFileExist(md5))
			{
				WriteInfoError("A megadott fájl még nem került feldolgozásra.");
				return string.Empty;
			}

			FileDownloadReturnMessage fdrm = null;
			try
			{
				fdrm = client.GetResultFile(new FileDownloadMessage(md5));
			}
			catch (FaultException<FaultTextAnalytics> ex)
			{
				WriteInfoError($"Az eredmény fájl letöltése a következő hibával állt le: { ex.Detail.ErrorText }.");
				return string.Empty;
			}

			string resultFile = Path.Combine(c_ResultFilePath, $"{Path.GetFileNameWithoutExtension(fdrm.DownloadedFilename)}2.xml");
			StreamFunc.StreamToFile(fdrm.FileByteStream, resultFile);
			return resultFile;
		}

		#endregion


		#region Információs ablak

		void WriteInfoError(string pMessage)
		{
			WriteInfo(pMessage, ipError);
		}

		void WriteInfoMessage(string pMessage)
		{
			WriteInfo(pMessage, ipMessage);
		}

		void WriteInfo(string pMessage, InfoPage pInfoPage)
		{
			MainFrameContent = MainFrame.Content;
			MainFrame.Visibility = Visibility.Visible;
			MainFrame.Content = pInfoPage;
			pInfoPage.tbInfo.Text = pMessage;
		}

		void InfoMessageOK()
		{
			MainFrame.Visibility = Visibility.Hidden;
		}

		void InfoErrorOK()
		{
			MainFrame.Content = MainFrameContent;
		}

		#endregion

	}
}
