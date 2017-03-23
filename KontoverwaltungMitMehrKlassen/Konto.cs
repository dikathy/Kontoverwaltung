using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KontoverwaltungMitMehrKlassen
{
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
                    if (tempBetrag == 0)
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
}
