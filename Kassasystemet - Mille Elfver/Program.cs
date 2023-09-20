using System.Collections.Generic;

namespace Kassasystemet___Mille_Elfver
{
    internal class Program
    {

        //skapar en dictionary med alla produkter och kodnamn
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

                            //startar "kvitto" innehållet
                            string receipt = "";

                            while (true)
                            {
                                Console.WriteLine("<productid> <antal>");
                                string userInput = Console.ReadLine().Trim();

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
                                    Console.WriteLine("Det här valet fanns inte, försök igen (exempelvis 300 1 för en banan)");
                                    continue;
                                }

                                string productId = productParts[0];
                                int quantityOfProducts;

                                //Felhantering om användaren inte börjar sin inmatning med produktID
                                if (int.TryParse(productParts[1], out quantityOfProducts))
                                {
                                    Console.WriteLine("Ogiltigt val, försök igen");
                                    continue;
                                }

                                //lägger till produkterna till kvittot
                                AddProductsToReceipt(productId, quantityOfProducts, ref receipt);

                            }

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


            //Seeding med file io

            //metod som visar alla tillgängliga produkter
            static void DisplayTheProducts()
            {
                Console.WriteLine("Tillgängliga produkter:");
                foreach (var product in availableProducts)
                {
                    Console.WriteLine($"{product.Key}: {product.Value.Name} ({product.Value.Price} {product.Value.PriceType})");
                }
            }

            //metod som lägger till produkterna till kvittot
            static void AddProductsToReceipt(string productId, int quantityOfProducts, ref string receipt)
            {
                //läser av existerande produkter om filen redan finns
                string products = File.Exists("../../../Kvitto.txt") ? File.ReadAllText("../../../Kvitto.txt") : "";

                //splittar upp products metoden till enskilda rader efter \n
                string[] productLines = products.Split('\n');

                //för varje produktrad i produkterna
                foreach (string product in productLines)
                {
                    //om produkten börjar med ett produktid ska detta göras
                    if (product.StartsWith(productId))
                    {
                        //om if-satsen ovan stämmer så ska mellanslagen splittas från product
                        string[] partsOfProduct = product.Split(' ');

                        //här kollar if-satsen om längden på produkten är det korrekta (4st efter split)
                        if (partsOfProduct.Length == 4)
                        {
                            //tilldelar tredje delen efter split att det är priset på produkten
                            int priceOfProduct = int.Parse(partsOfProduct[3]);
                            //uträkning för produktens pris * antalet
                            int totalPrice = priceOfProduct * quantityOfProducts;
                            //lägger till alla delar av products in i strängen productToAdd
                            string productToAdd = ($"{partsOfProduct[0]} {partsOfProduct[1]} {quantityOfProducts} {partsOfProduct[2]} {totalPrice}");
                            //compound operator
                            receipt += productToAdd + "\n";
                            Console.WriteLine("Produkt tillagd till kvittot.");
                            return;
                        }
                    }
                }
                Console.WriteLine("Produkten fanns inte, eller så angav du ogiltigt produktID");
            }

            //metod för att spara kvittot
            static void SaveAndDisplayReceipt(string receipt)
            {
                File.WriteAllText("../../../Receipt", receipt);
                Console.WriteLine("\n Kvittot har sparats ned!");
                Console.WriteLine(receipt);
            }

        }

        class Product
        {
            public string Name { get; set; }
            public int Price { get; set; }
            public string PriceType { get; set; }

            public Product(string name, int price, string priceType)
            {
                Name = name;
                Price = price;
                PriceType = priceType;
            }
        }
    }
}