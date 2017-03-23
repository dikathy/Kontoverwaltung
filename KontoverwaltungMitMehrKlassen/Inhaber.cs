using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KontoverwaltungMitMehrKlassen
{
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
}
