﻿using System;
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
                                            var kontonummer = vergebeneKontonummern.Count + 1;
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
                            inhaber.Kundennummer = vergebeneKundennummern.Count + 1;
                            vergebeneKundennummern.Add(inhaber.Kundennummer);
                            bestehendeKunden.Add(inhaber);
                            Konto konto = new Konto(inhaber, vergebeneKontonummern.Count + 1);
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
                                Kredit kredit = new Kredit(vergebeneKreditnummern.Count + 1, konto, kreditrahmen);
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
                    //Abbuchung oder Einzahlung
                    Console.WriteLine("\nBuchungen");
                    Console.WriteLine("[A] Abheben");
                    Console.WriteLine("[E] Einzahlen");
                    Console.WriteLine("[Z] Zurück");
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
                    else if (enteredKey.Key == ConsoleKey.Z)
                    {
                        continue;
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
}
