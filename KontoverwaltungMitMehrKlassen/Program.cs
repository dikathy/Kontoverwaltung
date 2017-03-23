using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KontoverwaltungMitMehrKlassen
{
    class Program
    {
        static List<Konto> bestehendeKonten = new List<Konto>();
        static List<Inhaber> bestehendeKunden = new List<Inhaber>();
        static List<int> vergebeneKontonummern = new List<int>();
        static List<int> vergebeneKreditnummern = new List<int>();
        static List<int> vergebeneKundennummern = new List<int>();

        static void Main(string[] args)
        {
            vergebeneKontonummern.Add(0);
            vergebeneKreditnummern.Add(0);

            Console.WriteLine("Willkommen! Bitte wählen Sie eine Funktion aus:");
            Console.WriteLine("[A] Anlegen");
            Console.WriteLine("[B] Buchen");
            Console.WriteLine("[D] Daten anzeigen");
            Console.WriteLine("[X] Beenden");

            var enteredKey = Console.ReadKey();
            while (true)
            {
                if (enteredKey.Key == ConsoleKey.A)
                {
                    //Bankkonto oder Kredit
                    Console.WriteLine("\nWas möchten Sie anlegen?");
                    Console.WriteLine("[B] Bankkonto");
                    Console.WriteLine("[K] Kredit");
                    Console.WriteLine("[X] Zurück");
                    enteredKey = Console.ReadKey();

                    if (enteredKey.Key == ConsoleKey.B)
                    {
                        Console.WriteLine("\nFür wen soll ein neues Bankkonto angelegt werden?");
                        Console.WriteLine("[B] Bestandskunde");
                        Console.WriteLine("[N] Neukunde");
                        Console.WriteLine("[X] Zurück");
                        enteredKey = Console.ReadKey();

                        if (enteredKey.Key == ConsoleKey.B)
                        {
                            if (vergebeneKundennummern.Count != 0)
                            {
                                Console.WriteLine("\nBitte die Kundennummer angeben:" + "\n(Mit 'Cancel' kann die Aktion abgebrochen werden)");
                                int kundennummerInput;
                                var input = Console.ReadLine();
                                if (input.ToString() == "Cancel")
                                {
                                    break;
                                }
                                var validKundennummer = int.TryParse(input, out kundennummerInput);
                                if (validKundennummer)
                                {
                                    validKundennummer = vergebeneKundennummern.Contains(kundennummerInput);
                                }
                                while (!validKundennummer)
                                {
                                    Console.WriteLine("\nDas ist keine gültige Kundennummer. Bitte erneut versuchen.");
                                    input = Console.ReadLine();
                                    if (input.ToString() == "Cancel")
                                    {
                                        break;
                                    }
                                    validKundennummer = int.TryParse(input, out kundennummerInput);
                                    validKundennummer = vergebeneKundennummern.Contains(kundennummerInput);
                                }
                                foreach (Inhaber inhaber in bestehendeKunden)
                                {
                                    if (inhaber.Kundennummer == kundennummerInput)
                                    {
                                        Console.WriteLine("\nNeues Konto für folgenden Kunden anlegen: " + inhaber.Vorname + " " + inhaber.Nachname + "?");
                                        Console.WriteLine("[J] Ja");
                                        Console.WriteLine("[N] Nein");
                                        var answer = Console.ReadKey();
                                        if (answer.Key == ConsoleKey.J)
                                        {
                                            var kontonummer = vergebeneKontonummern.Max() + 1;
                                            vergebeneKontonummern.Add(kontonummer);
                                            bestehendeKonten.Add(new Konto(inhaber, kontonummer));
                                            Console.WriteLine("\nNeues Konto mit der Kontonummer " + kontonummer + " angelegt.");
                                        }
                                        else if (answer.Key == ConsoleKey.N)
                                        {
                                            Console.WriteLine("\nAktion abgebrochen.");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nEs sind noch keine Kunden angelegt.");
                                Console.WriteLine("Drücken Sie eine beliebige Taste, in das Hauptmenü zurückzukehren.");
                                Console.ReadKey();
                            }                            
                        }
                        else if (enteredKey.Key == ConsoleKey.N)
                        {
                            Inhaber inhaber = new Inhaber();
                            int alter;

                            Console.WriteLine("\nBankkonto für Neukunden einrichten.");
                            Console.WriteLine("Nachname?");
                            inhaber.Nachname = Console.ReadLine();
                            Console.WriteLine("Vorname?");
                            inhaber.Vorname = Console.ReadLine();
                            Console.WriteLine("Alter?");
                            var validAlter = int.TryParse(Console.ReadLine(), out alter);
                            if (alter > 120)
                            {
                                validAlter = false;
                            }
                            while (!validAlter)
                            {
                                Console.WriteLine("\nKein gültiges Alter eingegeben");
                                validAlter = int.TryParse(Console.ReadLine(), out alter);
                                if (alter > 125)
                                {
                                    validAlter = false;
                                }
                            }
                            inhaber.Alter = alter;
                            inhaber.Kundennummer = vergebeneKundennummern.Max() + 1;
                            vergebeneKundennummern.Add(inhaber.Kundennummer);
                            bestehendeKunden.Add(inhaber);
                            Konto konto = new Konto(inhaber, vergebeneKontonummern.Max() + 1);
                            vergebeneKontonummern.Add(konto.Kontonummer);
                            bestehendeKonten.Add(konto);
                            Console.WriteLine("\nNeuer Kunde und neues Konto wurden erfolgreich angelegt.");
                            Console.WriteLine("Kundennummer: " + inhaber.Kundennummer);
                            Console.WriteLine("Kontonummer: " + konto.Kontonummer);
                            Console.ReadKey();
                        }
                        else if (enteredKey.Key == ConsoleKey.X)
                        {
                            break;
                        }
                    }
                    else if (enteredKey.Key == ConsoleKey.K)
                    {
                        //Kredit
                        int kontonummer;
                        Console.WriteLine("Neuen Kredit anlegen.");
                        Console.WriteLine("Für welches Konto soll ein Kredit gewährt werden?" + "\n(Mit 'Cancel' kann die Aktion abgebrochen werden");
                        var input = Console.ReadLine();
                        if (input.ToString() == "Cancel")
                        {
                            break;
                        }
                        var validKontonummer = int.TryParse(input, out kontonummer);
                        if (validKontonummer)
                        {
                            validKontonummer = vergebeneKontonummern.Contains(kontonummer);
                        }
                        while (!validKontonummer)
                        {
                            Console.WriteLine("\nDas ist keine gültige Kontonummer. Bitte erneut versuchen.");
                            input = Console.ReadLine();
                            validKontonummer = int.TryParse(input, out kontonummer);
                            if (validKontonummer)
                            {
                                validKontonummer = vergebeneKontonummern.Contains(kontonummer);
                            }
                        }
                        foreach (Konto konto in bestehendeKonten)
                        {
                            if (konto.Kontonummer == kontonummer)
                            {
                                if (konto.Inhaber.Alter < 18)
                                {
                                    Console.WriteLine("\nDer Kontoinhaber ist minderjährig und darf deshalb keinen Kredit bekommen.");
                                    Console.WriteLine("Drücken Sie eine beliebige Taste, um in das Hauptmenü zurückzukehren.");
                                    Console.ReadKey();
                                    break;
                                }
                                Console.WriteLine("Kreditrahmen:");
                                double kreditrahmen;
                                var validKreditrahmen = double.TryParse(Console.ReadLine(), out kreditrahmen);
                                while (!validKreditrahmen)
                                {
                                    Console.WriteLine("\nDas ist kein gültiger Kreditrahmen! Bitte erneut versuchen.");
                                    validKreditrahmen = double.TryParse(Console.ReadLine(), out kreditrahmen);
                                }
                                Kredit kredit = new Kredit(vergebeneKreditnummern.Max() + 1, konto, kreditrahmen);
                                vergebeneKreditnummern.Add(kredit.Kreditnummer);
                                konto.Kredite = kredit;
                                Console.WriteLine("Das Konto " + konto.Kontonummer + " verfügt nun über einen zusätzlichen Kreditrahmen von " + kredit.Kreditrahmen + " Euro.");
                                Console.ReadKey();
                            }
                        }
                    }
                }
                else if (enteredKey.Key == ConsoleKey.B)
                {
                    //Abbuchung oder Gutschrift
                    Console.WriteLine("\nBuchungen");
                    Console.WriteLine("[A] Abheben");
                    Console.WriteLine("[E] Einzahlen");
                    Console.WriteLine("[X] Zurück");
                    enteredKey = Console.ReadKey();

                    if (enteredKey.Key == ConsoleKey.A)
                    {
                        int kontonummer;
                        double betrag;
                        Console.WriteLine("\nVon welchem Konto soll abgehoben werden?");
                        Console.WriteLine("(Mit 'Cancel' kann der Vorgang abgebrochen werden)");
                        var input = Console.ReadLine();
                        if (input.ToString() == "Cancel")
                        {
                            break;
                        }
                        var validKontonummer = int.TryParse(input, out kontonummer);
                        validKontonummer = vergebeneKontonummern.Contains(kontonummer);
                        while (!validKontonummer)
                        {
                            Console.WriteLine("Das ist keine gültige Kontonummer. Bitte erneut versuchen.");
                            input = Console.ReadLine();
                            if (input.ToString() == "Cancel")
                            {
                                break;
                            }
                            validKontonummer = int.TryParse(input, out kontonummer);
                            validKontonummer = vergebeneKontonummern.Contains(kontonummer);
                        }
                        Console.WriteLine("\nWelcher Betrag soll vom Konto " + kontonummer + " abgehoben werden?");
                        Console.WriteLine("(Mit 'Cancel' kann der Vorgang abgebrochen werden)");
                        input = Console.ReadLine();
                        if (input.ToString() == "Cancel")
                        {
                            break;
                        }
                        var validBetrag = double.TryParse(input, out betrag);
                        while (!validBetrag)
                        {
                            Console.WriteLine("Dies ist kein gültiger Betrag. Bitte erneut versuchen.");
                            input = Console.ReadLine();
                            if (input.ToString() == "Cancel")
                            {
                                break;
                            }
                            validBetrag = double.TryParse(input, out betrag);
                        }
                        foreach (Konto konto in bestehendeKonten)
                        {
                            if (konto.Kontonummer == kontonummer)
                            {
                                konto.GeldAbheben(betrag);
                            }
                        }
                    }
                    else if (enteredKey.Key == ConsoleKey.E) 
                    {
                        //Bareinzahlung
                        int kontonummer;
                        double betrag;
                        double kontostand = 0;
                        Console.WriteLine("\nEinzahlung");
                        Console.WriteLine("Auf welches Konto möchten Sie einzahlen?");
                        Console.WriteLine("Mit 'Cancel' kehren Sie in das Hauptmenü zurück.");
                        var input = Console.ReadLine();
                        if (input == "Cancel")
                        {
                            break;
                        }
                        var validKontonummer = int.TryParse(input, out kontonummer);
                        validKontonummer = vergebeneKontonummern.Contains(kontonummer);
                        while (!validKontonummer)
                        {
                            Console.WriteLine("Dies ist keine gültige Kontonummer. Bitte erneut versuchen.");
                            input = Console.ReadLine();
                            if (input == "Cancel")
                            {
                                break;
                            }
                            validKontonummer = int.TryParse(input, out kontonummer);
                            validKontonummer = vergebeneKontonummern.Contains(kontonummer);
                        }
                        Console.WriteLine("Welchen Betrag möchten Sie einzahlen?");
                        Console.WriteLine("Mit 'Cancel' kehren Sie in das Hauptmenü zurück.");
                        input = Console.ReadLine();
                        if (input == "Cancel")
                        {
                            break;
                        }
                        var validBetrag = double.TryParse(input, out betrag);
                        while (!validBetrag)
                        {
                            Console.WriteLine("Dies ist kein gültiger Betrag. Bitte erneut versuchen.");
                            input = Console.ReadLine();
                            if (input == "Cancel")
                            {
                                break;
                            }
                            validBetrag = double.TryParse(input, out betrag);
                        }
                        
                        foreach (Konto konto in bestehendeKonten)
                        {
                            if (konto.Kontonummer == kontonummer)
                            {
                                konto.GeldEinzahlen(betrag);
                                kontostand = konto.Kontostand;
                            }
                        }
                        Console.WriteLine("Es wurden " + betrag + " Euro auf das Konto " + kontonummer + " eingezahlt.");
                        Console.WriteLine("Ihr Kontostand beträgt nun " + kontostand + " Euro");
                        Console.ReadKey();
                    }
                    else if (enteredKey.Key == ConsoleKey.X)
                    {
                        break;
                    }

                }
                else if (enteredKey.Key == ConsoleKey.D)
                {
                    Console.WriteLine("\nDatenanzeige");
                    Console.WriteLine("Bitte die Nummer des Kontos angeben:" + "\nMit 'Cancel' kehren Sie in das Hauptmenü zurück");
                    int kontonummer;
                    string input = Console.ReadLine();
                    if (input != "Cancel")
                    {
                        var validKontonummer = int.TryParse(input, out kontonummer);
                        while (!validKontonummer)
                        {
                            Console.WriteLine("Eine Kontonummer besteht nur aus Zahlen. Bitte erneut versuchen.");
                            validKontonummer = int.TryParse(Console.ReadLine(), out kontonummer);
                        }
                        foreach (Konto konto in bestehendeKonten)
                        {
                            if (konto.Kontonummer == kontonummer)
                            {
                                konto.DatenAnzeigen();
                            }
                        }
                    }                    
                }
                else if (enteredKey.Key == ConsoleKey.X)
                {
                    return;
                }
                Console.WriteLine("Bitte wählen Sie eine Funktion aus:");
                Console.WriteLine("[A] Anlegen");
                Console.WriteLine("[B] Buchen");
                Console.WriteLine("[D] Daten anzeigen");
                Console.WriteLine("[X] Beenden");
                enteredKey = Console.ReadKey();
            }            
        }
    }



    class Inhaber
    {
        private int _Kundennummer;

        public int Kundennummer
        {
            get { return _Kundennummer; }
            set { _Kundennummer = value; }
        }

        private string _Nachname;

        public string Nachname
        {
            get { return _Nachname; }
            set { _Nachname = value; }
        }

        private string _Vorname;

        public string Vorname
        {
            get { return _Vorname; }
            set { _Vorname = value; }
        }

        private int _Alter;

        public int Alter
        {
            get { return _Alter; }
            set { _Alter = value; }
        }
    }



    class Konto
    {
        private int _Kontonummer;

        public int Kontonummer
        {
            get { return _Kontonummer; }
            set { _Kontonummer = value; }
        }

        private double _Kontostand;

        public double Kontostand
        {
            get { return _Kontostand; }
        }

        private Inhaber _Inhaber;

        public Inhaber Inhaber
        {
            get { return _Inhaber; }
        }

        public Konto(Inhaber inhaber, double kontostand)
        {
            _Inhaber = inhaber;
            _Kontostand = kontostand;
        }

        public Konto(Inhaber inhaber)
        {
            _Inhaber = inhaber;
            _Kontostand = 0;
        }

        public Konto(Inhaber inhaber, int kontonummer)
        {
            _Kontonummer = kontonummer;
            _Inhaber = inhaber;
        }

        private List<Kredit> _Kredite = new List<Kredit>();

        public Kredit Kredite
        {
            set { _Kredite.Add(value); }
        }

        public void DatenAnzeigen()
        {
            Console.WriteLine("Kontodaten:");
            Console.WriteLine("Inhaber: " + _Inhaber.Vorname + " " + _Inhaber.Nachname);
            Console.WriteLine("Kontostand: " + _Kontostand + " Euro");
            var realgeld = _Kontostand - KreditsummeAusrechnen();
            Console.WriteLine("Realgeld: " + realgeld + " Euro");
            Console.WriteLine("Kreditrahmen insgesamt: " + KreditrahmenAusrechnen() + " Euro");
        }

        public void GeldAbheben(double betrag)
        {
            var tempBetrag = betrag;
            if ((_Kontostand - betrag) > 0)
            {
                _Kontostand -= tempBetrag;
            }
            else if (KreditsummeAusrechnen() + tempBetrag < KreditrahmenAusrechnen())
            {
                foreach (Kredit kredit in _Kredite)
                {
                    if(tempBetrag == 0)
                    {
                        break;
                    }
                    if (kredit.Kreditrahmen > kredit.Kreditsumme)
                    {
                        var verfügbareSumme = kredit.Kreditrahmen - kredit.Kreditsumme;
                        if (verfügbareSumme >= tempBetrag)
                        {
                            kredit.Kreditsumme += tempBetrag;
                        }
                        else
                        {
                            tempBetrag -= verfügbareSumme;
                            kredit.Kreditsumme = kredit.Kreditrahmen;
                        }
                    }
                }
                Console.WriteLine("Sie haben " + betrag + " Euro vom Konto " + Kontonummer + " abgehoben.");
            }
            else
            {
                double verfügbareSumme = KreditrahmenAusrechnen() - KreditsummeAusrechnen();
                Console.WriteLine("Sie können den Betrag nicht abheben, da Sie nurnoch " + verfügbareSumme + " Euro zur Verfügung haben.");
            }
        }

        public void GeldEinzahlen(double betrag)
        {
            _Kontostand += betrag;
        }

        private double KreditsummeAusrechnen()
        {
            double kreditsumme = 0;
            if (_Kredite != null)
            {
                foreach (Kredit kredit in _Kredite)
                {
                    kreditsumme += kredit.Kreditsumme;
                }
            }
            return kreditsumme;
        }

        private double KreditrahmenAusrechnen()
        {
            double kreditrahmen = 0;
            if (_Kredite != null)
            {
                foreach (Kredit kredit in _Kredite)
                {
                    kreditrahmen += kredit.Kreditrahmen;
                }
            }
            return kreditrahmen;
        }
    }



    class Kredit
    {
        public Kredit(int kreditnummer,Konto konto, double kreditrahmen)
        {
            _Kreditnummer = kreditnummer;
            _Konto = konto;
            _Kreditrahmen = kreditrahmen;
        }

        private int _Kreditnummer;

        public int Kreditnummer
        {
            get { return _Kreditnummer; }
        }

        private Konto _Konto;

        public Konto Konto
        {
            get { return _Konto; }
        }

        private double _Kreditrahmen;

        public double Kreditrahmen
        {
            get { return _Kreditrahmen; }
            set
            {
                if (_Konto.Inhaber.Alter >= 18)
                {
                    _Kreditrahmen += value;
                }
                else
                {
                    Console.WriteLine("Der Inhaber ist noch nicht volljährig und darf daher keinen Kredit aufnehmen!");
                }
            }               
        }

        private double _Kreditsumme;

        public double Kreditsumme
        {
            get { return _Kreditsumme; }
            set
            {
                if (Kreditrahmen >= _Kreditsumme + value)
                {
                    _Kreditsumme += value;
                }
                else
                {
                    Console.WriteLine("Der Betrag würde den Kreditrahmen übersteigen!");
                }    
            }
        }
    }
}
