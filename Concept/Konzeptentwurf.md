# Konzeptentwurf Plausibilitätsillusion

### Thema und Forschungsfrage
Wie wirkt sich plausibles Verhalten eines virtuellen Charakters (z. B. Blickkontakt, Reaktionsfähigkeit) auf die wahrgenommene Präsenz im Raum in einer VR-Anwendung aus?


### Interaktionskonzept
<img style="width: 300px;" src="./Grafik_Karlchen.png"> <br>
<sub>Konzeptgrafik Roboter von pia659</sub>

Im Rahmen dieses Projekts wird untersucht, wie sich das Verhalten virtueller Charaktere auf die Wirkung von VR-Inhalten auswirkt, insbesondere im Hinblick auf Lernprozesse und Nutzererleben. Dazu wird eine VR-Anwendung entwickelt, in der der virtuelle Roboter „Karlchen“ den Nutzer durch kurze Lerneinheiten führt.

Es werden zwei Versionen von Karlchen erstellt, die sich im Grad ihrer Verhaltensplausibilität unterscheiden:

1. Karlchen mit plausiblen Verhaltensweisen:
Diese Version interagiert aktiv mit dem Nutzer. Sie nimmt Blickkontakt auf, reagiert auf Gesten und spricht den Nutzer direkt an. Ziel ist es, eine möglichst natürliche und glaubwürdige soziale Interaktion zu simulieren.

2. Karlchen ohne plausibles Verhalten:
In dieser Variante bleibt der Roboter statisch. Er führt keine aktiven Interaktionen mit dem Nutzer durch und gibt Informationen lediglich nach einem Buttonklick weiter. Es findet kein erkennbares soziales Verhalten statt.

Die beiden Roboter wechseln sich in kurzen Sequenzen innerhalb der VR-Anwendung ab. 
Am Ende folgt eine kurze Abfrage – in Form einer Meinungsabfrage – um zu erfassen, wie sich die Unterschiede im Verhalten der Charaktere auf die wahrgenommene Präsenz im virtuellen Raum auswirken.

Das Ziel ist es, herauszufinden, ob und in welchem Ausmaß die Plausibilität virtueller Charaktere die Präsenz in einer VR-Umgebung beeinflusst.

Die Tabelle zeigt die Merkmale der beiden Roboter:


| **Merkmal**             | **Hohe Plausibilität**                                                                                                                                                                                                                                                                                                                           | **Geringe Plausibilität**                                                                                                                           |
| ----------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | --------------------------------------------------------------------------------------------------------------------------------------------------- |
| Reaktions-möglichkeiten | Der Roboter reagiert jederzeit auf Gesten oder Blickkontakt sowie Handlung/Interaktion mit ihm.<br/>Er reagiert auch auf ähnliche Dinge, die man mit dem anderen Roboter macht.<br/><br/>Je nach Abschnitt bestehen gewisse Möglichkeiten zu Handlungen/Interaktionen mit Gegenständen oder im Raum, auf die der Roboter jeweils reagieren kann. Die Anwendung wird automatisch fortgeführt, wenn der Nutzer eine Ziel oder eine Aufgabe erfüllt hat. | Die Anwendung und der Roboter werden per Button aktiviert und fortgeführt.<br/>Es bestehen keine automatischen Reaktionen.                          |
| Reaktions-verhalten     | Der Roboter spricht vorgefertigte Sätze, die sich auf die gewissen Situationen und definierten Interaktionsmöglichkeiten beziehen.<br/><br/>Der Roboter besitzt entsprechende Gestik und Bewegung (Blickkontakt, Mund und Augen bewegen sich, Winken).                                                                                           | Der Roboter spricht zufällige und oberflächliche Sätze, die nicht auf die Situation abgestimmt sind.<br/><br/>Der Roboter besitzt eintönige Gestik. |
| Raum-verhalten          | Der Roboter läuft oder schwebt durch den Raum und zeigt auf Objekte, von denen er spricht.<br/>Er spricht den Spieler direkt an.                                                                                                                                                                                                                 | Der Roboter bleibt ortsgebunden oder teleportiert sich.<br/>Er spricht vom Spieler in dritter Person.                                               |
| Präsenz                 | Der Roboter baut Beziehungen mit dem Nutzer auf, merkt sich den Verlauf der Geschehnisse und lobt ausgeführte Aktionen.                                                                                                                                                                                                                          | Der Roboter geht strikt zum nächsten Thema über.                                                                                                    |


# Zeitplan

| **Meilenstein** | **Frist**  | **Ziele & Aufgaben** |
|-------------|--------|----------|
| 1           | 30.04  | **Ziel**<br>Bestehende Grundlagen und ausgearbeitetes Konzept<br><br>**Alle**<br>- Konzept erstellen<br>- Git-Repository anlegen (AVR2025-Plausibilitaetsillusion)<br>- Erste Commits der Teammitglieder <br><br> **maikbartelsII**<br>- Zusammenfassende Präsentation erstellen|
| 2           | 07.05  | **Ziel**<br>Technische Basis und Skript der Anwendung<br><br>**Alle**<br>- Verbindung mit der Quest ermöglichen<br>- Thema und passende Interaktionen/Objekte auswählen<br>- Skript mit exakten Texten, Interaktionen und Ablauf erstellen<br>- Detailliertes Ablaufdiagramm mit den definierten Interaktionsmöglichkeiten erstellen<br><br>**RoxiBu**<br>- Projekt in Unity anlegen und grobe Szene erstellen |
| 3           | 04.06  | **Ziel**<br>Funktionstüchtige VR-Anwendung nach erstelltem Skript<br><br>**RoxiBu**<br>- Roboter 3D-Modellierung, Assets und Animationen erstellen<br>- Roboter Mimik & Gestik programmieren<br><br>**pia659**<br>- Gegenstände und Raum modellieren<br>- Gegenstände und Interaktionsmöglichkeiten programmieren<br><br>**panieldeschel**<br>- Roboter Tonspuren, Umgebungs- und Gegenstandsgeräusche erstellen & einbinden<br>- Anwendungsverlauf und -logik programmieren<br><br>**maikbartelsII**<br>- Roboter Logik, Gestik & Verhalten programmieren<br>- Szenen programmieren |
| 4           | 25.06  | **Ziel**<br>Vervollständigte und erweiterte Anwendungslogik<br><br>**Alle**<br>- Programmierung ergänzen und verbessern |
| 5           | 06.07  | **Ziel**<br>Fertiggestelltes Projekt<br><br>**Alle**<br>- Testen & verbessern<br>- Abschlusspräsentation |
