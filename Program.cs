using System.Security.Cryptography.X509Certificates;

namespace Restaurang
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Kund alex = new Kund("Alex", 500);
            alex.MenyOrder();

            Console.WriteLine("Tack för att du besökte denna restaurang!");
        }


        static class Meny
        {

            public static Dictionary<string, decimal> menyList = new Dictionary<string, decimal>
            {
                { "1. Pizza", 99.99m },
                { "2. Hamburgare", 199.99m },
                { "3. Sushi", 149.99m },
                { "4. Kebab", 169.99m },
                { "5. Kinamat", 119.99m }
            };


            public static void DisplayMeny()
            {
                foreach (var maträtt in menyList)
                {
                    Console.WriteLine($"{maträtt.Key}: {maträtt.Value}kr");
                }
            }
            
        }

        class Kund
        {
            public string Name { get; }
            private int _Money { get; set; }
            public int _TotalAmount { get; set; }
            private List<(string Mat, decimal Price)> _Order = new List<(string, decimal)>();
            public Kund(string name, int money)
            {
                Name = name;
                _Money = money;
            }

            public void MenyOrder()
            {


                Meny.DisplayMeny();
                Console.WriteLine("Vad önskas från menyn idag?\nVälj en siffra från menyn mellan 1-5");

                int input;

                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out input) && input <= 5 && input >= 1) 
                    {
                        break;
                    } else
                    {
                        Console.WriteLine("Du har inte angett en siffra mellan 1-5 från menyn");
                    } 
                }

                var item = Meny.menyList.ElementAt(input-1);

                _Order.Add((item.Key.Substring(3), item.Value));

                Console.WriteLine($"Du har valt {item.Key.Substring(3)}. som kostar {item.Value}kr\n\n");


                while (true)
                {


                    Console.WriteLine("Skulle du vilja beställa något mer?\nY/N");
                    string again = Console.ReadLine().ToUpper();


                    if (again == "Y")
                    {
                        MenyOrder();
                        break;
                    }
                    else if (again == "N")
                    {
                        Payment();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ange antingen Y, eller N");
                    }

                }

            }

            public void Payment()

            {
                Console.WriteLine("Tack för din beställning");
                Console.WriteLine("Det här har du beställt\n");

                foreach (var item in _Order)
                {
                    Console.WriteLine(item.Mat);
                }
                
                foreach (var item in _Order)
                {
                    _TotalAmount += (int)item.Price; 
                }

                Console.WriteLine($"Totala kostnaden för din beställning blir: {_TotalAmount}:-");

                if (_TotalAmount > _Money)
                {
                    Console.WriteLine("Tyvärr, du har inte tillräckligt med pengar, du får diska för att betala av din skuld");
                } else
                {
                    _Money -= _TotalAmount;
                }
            }




        }
    }
}
