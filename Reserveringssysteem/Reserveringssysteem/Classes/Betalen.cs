using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserveringssysteem.Classes
{
    internal class Betalen
    {
        public static void betaalOpties()
        {
            // De user groeten en de betaal opties laten zien.
            Console.WriteLine("Welkom op het betaalscherm.\n\n");
            Console.WriteLine("Kies hier uw betaal methode:\n");
            Console.WriteLine("Druk op \"1\" voor iDeal.\n");
            Console.WriteLine("Druk op \"2\" voor PayPal.\n");
            Console.WriteLine("Druk op \"3\" voor CreditCard.\n");
            Console.Write("Uw keuze: ");

            // aflezen welke betaalmethode de user heeft gekozen
            string betaalMethodeInput = Console.ReadLine();

            bool goedeKeuze = false;

            // checken of de user een juiste input geeft
            if (betaalMethodeInput == "1" || betaalMethodeInput == "2" || betaalMethodeInput == "3")
            {
                goedeKeuze = true;
            }

            while (!goedeKeuze)
            {
                Console.Write("Ongeldige invoer, kies A.U.B. uit het keuze menu hierboven: ");
                betaalMethodeInput = Console.ReadLine();

                // zolang de user geen goede input geeft, krijgt hij de mogelijkheid om opniew een input te geven
                if (betaalMethodeInput == "1" || betaalMethodeInput == "2" || betaalMethodeInput == "3")
                {
                    goedeKeuze = true;
                }
            }

            // de functie die overeenkomt met de userinput wordt uitgevoerd
            if (betaalMethodeInput == "1")
            {
                Console.Clear();
                iDeal();
            }
            if (betaalMethodeInput == "2")
            {
                Console.Clear();
                payPal();
            }
            if (betaalMethodeInput == "3")
            {
                Console.Clear();
                creditCard();
            }
        }

        public static void Main()
        {
            betaalOpties();
        }
        public static void iDeal()
        {
            Console.WriteLine("U heeft gekozen voor iDeal.\n");
            Console.WriteLine("Druk op het bijbehorende nummer van uw bank om die bank te kiezen.\nDruk op \"Q\" om terug te keren naar het vorige scherm.\n");
            Console.WriteLine("1:  ABN AMRO\n2:  ASN Bank\n3:  Bunq\n4:  ING\n5:  Knab\n6:  Rabobank\n7:  RegioBank\n8:  Revolut\n9:  SNS\n10: Svenska Handelsbanken\n11: Triodos Bank\n12: Van Landschot\n");
            Console.Write("Uw keuze: ");

            // user heeft gekozen voor iDeal en moet bank kiezen
            string bankInput = Console.ReadLine();

            // als de user q als input geeft gaat de user terug naar het betaalmethode scherm
            if (Qchecker(bankInput))
            {
                Console.Clear();
                betaalOpties();
            }

            bool goedeKeuze = false;

            if (bankInput == "1" || bankInput == "2" || bankInput == "3" || bankInput == "4" || bankInput == "5" || bankInput == "6" || bankInput == "7" || bankInput == "8" || bankInput == "9" || bankInput == "10" || bankInput == "11" || bankInput == "12" || Qchecker(bankInput))
            {
                goedeKeuze = true;
            }

            // user kiest bank. de user krijgt steeds opnieuw de keuze totdat hij een geldige input geeft
            while (!goedeKeuze)
            {
                Console.Write("Ongeldige invoer, kies A.U.B. uit het keuze menu hierboven: ");
                bankInput = Console.ReadLine();

                if (bankInput == "1" || bankInput == "2" || bankInput == "3" || bankInput == "4" || bankInput == "5" || bankInput == "6" || bankInput == "7" || bankInput == "8" || bankInput == "9" || bankInput == "10" || bankInput == "11" || bankInput == "12" || Qchecker(bankInput))
                {
                    goedeKeuze = true;
                }
            }

            // als de user q als input geeft gaat de user terug naar het betaalmethode scherm
            if (Qchecker(bankInput))
            {
                Console.Clear();
                betaalOpties();
            }
            // bank keuze van de user wordt gelezen. User word naar iban invoer scherm gestuurd
            if (bankInput == "1")
            {
                Console.Clear();
                ibanInvoer("ABN AMRO");
            }
            if (bankInput == "2")
            {
                Console.Clear();
                ibanInvoer("ASN Bank");
            }
            if (bankInput == "3")
            {
                Console.Clear();
                ibanInvoer("Bunq");
            }
            if (bankInput == "4")
            {
                Console.Clear();
                ibanInvoer("ING");
            }
            if (bankInput == "5")
            {
                Console.Clear();
                ibanInvoer("Knab");
            }
            if (bankInput == "6")
            {
                Console.Clear();
                ibanInvoer("Rabobank");
            }
            if (bankInput == "7")
            {
                Console.Clear();
                ibanInvoer("RegioBank");
            }
            if (bankInput == "8")
            {
                Console.Clear();
                ibanInvoer("Revolut");
            }
            if (bankInput == "9")
            {
                Console.Clear();
                ibanInvoer("SNS");
            }
            if (bankInput == "10")
            {
                Console.Clear();
                ibanInvoer("Svenska Handelsbanken");
            }
            if (bankInput == "11")
            {
                Console.Clear();
                ibanInvoer("Triodos Bank");
            }
            if (bankInput == "12")
            {
                Console.Clear();
                ibanInvoer("Van Landschot");
            }
        }
        public static void ibanInvoer(string bankKeuze)
        {
            Console.WriteLine($"U heeft gekozen voor {bankKeuze}.\n");
            Console.WriteLine("Vul hieronder uw IBAN nummer in.\n");
            Console.WriteLine("Wilt u naar het vorige scherm?\nDruk dan op \"Q\".\nLET OP: VUL HET IBAN NUMMER ZONDER SPATIES ER TUSSEN IN!");
            Console.Write("Uw IBAN nummer: ");

            // de user krijgt de optie op zijn IBAN nummer in te vullen
            string ibanInput = Console.ReadLine();
            bool ibanCorrect = false;

            // als de user q als input geeft gaat de user terug naar het betaalmethode scherm
            if (Qchecker(ibanInput))
            {
                Console.Clear();
                iDeal();
            }

            // het IBAN nummer wordt op geldigheid gecheckt
            if (ibanChecker(ibanInput))
            {
                ibanCorrect = true;
            }

            // user krijgt de mogelijkheid om opniew IBAN in te vullen totdat deze geldig is
            while (!ibanCorrect)
            {
                Console.WriteLine("\nDit is een ongeldig IBAN nummer.\nProbeer het nog een keer.\nLET OP: VUL HET IBAN NUMMER ZONDER SPATIES ER TUSSEN IN!");
                Console.Write("Uw IBAN nummer: ");
                ibanInput = Console.ReadLine();
                if (Qchecker(ibanInput))
                {
                    Console.Clear();
                    iDeal();
                }
                if (ibanChecker(ibanInput))
                {
                    ibanCorrect = true;
                }
            }

            // User moet wachtwoord invullen na dat hij een geldige IBAN heeft ingevult
            if (ibanCorrect)
            {
                Console.Write("\nVul hier uw wachtwoord van uw bank account in\nLET OP: HET WACHTWOORD MOET MINSTENS 12 KARAKTERS LANG ZIJN,1 SPECIAAL KARAKTER(!@#$%^&*), 1 HOOFDLETTER\nEN 1 CIJFER BEVATTEN\n\nUw wachtwoord: ");
                string wachtwoordInput = Console.ReadLine();
                bool wachtwoordCorrect = false;

                // De betaling is gelukt als het wachtwoord klopt. User wordt terug gestuurd naar het betaalmethode scherm
                if (wachtwoordChecker(wachtwoordInput))
                {
                    wachtwoordCorrect = true;
                    Console.WriteLine("\nDe betaling is gelukt!\nDank u wel en geniet van uw film!");
                    Console.Write("\nU kunt op \"Q\" drukken om terug te keren naar het hoofdscherm: ");
                    string exitInput = Console.ReadLine();
                    bool Qpressed = false;
                    if (Qchecker(exitInput))
                    {
                        Console.Clear();
                        betaalOpties();
                    }
                    while (!Qpressed)
                    {
                        Console.Write("\nDruk op \"Q\" om terug te keren: ");
                        exitInput = Console.ReadLine();
                        if (Qchecker(exitInput))
                        {
                            Console.Clear();
                            betaalOpties();
                        }
                    }
                }

                // wachtwoord is ongeldig. User krijgt nog 2 pogingen om wachtwoord in te vullen
                int pogingen = 2;
                while (!wachtwoordCorrect)
                {
                    Console.Write($"\nVul hier uw wachtwoord van uw bank account in\nLET OP: HET WACHTWOORD MOET MINSTENS 12 KARAKTERS LANG ZIJN, 1 SPECIAAL KARAKTER(!@#$%^&*), 1 HOOFDLETTER\nEN 1 CIJFER\n\nu heeft nog {pogingen--} pogingen\nUw wachtwoord: ");
                    wachtwoordInput = Console.ReadLine();

                    if (wachtwoordChecker(wachtwoordInput))
                    {
                        wachtwoordCorrect = true;
                        Console.WriteLine("\nDe betaling is gelukt!\nDank u wel en geniet van uw film!");
                        Console.Write("\nU kunt op \"Q\" drukken om terug te keren naar het hoofdscherm: ");
                        string exitInput = Console.ReadLine();
                        bool Qpressed = false;
                        if (Qchecker(exitInput))
                        {
                            Console.Clear();
                            betaalOpties();
                        }
                        while (!Qpressed)
                        {
                            Console.Write("\nDruk op \"Q\" om terug te keren: ");
                            exitInput = Console.ReadLine();
                            if (Qchecker(exitInput))
                            {
                                Console.Clear();
                                betaalOpties();
                            }
                        }
                    }
                    if (pogingen == 1)
                    {
                        Console.Write($"\nVul hier uw wachtwoord van uw bank account in\nLET OP: HET WACHTWOORD MOET MINSTENS 12 KARAKTERS LANG ZIJN, 1 SPECIAAL KARAKTER(!@#$%^&*), 1 HOOFDLETTER\nEN 1 CIJFER BEVATTEN\n\nu heeft nog {pogingen--} poging\nUw wachtwoord: ");
                        wachtwoordInput = Console.ReadLine();
                    }

                    // user heeft te vaak geprobeert om wachtwoord in te vullen en word terug gestuurd naar het betaalmethode scherm
                    if (pogingen <= 0)
                    {
                        Console.Write("\nU heeft te vaak geprobeert uw wachtwoord in te vullen.\nVoor veiligheidsredenen moeten wij u terug sturen naar het hoofdmenu.\nDruk op \"Q\" om terug te keren: ");
                        string Qinput = Console.ReadLine();
                        bool isItQ = false;
                        if (Qchecker(Qinput))
                        {
                            Console.Clear();
                            betaalOpties();
                        }
                        while (!isItQ)
                        {
                            Console.Write("\nU heeft te vaak geprobeert uw wachtwoord in te vullen.\nVoor veiligheidsredenen moeten wij u terug sturen naar het hoofdmenu.\nDruk op \"Q\" om terug te keren: ");
                            Qinput = Console.ReadLine();
                            if (Qchecker(Qinput))
                            {
                                Console.Clear();
                                betaalOpties();
                            }
                        }
                    }
                }
            }
        }
        public static void payPal()
        {
            Console.WriteLine("U heeft gekozen voor PayPal.\n");
            Console.WriteLine("Vul hieronder uw Email adres en wachtwoord in.\n");
            Console.WriteLine("Wilt u naar het vorige scherm?\nDruk dan op \"Q\".\n");
            Console.Write("Uw Email adres: ");

            // de user krijgt de optie op zijn email adres in te vullen
            string eMailInput = Console.ReadLine();
            bool eMailCorrect = false;
            if (Qchecker(eMailInput))
            {
                Console.Clear();
                betaalOpties();
            }
            // email word gecheckt op geldigheid
            if (eMailChecker(eMailInput))
            {
                eMailCorrect = true;
            }
            // user krijgt de mogelijkheid om opniew email in te vullen totdat deze geldig is
            while (!eMailCorrect)
            {
                Console.WriteLine("\nHet ingevoerde Email adres is ongeldig, probeert u het nog eens: ");
                eMailInput = Console.ReadLine();
                if (eMailChecker(eMailInput))
                {
                    eMailCorrect = true;
                }
                if (Qchecker(eMailInput))
                {
                    Console.Clear();
                    betaalOpties();
                }
            }
            // User moet wachtwoord invullen na dat hij een geldige email heeft ingevult
            if (eMailCorrect)
            {
                Console.Write("\nVul hier uw wachtwoord van uw PayPal account in\nLET OP: HET WACHTWOORD MOET MINSTENS 12 KARAKTERS LANG ZIJN,1 SPECIAAL KARAKTER(!@#$%^&*), 1 HOOFDLETTER\nEN 1 CIJFER BEVATTEN\n\nUw wachtwoord: ");
                string wachtwoordInput = Console.ReadLine();
                bool wachtwoordCorrect = false;
                // De betaling is gelukt als het wachtwoord klopt. User wordt terug gestuurd naar het betaalmethode scherm
                if (wachtwoordChecker(wachtwoordInput))
                {
                    wachtwoordCorrect = true;
                    Console.WriteLine("\nDe betaling is gelukt!\nDank u wel en geniet van uw film!");
                    Console.Write("\nU kunt op \"Q\" drukken om terug te keren naar het hoofdscherm: ");
                    string exitInput = Console.ReadLine();
                    bool Qpressed = false;
                    if (Qchecker(exitInput))
                    {
                        Console.Clear();
                        betaalOpties();
                    }
                    while (!Qpressed)
                    {
                        Console.Write("\nDruk op \"Q\" om terug te keren: ");
                        exitInput = Console.ReadLine();
                        if (Qchecker(exitInput))
                        {
                            Console.Clear();
                            betaalOpties();
                        }
                    }
                }
                // wachtwoord is ongeldig. User krijgt nog 2 pogingen om wachtwoord in te vullen
                int pogingen = 2;
                while (!wachtwoordCorrect)
                {
                    Console.Write($"\nVul hier uw wachtwoord van uw PayPal account in\nLET OP: HET WACHTWOORD MOET MINSTENS 12 KARAKTERS LANG ZIJN, 1 SPECIAAL KARAKTER(!@#$%^&*), 1 HOOFDLETTER\nEN 1 CIJFER\n\nu heeft nog {pogingen--} pogingen\nUw wachtwoord: ");
                    wachtwoordInput = Console.ReadLine();

                    if (wachtwoordChecker(wachtwoordInput))
                    {
                        wachtwoordCorrect = true;
                        Console.WriteLine("\nDe betaling is gelukt!\nDank u wel en geniet van uw film!");
                        Console.Write("\nU kunt op \"Q\" drukken om terug te keren naar het hoofdscherm: ");
                        string exitInput = Console.ReadLine();
                        bool Qpressed = false;
                        if (Qchecker(exitInput))
                        {
                            Console.Clear();
                            betaalOpties();
                        }
                        while (!Qpressed)
                        {
                            Console.Write("\nDruk op \"Q\" om terug te keren: ");
                            exitInput = Console.ReadLine();
                            if (Qchecker(exitInput))
                            {
                                Console.Clear();
                                betaalOpties();
                            }
                        }
                    }
                    if (pogingen == 1)
                    {
                        Console.Write($"\nVul hier uw wachtwoord van uw PayPal account in\nLET OP: HET WACHTWOORD MOET MINSTENS 12 KARAKTERS LANG ZIJN, 1 SPECIAAL KARAKTER(!@#$%^&*), 1 HOOFDLETTER\nEN 1 CIJFER BEVATTEN\n\nu heeft nog {pogingen--} poging\nUw wachtwoord: ");
                        wachtwoordInput = Console.ReadLine();
                    }
                    // user heeft te vaak geprobeert om wachtwoord in te vullen en word terug gestuurd naar het betaalmethode scherm
                    if (pogingen <= 0)
                    {
                        Console.Write("\nU heeft te vaak geprobeert uw wachtwoord in te vullen.\nVoor veiligheidsredenen moeten wij u terug sturen naar het hoofdmenu.\nDruk op \"Q\" om terug te keren: ");
                        string Qinput = Console.ReadLine();
                        bool isItQ = false;
                        if (Qchecker(Qinput))
                        {
                            Console.Clear();
                            betaalOpties();
                        }
                        while (!isItQ)
                        {
                            Console.Write("\nU heeft te vaak geprobeert uw wachtwoord in te vullen.\nVoor veiligheidsredenen moeten wij u terug sturen naar het hoofdmenu.\nDruk op \"Q\" om terug te keren: ");
                            Qinput = Console.ReadLine();
                            if (Qchecker(Qinput))
                            {
                                Console.Clear();
                                betaalOpties();
                            }
                        }
                    }
                }
            }
        }
        public static void creditCard()
        {
            Console.WriteLine("U heeft gekozen voor CreditCard.\n");
            Console.WriteLine("Vul hieronder het CreditCardnummer in van uw CreditCard:\n");
            Console.WriteLine("Wilt u naar het vorige scherm?\nDruk dan op \"Q\".\n");
            Console.Write("Uw CreditCardnummer: ");

            // de user krijgt de optie op zijn creditcard nummer in te vullen
            string creditCardNummerInput = Console.ReadLine();
            bool creditCardNummerCorrect = false;
            if (Qchecker(creditCardNummerInput))
            {
                Console.Clear();
                betaalOpties();
            }

            // creditcard nummer word gecheckt op geldigheid
            if (creditCardNummerChecker(creditCardNummerInput))
            {
                creditCardNummerCorrect = true;
            }

            // user krijgt de mogelijkheid om opniew creditcard nummer in te vullen totdat deze geldig is
            while (!creditCardNummerCorrect)
            {
                Console.Write("\nHet ingevoerde CreditCardNummer is ongeldig, probeert u het nog eens\nUw CreditCardnummer: ");
                creditCardNummerInput = Console.ReadLine();
                if (creditCardNummerChecker(creditCardNummerInput))
                {
                    creditCardNummerCorrect = true;
                }
                if (Qchecker(creditCardNummerInput))
                {
                    Console.Clear();
                    betaalOpties();
                }
            }

            // na een geldig creditcard nummer te hebben ingevult, moet de user de vervaldatum van de creditcard invullen.
            if (creditCardNummerCorrect)
            {
                Console.Write("\nVul hier de vervaldatum van uw CreditCard in (MM/JJ of MM-JJ)\n\nDe vervaldatum: ");
                string vervaldatumInput = Console.ReadLine();
                bool vervaldatumCorrect = false;
                if (vervaldatumChecker(vervaldatumInput))
                {
                    vervaldatumCorrect = true;
                }
                if (Qchecker(vervaldatumInput))
                {
                    Console.Clear();
                    betaalOpties();
                }
                // user krijgt de mogelijkheid om steeds opniew een vervaldatum in te vullen totdat deze geldig is
                while (!vervaldatumCorrect)
                {
                    Console.Write("\nDe ingevoerde vervaldatum is ongeldig of uw creditcard is verlopen, probeert u het nog eens.\nLET ER OP DAT U HET FORMAAT MM/JJ OF MM-JJ GEBRUIKT (INCLUSIEF HET STREEPJE OF DE SLASH)\nDe vervaldatum van uw creditcard: ");
                    vervaldatumInput = Console.ReadLine();
                    if (vervaldatumChecker(vervaldatumInput))
                    {
                        vervaldatumCorrect = true;
                    }
                    if (Qchecker(vervaldatumInput))
                    {
                        Console.Clear();
                        betaalOpties();
                    }
                }
                // user moet na een geldige vervaldatum ingevult te hebben, een CVC code invullen.
                if (vervaldatumCorrect)
                {
                    Console.Write("\nVul hier de CVC code van uw creditcard in.\n\nDe CVC code: ");
                    string cvcInput = Console.ReadLine();
                    bool cvcCorrect = false;

                    // De CVC code is geldig en de betaling is gelukt. User wordt terug getsuurd naar het betaalmethode schrem
                    if (cvcChecker(cvcInput))
                    {
                        cvcCorrect = true;
                        Console.WriteLine("\nDe betaling is gelukt!\nDank u wel en geniet van uw film!");
                        Console.Write("\nU kunt op \"Q\" drukken om terug te keren naar het hoofdscherm: ");
                        string exitInput = Console.ReadLine();
                        bool Qpressed = false;
                        if (Qchecker(exitInput))
                        {
                            Console.Clear();
                            betaalOpties();
                        }
                        while (!Qpressed)
                        {
                            Console.Write("\nDruk op \"Q\" om terug te keren: ");
                            exitInput = Console.ReadLine();
                            if (Qchecker(exitInput))
                            {
                                Console.Clear();
                                betaalOpties();
                            }
                        }
                    }
                    if (Qchecker(cvcInput))
                    {
                        Console.Clear();
                        betaalOpties();
                    }

                    // CVC code is niet geldig. User krijgt nog 2 pogingen om een gelidge CVC code in te vullen
                    int pogingen = 2;
                    while (!cvcCorrect)
                    {
                        Console.Write($"\nVul hier de CVC code (3 cijfers achter de kaart) van uw creditcard in\n\nU heeft nog {pogingen--} pogingen.\nDe CVC code: ");
                        cvcInput = Console.ReadLine();

                        if (cvcChecker(cvcInput))
                        {
                            cvcCorrect = true;
                            Console.WriteLine("\nDe betaling is gelukt!\nDank u wel en geniet van uw film!");
                            Console.Write("\nU kunt op \"Q\" drukken om terug te keren naar het hoofdscherm: ");
                            string exitInput = Console.ReadLine();
                            bool Qpressed = false;
                            if (Qchecker(exitInput))
                            {
                                Console.Clear();
                                betaalOpties();
                            }
                            while (!Qpressed)
                            {
                                Console.Write("\nDruk op \"Q\" om terug te keren: ");
                                exitInput = Console.ReadLine();
                                if (Qchecker(exitInput))
                                {
                                    Console.Clear();
                                    betaalOpties();
                                }
                            }
                        }
                        if (pogingen == 1)
                        {
                            Console.Write($"\nVul hier de CVC code (3 cijfers achter de kaart) van uw creditcard in\n\nU heeft nog {pogingen--} poging.\nDe CVC code: ");
                            cvcInput = Console.ReadLine();
                        }

                        // User heeft te vaak geprobeert om een CVC code in te vullen. User wordt terug gestuurd naar het betaalmethode scherm
                        if (pogingen <= 0)
                        {
                            Console.Write("\nU heeft te vaak geprobeert uw CVC code in te vullen.\nVoor veiligheidsredenen moeten wij u terug sturen naar het hoofdmenu.\nDruk op \"Q\" om terug te keren: ");
                            string Qinput = Console.ReadLine();
                            bool isItQ = false;
                            if (Qchecker(Qinput))
                            {
                                Console.Clear();
                                betaalOpties();
                            }
                            while (!isItQ)
                            {
                                Console.Write("\nU heeft te vaak geprobeert uw CVC code in te vullen.\nVoor veiligheidsredenen moeten wij u terug sturen naar het hoofdmenu.\nDruk op \"Q\" om terug te keren: ");
                                Qinput = Console.ReadLine();
                                if (Qchecker(Qinput))
                                {
                                    Console.Clear();
                                    betaalOpties();
                                }
                            }
                        }
                    }
                }
            }
        }
        // checkt of userinput "q" of "Q" is
        public static bool Qchecker(string s)
        {
            if (s == "q" || s == "Q")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool ibanChecker(string ibanInput)
        {
            // checkt of het IBAN nummer 18 karakters is
            if (ibanInput.Length != 18)
            {
                return false;
            }
            char[] ibanArray = new char[18];

            for (int i = 0; i < ibanArray.Length; i++)
            {
                ibanArray[i] = ibanInput[i];
                // checkt of het 1e, 2e, 5e, 6e, 7e en 8e karakter een letter is
                if (i == 0 || i == 1 || i == 4 || i == 5 || i == 6 || i == 7)
                {
                    if (!charChecker(ibanArray[i]))
                    {
                        return false;
                    }
                }
                // checkt of het 3e, 4e, 9e, 10e, 11e, 12e, 13e, 14e, 15e, 16e, 17e en 18e karakter een cijfer is
                if (i == 2 || i == 3 || i == 8 || i == 9 || i == 10 || i == 11 || i == 12 || i == 13 || i == 14 || i == 15 || i == 16 || i == 17)
                {
                    if (!charIsNummerChecker(ibanArray[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        // checkt of een gegeven karakter een letter is
        public static bool charChecker(char c)
        {
            if (c == 'a' || c == 'b' || c == 'c' || c == 'd' || c == 'e' || c == 'f' || c == 'g' || c == 'h' || c == 'i' || c == 'j' || c == 'k' || c == 'l' || c == 'm' || c == 'n' || c == 'o' || c == 'p' || c == 'q' || c == 'r' || c == 's' || c == 't' || c == 'u' || c == 'v' || c == 'w' || c == 'x' || c == 'y' || c == 'z' || c == 'A' || c == 'B' || c == 'C' || c == 'D' || c == 'E' || c == 'F' || c == 'G' || c == 'H' || c == 'I' || c == 'J' || c == 'K' || c == 'L' || c == 'M' || c == 'N' || c == 'O' || c == 'P' || c == 'Q' || c == 'R' || c == 'S' || c == 'T' || c == 'U' || c == 'V' || c == 'W' || c == 'X' || c == 'Y' || c == 'Z')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // checkt of een gegeven karakter een cijfer is
        public static bool charIsNummerChecker(char c)
        {
            if (c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // checkt er een nummer in een string zit
        public static bool nummerInStringchecker(string s)
        {
            if (s.Contains("1") || s.Contains("2") || s.Contains("3") || s.Contains("4") || s.Contains("5") || s.Contains("6") || s.Contains("7") || s.Contains("8") || s.Contains("9") || s.Contains("0"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // checkt of een wachtwoord geldig is
        public static bool wachtwoordChecker(string wachtwoord)
        {
            if (specialeKarakterChecker(wachtwoord) && hoofdletterChecker(wachtwoord) && nummerInStringchecker(wachtwoord) && wachtwoord.Length >= 12)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // checkt of er een speciale karakter in een string zit
        public static bool specialeKarakterChecker(string input)
        {
            if (input.Contains('!') || input.Contains('@') || input.Contains('#') || input.Contains('$') || input.Contains('%') || input.Contains('^') || input.Contains('&') || input.Contains('*'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // checkt of er een hoofdletter in een string zit
        public static bool hoofdletterChecker(string input)
        {
            if (input.Contains('A') || input.Contains('B') || input.Contains('C') || input.Contains('D') || input.Contains('E') || input.Contains('F') || input.Contains('G') || input.Contains('H') || input.Contains('I') || input.Contains('J') || input.Contains('K') || input.Contains('L') || input.Contains('M') || input.Contains('N') || input.Contains('O') || input.Contains('P') || input.Contains('Q') || input.Contains('R') || input.Contains('S') || input.Contains('T') || input.Contains('U') || input.Contains('V') || input.Contains('W') || input.Contains('X') || input.Contains('Y') || input.Contains('Z'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // checkt of er een kleine letter in een string zit
        public static bool kleineLetterChecker(string input)
        {
            if (input.Contains('a') || input.Contains('b') || input.Contains('c') || input.Contains('d') || input.Contains('e') || input.Contains('f') || input.Contains('g') || input.Contains('h') || input.Contains('i') || input.Contains('j') || input.Contains('k') || input.Contains('l') || input.Contains('m') || input.Contains('n') || input.Contains('o') || input.Contains('p') || input.Contains('q') || input.Contains('r') || input.Contains('s') || input.Contains('t') || input.Contains('u') || input.Contains('v') || input.Contains('w') || input.Contains('x') || input.Contains('y') || input.Contains('z'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // checkt of een gegeven email geldig is
        public static bool eMailChecker(string eMail)
        {
            if (eMail.Contains('@') && eMail.Contains('.'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // checkt of een gegeven creditCardnummer daadwerkelijk een nummer is en geldig is
        public static bool creditCardNummerChecker(string creditCardNummer)
        {
            if (creditCardNummer.Length == 16 && !kleineLetterChecker(creditCardNummer) && !hoofdletterChecker(creditCardNummer))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // Checkt of een gegeven datum geldig is
        public static bool vervaldatumChecker(string vervaldatum)
        {
            bool valid = true;
            if (vervaldatum.Length != 5)
            {
                return false;
            }

            // checken of de gegeven maand en jaartal geldig zijn (in format MM/JJ of MM-JJ).
            for (int i = 0; i < vervaldatum.Length; i++)
            {
                if (i == 0 && !(vervaldatum[i] == '0' || vervaldatum[i] == '1'))
                {
                    valid = false;
                }
                if (i == 1)
                {
                    if (vervaldatum[0] == '0')
                    {
                        if (!(charIsNummerChecker(vervaldatum[i])))
                        {
                            valid = false;
                        }
                    }
                    if (vervaldatum[0] == '1')
                    {
                        if (!(vervaldatum[i] == '0' || vervaldatum[i] == '1' || vervaldatum[i] == '2'))
                        {
                            valid = false;
                        }
                    }
                }
                if (i == 2 && !(vervaldatum[i] == '-' || vervaldatum[i] == '/'))
                {
                    valid = false;
                }
                if (i == 3 && !charIsNummerChecker(vervaldatum[i]))
                {
                    valid = false;
                }
                if (i == 4 && !charIsNummerChecker(vervaldatum[i]))
                {
                    valid = false;
                }
            }
            if (!valid)
            {
                return false;
            }

            // De huidige datum krijgen en omzetten naar MM/YY format
            DateTime huidigeDatum = DateTime.UtcNow.Date;
            string huidigeDatumString = huidigeDatum.ToString("MM/yy");

            // vervaldatum omzetten naar ints
            char charVervalMaand0 = (char)vervaldatum[0];
            int vervalMaand0 = charVervalMaand0 - '0';
            char charVervalMaand1 = (char)vervaldatum[1];
            int vervalMaand1 = charVervalMaand1 - '0';
            char charVervalJaar0 = (char)vervaldatum[3];
            int vervalJaar0 = charVervalJaar0 - '0';
            char charVervalJaar1 = (char)vervaldatum[4];
            int vervalJaar1 = charVervalJaar1 - '0';

            // huidige datum omzetten naar ints
            char charHuidigeMaand0 = (char)huidigeDatumString[0];
            int huidigeMaand0 = charHuidigeMaand0 - '0';
            char charHuidigeMaand1 = (char)huidigeDatumString[1];
            int huidigeMaand1 = charHuidigeMaand1 - '0';
            char charHuidigeJaar0 = (char)huidigeDatumString[3];
            int huidigeJaar0 = charHuidigeJaar0 - '0';
            char charHuidigeJaar1 = (char)huidigeDatumString[4];
            int huidigeJaar1 = charHuidigeJaar1 - '0';

            valid = false;

            // checkt of de gegeven datum eerder is dan de vervaldatum
            if (huidigeJaar0 < vervalJaar0)
            {
                valid = true;
            }
            else if (huidigeJaar0 == vervalJaar0)
            {
                if (huidigeJaar1 < vervalJaar1)
                {
                    valid = true;
                }
                else if (huidigeJaar1 == vervalJaar1)
                {
                    if (huidigeMaand0 < vervalMaand0)
                    {
                        valid = true;
                    }
                    else if (huidigeMaand0 == vervalMaand0)
                    {
                        if (huidigeMaand1 < vervalMaand1)
                        {
                            valid = true;
                        }
                        else if (huidigeMaand1 == vervalMaand1)
                        {
                            valid = true;
                        }
                        else
                        {
                            valid = false;
                        }
                    }
                    else
                    {
                        valid = false;
                    }
                }
                else
                {
                    valid = false;
                }
            }
            else
            {
                valid = false;
            }
            return valid;
        }
        // Checkt of een gegeven cvc code geldig is
        public static bool cvcChecker(string cvcInput)
        {
            if (cvcInput.Length != 3)
            {
                return false;
            }
            bool valid = false;
            int counter = 0;
            for (int i = 0; i < cvcInput.Length; i++)
            {
                if (charIsNummerChecker(cvcInput[i]))
                {
                    counter++;
                }
            }
            if (counter == 3)
            {
                valid = true;
            }
            return valid;
        }
    }
}
