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
