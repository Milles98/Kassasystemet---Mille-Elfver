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
            { "300", new Product("Bananer", 40, "Styckpris") },
            { "301", new Product("Nutella", 20, "Styckpris") },
            { "302", new Product("Citron", 5, "Styckpris") },
            { "303", new Product("Jordgubbar", 10, "Styckpris") },
            { "304", new Product("Grädde", 16, "Styckpris") },
            { "305", new Product("Choklad", 10, "Styckpris") },
            { "306", new Product("Apelsiner", 30, "Kilopris") },
            { "307", new Product("Mango", 30, "Styckpris") },
            { "308", new Product("Tomater", 30, "Kilopris") },
            { "309", new Product("Kött", 30, "Kilopris") },
            { "310", new Product("Godis", 30, "Kilopris") }
        };

        static void Main(string[] args)
        {
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

                            string receipt = NewSale();

                            //här ska kvittot visas efter att produkter med sitt id lagts in

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

            //en metod för ny försäljning
            static string NewSale()
            {
                //deklarerar variabeln receipt som sedan kommer tilldelas info från SaveAndDisplayReceipt
                string receipt = "";

                while (true)
                {
                    Console.WriteLine("Kommandon:");
                    Console.WriteLine("<productid> <antal> <PAY>");
                    Console.Write("Kommando: ");
                    string userInput = Console.ReadLine().Trim();

                    //om användaren skriver pay, string comparison så att användaren kan skriva pay eller PAY
                    if (userInput.Equals("PAY", StringComparison.OrdinalIgnoreCase))
                    {
                        //sparar kvittot och visar det
                        SaveAndDisplayReceipt(receipt);
                        break;
                    }

                    //split för produktid och antal
                    string[] productParts = userInput.Split(' ');

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
                        Console.WriteLine("Ogiltigt val, försök igen");
                        continue;
                    }

                    //lägger till produkterna till kvittot
                    AddProductsToReceipt(productId, quantityOfProducts, ref receipt);

                }

                //return statement om while loopen inte användas
                return receipt;
            }


            //metod som visar alla tillgängliga produkter
            static void DisplayTheProducts()
            {
                Console.WriteLine("Tillgängliga produkter:");
                foreach (var product in availableProducts)
                {
                    Console.WriteLine($"{product.Key}: {product.Value.Name} ({product.Value.Price} {product.Value.PriceType})\n");
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
                        decimal totalPrice = selectedProduct.Price * quantityOfProducts;
                        string productToAdd = $"{productId} {selectedProduct.Name} {quantityOfProducts} {selectedProduct.PriceType} {totalPrice}";
                        receipt += productToAdd + "\n";

                        //Rensar konsolen inför nästa produkt som läggs in
                        Console.Clear();
                        Console.WriteLine($"{quantityOfProducts} {selectedProduct.Name} har lagts till på kvittot.");
                    }
                }
                else
                {
                    Console.WriteLine("Produkt finns ej eller fel inmatning");
                }
            }
        }

        //metod för att spara kvittot
        static void SaveAndDisplayReceipt(string receipt)
        {
            //räknar ut totalen av allt som lagts in på kvittot och visar det
            decimal totalAmount = CalculateTotalAmount(receipt);

            //får fram nuvarande tid
            DateTime dateTime = DateTime.Now;

            //formatterar datum och tid till en sträng
            string formattedDate = dateTime.ToString("yyyy-MM-dd HH:mm:ss");

            //lägger till datumet och total summan på kvittot:
            Console.WriteLine();
            string receiptWithDateandTotalAmount = $"KVITTO {formattedDate}\n\n{receipt}\nTotal: {totalAmount} KR ";

            //sparar ned kvittot
            File.WriteAllText("../../../Receipt", receiptWithDateandTotalAmount);
            Console.WriteLine(receiptWithDateandTotalAmount);
        }

        //metod som räknar ut totalen som jag sen lägger in på kvittot
        static int CalculateTotalAmount(string receipt)
        {
            //splittar kvittot till individuella rader
            string[] linesInReceipt = receipt.Split('\n');

            int totalAmount = 0;

            //foreach loop som kollar inuti varje rad
            foreach (string line in linesInReceipt)
            {
                //splittar varje rad till enskilda delar
                string[] partsInReceipt = line.Split(' ');

                if (partsInReceipt.Length >= 5)
                {
                    if (int.TryParse(partsInReceipt[partsInReceipt.Length - 1], out int totalPrice))
                    {
                        totalAmount += totalPrice;
                    }
                }
            }
            return totalAmount;
        }

    }



    class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; } //decimal för priserna
        public string PriceType { get; set; }

        public Product(string name, int price, string priceType)
        {
            Name = name;
            Price = price;
            PriceType = priceType;
        }
    }

}
