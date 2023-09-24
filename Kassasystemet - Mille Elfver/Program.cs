using System.Collections.Generic;

namespace Kassasystemet___Mille_Elfver
{
    internal class Program
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

        //skapat en dictionary med alla produkter och kodnamn
        static Dictionary<string, Product> availableProducts = new Dictionary<string, Product>
        {
            { "300", new Product("Bananer", 15.50m, "st kostnad:") },
            { "301", new Product("Nutella", 21.90m, "st kostnad:") },
            { "302", new Product("Citron", 5.50m, "st kostnad:") },
            { "303", new Product("Jordgubbar", 39.90m, "st kostnad:") },
            { "304", new Product("Grädde", 24.90m, "st kostnad:") },
            { "305", new Product("Choklad", 22.90m, "st kostnad:") },
            { "306", new Product("Apelsiner", 10, "st kostnad:") },
            { "307", new Product("Mango", 20, "st kostnad:") },
            { "308", new Product("Tomater", 49.90m, "st kostnad:") },
            { "309", new Product("Kött", 229.90m, "st kostnad:") },
            { "310", new Product("Godis", 99.50m, "st kostnad:") }
        };

        static void Main(string[] args)
        {

            MainMenu();

            //en metod för menyn
            static void MainMenu()
            {
                bool programRunning = true;
                do
                {
                    //Menylista för kassasystemet 
                    Console.Clear();
                    Console.WriteLine("KASSA");
                    Console.WriteLine("1. Ny kund");
                    Console.WriteLine("0. Avsluta");
                    Console.Write("Svar: ");

                    int val;
                    string userInput = Console.ReadLine();
                    if (int.TryParse(userInput, out val) && val >= 0 && val <= 1) //try parse istället för try catch
                    {
                        switch (val)
                        {
                            case 1:
                                //Kassan startas med ny försäljning
                                Console.Clear();
                                Console.WriteLine("KASSA");

                                string receipt = NewSale();

                                Console.ReadKey();
                                break;

                            case 0:
                                Console.WriteLine("Tryck valfri knapp för att avsluta programmet.");
                                programRunning = false;
                                Console.ReadKey();
                                break;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Ogiltig inmatning, tryck valfri knapp för att återgå till menyn och välj '1' eller '0'");
                    }
                    Console.ReadKey();

                } while (programRunning == true);
            }

            //en metod för ny försäljning
            static string NewSale()
            {
                //deklarerar variabeln receipt som sedan kommer tilldelas info från SaveAndDisplayReceipt
                string receipt = "";

                while (true)
                {
                    Console.WriteLine("Kommandon:");
                    Console.WriteLine("<productid> <antal> <PAY> <ITEMS> <MENU>");
                    Console.Write("Kommando: ");
                    string userInput = Console.ReadLine().Trim();

                    //om användaren skriver pay, string comparison så att användaren kan skriva pay eller PAY
                    if (userInput.Equals("PAY", StringComparison.OrdinalIgnoreCase))
                    {
                        //sparar kvittot och visar det
                        SaveAndDisplayReceipt(receipt);
                        Console.WriteLine("\nKöpet har genomförts och kvitto nedsparat. Tryck valfri knapp för att komma tillbaka till menyn");
                        break;
                    }

                    //om användaren skriver items, string comparison så att användaren kan skriva items hur den vill 
                    if (userInput.Equals("ITEMS", StringComparison.OrdinalIgnoreCase))
                    {
                        //Skriver ut listan på alla produkter
                        Console.Clear();
                        DisplayTheProducts();
                        continue;
                    }

                    if (userInput.Equals("MENU", StringComparison.OrdinalIgnoreCase))
                    {
                        MainMenu();
                    }

                    //split för produktid och antal
                    string[] productParts = userInput.Split(' ');

                    //felhantering om längden inte är 2 (ex 300 1)
                    if (productParts.Length != 2)
                    {
                        Console.Clear();
                        Console.WriteLine("Det här valet fanns inte, här är en lista för produkterna:\n");
                        DisplayTheProducts();
                        continue;
                    }

                    string productId = productParts[0];
                    int quantityOfProducts;

                    //Felhantering om användaren inte börjar sin inmatning med produktID
                    if (!int.TryParse(productParts[1], out quantityOfProducts))
                    {
                        Console.WriteLine("Det här valet fanns inte, här är en lista för produkterna:\n");
                        DisplayTheProducts();
                        continue;
                    }

                    //lägger till produkterna till kvittot
                    AddProductsToReceipt(productId, quantityOfProducts, ref receipt);

                }

                //return receipt om while loopen inte används
                return receipt;
            }

            //metod som visar alla tillgängliga produkter
            static void DisplayTheProducts()
            {
                Console.WriteLine("Tillgängliga produkter:");
                foreach (var product in availableProducts)
                {
                    Console.WriteLine($"{product.Key}: {product.Value.Name} ({product.Value.UnitPrice} {product.Value.PriceType})\n");
                }
            }

            //metod som lägger till produkterna till kvittot
            static void AddProductsToReceipt(string productId, int quantityOfProducts, ref string receipt)
            {
                Console.Clear();
                if (availableProducts.TryGetValue(productId, out Product selectedProduct))
                {
                    if (quantityOfProducts < 1)
                    {
                        Console.WriteLine("Du kan inte ange mindre än 1 i antal!");
                    }
                    else
                    {
                        decimal totalPrice = selectedProduct.UnitPrice * quantityOfProducts;
                        string productToAdd = $"{productId} {selectedProduct.Name} {quantityOfProducts} {selectedProduct.PriceType} {totalPrice}";
                        receipt += productToAdd + "\n";

                        //Skriver ut vad som lagts till i kassan
                        Console.WriteLine($"Produkt: {selectedProduct.Name}, {quantityOfProducts} st, totalt pris: {totalPrice} kr har lagts till på kvittot.");
                    }
                }
                else
                {
                    Console.WriteLine("Det här valet fanns inte, här är en lista för produkterna:\n");
                    DisplayTheProducts();
                }
            }
        }

        //metod för att spara kvittot
        static void SaveAndDisplayReceipt(string receipt)
        {

            //räknar ut totalen av allt som lagts in på kvittot och visar det
            decimal totalAmount = CalculateTotalAmount(receipt);

            //får fram nuvarande tid till innehållet i kvittots textfil
            DateTime dateTime = DateTime.Now;

            //datum till kvittot när det skrivs ut (i textfilens rubrik) 
            var date = DateTime.Now.ToShortDateString();

            //formatterar datum och tid till en sträng
            string formattedDate = dateTime.ToString("yyyy-MM-dd HH:mm:ss");

            //denna gör så att 40 chars av - läggs på kvittot så att det blir lättare att skilja åt
            string receiptSeparator = new string('-', 40);

            //lägger till datumet och total summan på kvittot:
            string receiptWithDateandTotalAmount = $"\n{receiptSeparator}\nKVITTO {formattedDate}\n\n{receipt}\nTotal: {totalAmount} KR\n{receiptSeparator}";

            //filnamn för kvittot med kvittonummer och datum
            string fileName = $"Kvitto - {date}.txt";

            //sparar ned kvittot med en Append så att det fylls på med nya kvitton
            File.AppendAllText($"../../../{fileName}", receiptWithDateandTotalAmount);
            Console.WriteLine(receiptWithDateandTotalAmount);
        }

        //metod som räknar ut totalen på alla produkter som jag sen lägger in på kvittot
        static decimal CalculateTotalAmount(string receipt)
        {
            //splittar produkterna i kvittot till individuella rader
            string[] linesInReceipt = receipt.Split('\n');

            //tilldelar variabeln totalAmount 0, så fylls den på nedan
            decimal totalAmount = 0m;

            //foreach loop som kollar inuti varje rad 
            foreach (string line in linesInReceipt)
            {
                //splittar innehållet i varje rad till enskilda delar
                string[] partsInReceipt = line.Split(' ');

                //om raden är större än eller lika med 4, plussa på priset på totalen
                if (partsInReceipt.Length >= 5)
                {
                    if (decimal.TryParse(partsInReceipt[partsInReceipt.Length - 1], out decimal totalPrice))
                    {
                        totalAmount += totalPrice;
                    }
                }
            }
            return totalAmount;
        }

    }

}
