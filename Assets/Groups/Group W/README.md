Im Folgenden finden sich TODOs und DONEs.

### TODOs
* Finished :)


### DONEs
* __DONEs Larry__
	* Scenes, Scripts Ordner in unserem Group W Ordner angelegt
	* Charaktere entsprechend der Kampfsituation plaziert
	* PlayerProperties Skript zum definieren von:
	  * Maximaler / aktueller HP
	  * möglicher Reihenpositionen / aktueller Reihenposition
	  * auswählbaren Waffen / aktueller Waffe
		* kann nun über enstprechende Tastenbelegung während Decision Phase gesetzt werden
	  * auswählbaren Ziel-Reihenpositionen / aktuell ausgewählte Ziel-Reihenposition
		* kann nun über enstprechende Tastenbelegung während Decision Phase gesetzt werden

	 * Timeranzeige (inkl. Beschreibung, in welcher Phase wir uns befinden, und was zu tun ist)
	  * x Sek. bis Entscheidungsphase vorbei.
	  * Wenn abgelaufen: Übergang zu Action Phase
	  * Auswahl der Waffe / des Ziels über entsprechende Tasten. Standard-Tastenbelegung wurde ausgetauscht.
		* Tastenbelegung (anhand Beispiel des WASD-Charakters)
		* Q = Lego
		* W = Paper
		* E = Scissors
		* R = Front row
		* F = Back row
	* Feedback für "Waffe ausgewählt" und "Ziel ausgewählt" (lokaler Gegner soll Auswahl nicht sehen können!)
		* Feedback über triggern einer entsprechenden Animation des Charakters.
			* Bug bei 'dpad oben', alle Animationen wurden sofort abgebrochen --> ersetzt durch spawnen einer dummy Waffe (Fisch), bei jeder Auswahl bekommt dieser eine neue Random Farbe, damit mehrmalige Auswahlen voneinander unterscheidbar sind
			* Farbe wird für Action Phase beibehalten :)
		* Verfügbare Animationen finden sich in Assets/Packages/LEGO/Scripts/Lego Minifig/MinifigController.cs, enum SpecialAnimation.
		* Wenn in Decision Phase:
			* ~~Waffe ausgewählt: Dance~~
			* Ziel ausgewählt: Wave
		* Wenn in Action Phase:
			* Waffe/Ziel ausgwählt: IdleImpatient
	* JSONs zum externen konfigurieren von Waffeneigenschaften
		* type
		* weakness
		* strength
		* asset
		* power
		* row
	* Schadensberechnung - Base damage der Waffe (Vorder/Hinterreihe) mal Multiplier (Stärke/Schwäche/Neutralität gegenüber Ziel)
	* Angriffsanimationen
		* Back: stehenbleiben, Fernkampfwaffe nutzen (z.B. Waffe werfen)
		* Front: zum Gegner hinlaufen
		* Update des HP-Balkens
		* HP-Balken bewegt sich mit, wenn ein Charakter läuft, aber nicht, wenn er sich umdreht oder stirbt
	* Beschränkung der Handlungsmöglichkeiten innerhalb Decision Phase
		* Target ist valide, wenn 
			* front vs. front
			* back vs. front 
			* back vs. back
			* front vs. back && front.hp <= 0;
			* && hp > 0

		* Target ist invalide, wenn
			* front vs. back
			* front vs. back && front.hp > 0;
			* hp <= 0

		* Abspielen entsprechender Animation, wenn invalide Auswahl getroffen wird
	* Wenn Charakter tot: zu Boden fallen, liegen bleiben

	* Spielende
		* Zeitablauf || alle besiegt
		* Bestimmung des Gewinner-Teams

	* Integration mit Interconnections Gruppe
	* KI player

* __DONEs Polina__
	* Methode/Funktion mit folgendem Verhalten:
		* eine Reihe (Front/Back) und einen Waffentyp (Lego/Paper/Scissors) als Parameter entgegen nehmen
			*  für entsprechende Enums, siehe PlayerProperties.cs
		    *  Assets Fernkampf + Nahkampf unterschiedlich
		* der entsprechende Charakter, an dem das Script attached ist, sollte dann (visuell) die entsprechende Waffe in die Hand gelegt bekommen
			* Waffe sollte sich bei Bewegung des Charakters mitbewegen

	* Environment
		* Zusammenbasteln einer hübschen Umgebung
			* Boden
			* Himmel / Decke
		* Objekte (z.B. Bäume, Gebäude etc.)
		* Beleuchtung
		* HP-Balken Grün/Rot
		*  Auswahl + Plazierung Assets für
			* Spiel-stats (Gesamtlaufzeit des Spiels, verstrichene Zeit, Rundennummer)
			* Infos zur aktuellen phase (Action vs. Decision vs. Spielende, wie lange ist noch Zeit für den Input in der Decision phase etc)
		*  Schwarzen Kasten entfernen und durch schönen Himmel + Umgebung ersetzen

	* Custom Animationen
		* Schlagen (für Nahkampfwaffe)
		* Werfen (für Fernkampfwaffe)
		* Sterben (langsam und qualvoll zu Boden fallen)

	* Verschönerungen
		* Anzeige "Weakness", "Neutral", "Resistence", wenn Charakter angegriffen wurde
			* in passendem Schriftstil, unsere default weiße Plan mit schwarzem Arial Text ist dafür nicht so nice

### LICENSE
* Music included in this project:
Wuxia2_Orchestra by PeriTune | http://peritune.com
Attribution 4.0 International (CC BY 4.0)
https://creativecommons.org/licenses/by/4.0/
Music promoted by https://www.chosic.com/
