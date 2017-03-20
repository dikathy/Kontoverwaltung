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
            int alter;
            double kontostand;
            Inhaber kunde = new Inhaber();
            Konto konto = new Konto(kunde);
            
            Console.WriteLine("Willkommen! Bitte wählen sie eine Funktion aus:");
            Console.WriteLine("[A] Anlegen");
            Console.WriteLine("[B] Buchung");
            Console.WriteLine("[K] Kredit");
            var enteredKey = Console.ReadKey();

            switch (enteredKey.KeyChar)
            {
                case 'A':
                    Console.WriteLine("[I] Inhaber anlegen");
                    Console.WriteLine("[K] Konto anlegen (Inhaber muss vorher angelegt sein)");
                    enteredKey = Console.ReadKey();

                    switch (enteredKey.KeyChar)
                    {
                        case 'I':
                            Console.WriteLine("Nachname?");
                            kunde.Nachname = Console.ReadLine();
                            Console.WriteLine("Vorname?");
                            kunde.Vorname = Console.ReadLine();
                            Console.WriteLine("Alter?");                                                        
                            var validAlter = int.TryParse(Console.ReadLine(), out alter);
                            while (!validAlter)
                            {
                                Console.WriteLine("Bitte ein gültiges Alter eingeben.");
                                validAlter = int.TryParse(Console.ReadLine(), out alter);
                            }
                            kunde.Alter = alter;
                            break;
                        case 'K':
                            Console.WriteLine("Kontostand?");
                            var validKontostand = double.TryParse(Console.ReadLine(), out kontostand);
                            while (!validKontostand)
                            {
                                Console.WriteLine("Bitte einen gültigen Wert als Kontostand angeben!");
                                validKontostand = double.TryParse(Console.ReadLine(), out kontostand);
                            }
                            konto.Kontostand = kontostand;
                            break;
                        default:
                            break;
                    }
                    break;
                case 'B':
                    Console.WriteLine("[A] Abbuchung");
                    Console.WriteLine("[G] Gutschrift");
                    break;
                case 'K':
                    Console.WriteLine("[K] Kredit Anlegen");
                    Console.WriteLine("[R] Kreditrahmen erhöhen");
                    Console.WriteLine("[S] Kreditsumme erhöhen");
                    break;
                default:
                    break;
            }

        }       
    }

    class Inhaber
    {
        public int Kundennummer
        {
            get { return 55555; }
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

        public Inhaber(string nachname, string vorname, int alter)
        {
            _Nachname = nachname;
            _Vorname = vorname;
            _Alter = alter;
        }
        public Inhaber()
        {

        }
    }

    class Konto
    {
        public int Kontonummer
        {
            get { return 100000; }
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
    }

    class Kredit
    {
        public int Kreditnummer
        {
            get { return 900000; }
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
