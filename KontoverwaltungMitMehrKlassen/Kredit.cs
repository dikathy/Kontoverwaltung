using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KontoverwaltungMitMehrKlassen
{
    class Kredit
    {
        public Kredit(int kreditnummer, Konto konto, double kreditrahmen)
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
        /// <summary>
        /// Hinweis: Kreditrahmen darf nicht überschritten werden.
        /// </summary>
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
