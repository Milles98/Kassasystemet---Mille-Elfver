namespace Kassasystemet___Mille_Elfver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Programmera ett kassasystem - (som man har i kassan i en matbutik)
            //använd double till talen (avrunar typ banan till 19.50 (2 decimaler) kan använda Math.Round med 2 decimaler
            //man får dock använda int istället om det blir jobbigt
            //Kan göra kvitto tabell med ,30 osv, kolla övningsuppgifter

            // ToUpper() (att använda till att göra "PAY" till stora bokstäver) nog bra att lösa med string kanske? osäker
            //Console.WriteLine("Skriv in ditt namn");
            //string name = Console.ReadLine();
            //char firstLetter = name[0];
            //string newName = char.ToUpper(firstLetter) + name.Substring(1).ToLower();
            //Console.WriteLine(newName);

            //Kassasystemet how to:
            //0. Data seeding en fil med produkter
            //allt i en stor loop
            //1. Göra meny (cw)
            //2. User input i en variabel, kan använda (switch) (console.readline) (if sats) (loopar)
            //3. Göra string manipulation (split) och spara i en (array[300] är kod), (array[300, 2] andra är antal
            //4. file IO 
            //5. PAY
            //6. file IO igen
            //7. ett meddelande (vad användaren gjort) sedan går tbx till 1 eller avslutar

            //Seeding med file io
            static void AllProducts()
            {
                if (File.Exists("../../../Kvitto.txt")) return;

                string products = "300 Bananer 40 Styckpris\n" +
                                  "301 Nutella 20 Styckpris\n" +
                                  "302 Citron 5 Styckpris\n" +
                                  "303 Jordgubbar 10 Styckpris\n" +
                                  "304 Grädde 16 Styckpris\n" +
                                  "305 Choklad 10 Styckpris\n" +
                                  "306 Apelsiner 30 Kilopris\n" +
                                  "307 Mango 30 Styckpris\n" +
                                  "308 Tomater 30 Kilopris\n" +
                                  "309 Kött 30 Kilopris\n" +
                                  "310 Godis 30 Kilopris\n";
                string[] productLines = products.Split(' ');

                foreach (string productLine in productLines)
                {
                    Console.Write(productLine);
                }
                

                File.WriteAllText("../../../Kvitto.txt", products);
            }


            bool programRunning = true;
            do
            {
                try
                {
                    int val;

                    //Menylista för kassasystemet 
                    Console.Clear();
                    Console.WriteLine("KASSA");
                    Console.WriteLine("1. Ny kund");
                    Console.WriteLine("0. Avsluta");
                    Console.Write("Svar: ");

                    val = Convert.ToInt32(Console.ReadLine());
                    switch (val)
                    {
                        case 1:
                            //Kassan startas med ny försäljning
                            Console.Clear();
                            Console.WriteLine("KASSA");
                            //här ska kvittot visas efter att produkter med sitt id lagts in

                            //här ska kommandon skrivas på följande sätt: <productid> <antal>
                            Console.WriteLine("kommandon:");
                            Console.WriteLine("<productid <antal>");

                            //låter användaren mata in vilken produkt samt antalet
                            
                            Console.ReadKey();
                            break;

                        case 0:
                            Console.WriteLine("Tryck valfri knapp för att avsluta programmet.");
                            programRunning = false;
                            Console.ReadKey();
                            break;
                    }
                }

                catch (FormatException)
                {
                    Console.WriteLine("Ogiltig inmatning, tryck valfri knapp för att återgå till menyn");
                    Console.ReadKey();
                }
            } while (programRunning == true);








        }
    }
}