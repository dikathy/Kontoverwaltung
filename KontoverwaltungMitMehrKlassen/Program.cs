using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KontoverwaltungMitMehrKlassen
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Konto> bestehendeKonten = new List<Konto>();
            List<Inhaber> bestehendeKunden = new List<Inhaber>();
            List<int> kontonummern = new List<int>();
            List<int> kreditnummern = new List<int>();
            List<int> kundennummern = new List<int>();

            Console.WriteLine("Willkommen! Bitte wählen Sie eine Funktion aus:");
            Console.WriteLine("[A] Anlegen");
            Console.WriteLine("[B] Buchen");
            Console.WriteLine("[D] Daten anzeigen");
            Console.WriteLine("[X] Beenden");

            var enteredKey = Console.ReadKey();

            if (enteredKey.Key == ConsoleKey.A)
            {
                //Bankkonto oder Kredit
                Console.WriteLine("Was möchten Sie anlegen?");
                Console.WriteLine("[B] Bankkonto");
                Console.WriteLine("[K] Kredit");
                Console.WriteLine("[X] Zurück");
                enteredKey = Console.ReadKey();

                if (enteredKey.Key == ConsoleKey.B)
                {
                    Console.WriteLine("Für wen soll ein neues Bankkonto angelegt werden?");
                    Console.WriteLine("[B] Bestandskunde");
                    Console.WriteLine("[N] Neukunde");
                    Console.WriteLine("[X] Zurück");
                    enteredKey = Console.ReadKey();

                    if (enteredKey.Key == ConsoleKey.B)
                    {
                        Console.WriteLine("Bitte die Kundennummer angeben:");
                        int kundennummer;
                        var validKundennummer = int.TryParse(Console.ReadLine(), out kundennummer);
                        while (!validKundennummer)
                        {
                            Console.WriteLine("Das ist keine gültige Kundennummer. Bitte erneut versuchen.");
                            var input = Console.ReadLine();
                            if (input.ToString() != "Cancel")
                            {
                                break;
                            }
                            validKundennummer = int.TryParse(input, out kundennummer);
                            validKundennummer = kundennummern.Contains(kundennummer);
                        }
                        foreach (Inhaber inhaber in bestehendeKunden)
                        {
                            if (inhaber.Kundennummer == kundennummer)
                            {
                                Console.WriteLine("Neues Konto für folgenden Kunden anlegen: " + inhaber.Vorname + inhaber.Nachname + "?");
                                Console.WriteLine("[J] Ja");
                                Console.WriteLine("[N] Nein");
                                var answer = Console.ReadKey();
                                if (answer.Key == ConsoleKey.J)
                                {
                                    var kontonummer = kontonummern.Max() + 1;
                                    kontonummern.Add(kontonummer);
                                    bestehendeKonten.Add(new Konto(inhaber, kontonummer));
                                    Console.WriteLine("Neues Konto mit der Kontonummer " + kontonummer + "angelegt.");
                                }
                                else if (answer.Key == ConsoleKey.N)
                                {
                                    return;
                                }
                            }
                        }
                    }
                    else if(enteredKey.Key == ConsoleKey.N)
                    {
                        Inhaber inhaber = new Inhaber();
                        int alter;

                        Console.WriteLine("Bankkonto für Neukunden einrichten.");
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
                            Console.WriteLine("Kein gültiges Alter eingegeben");
                            validAlter = int.TryParse(Console.ReadLine(), out alter);
                            if (alter > 125)
                            {
                                validAlter = false;
                            }
                        }
                        inhaber.Alter = alter;                        
                        inhaber.Kundennummer = kundennummern.Max() + 1;
                        kundennummern.Add(inhaber.Kundennummer);
                        bestehendeKunden.Add(inhaber);                        
                        Konto konto = new Konto(inhaber, kontonummern.Max() + 1);
                        kontonummern.Add(konto.Kontonummer);
                        bestehendeKonten.Add(konto);
                        Console.WriteLine("Neuer Kunde und neues Konto wurden erfolgreich angelegt.");
                        Console.WriteLine("Kundennummer: " + inhaber.Kundennummer);
                        Console.WriteLine("Kontonummer: " + konto.Kontonummer);
                        Console.ReadKey();
                    }
                }
            }
            else if (enteredKey.Key == ConsoleKey.B)
            {
                //Abbuchung oder Gutschrift
            }
            else if (enteredKey.Key == ConsoleKey.D)
            {
                Console.WriteLine("Datenanzeige");
                Console.WriteLine("Bitte die Nummer des Kontos angeben:", "Mit 'Cancel' kehren Sie in das Hauptmenü zurück");
                int kontonummer;
                var validKontonummer = int.TryParse(Console.ReadLine(), out kontonummer);
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
            else if(enteredKey.Key == ConsoleKey.X)
            {
                return;
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
            set { _Kontostand += value; }
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

        private List<Kredit> _Kredite;

        public Kredit Kredite
        {
            set { _Kredite.Add(value) ; }
        }


        public void DatenAnzeigen()
        {
            double kreditsumme = 0;
            Console.WriteLine("Kontodaten:");
            Console.WriteLine("Inhaber: " + _Inhaber.Vorname + _Inhaber.Nachname);
            Console.WriteLine("Kontostand: " + _Kontostand);
            foreach (Kredit kredit in _Kredite)
            {
                kreditsumme += kredit.Kreditsumme;
            }
            var realgeld = _Kontostand - kreditsumme;
            Console.WriteLine("Realgeld: " + realgeld);
        }
    }

    class Kredit
    {
        private int _Kreditnummer;

        public int Kreditnummer
        {
            get { return _Kreditnummer; }
            set { _Kreditnummer = value; }
        }

        private Konto _Konto;

        public Konto Konto
        {
            get { return _Konto; }
            set { _Konto = value; }
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
