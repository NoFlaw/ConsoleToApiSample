using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleToWebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                var userChoice = DisplayMenu();

                switch (userChoice)
                {
                    case 1:
                        var productId = 0;
                        Console.WriteLine("You have chosen to lookup a product, please enter a id to search by: ");
                        if (Int32.TryParse(Console.ReadLine(), out productId))
                            GetProductsAsync(productId).Wait();  
                        break;
                    case 2:
                        Console.WriteLine("2");
                        break;
                    case 5: 
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\n\nYou selected an invalid option: {0}. Please try again.\r\n\n    Press ENTER to continue....", userChoice);
                        continue;
                }
            } while (Console.ReadLine() != "5");            
        }

        private static async Task GetProductsAsync(int id)
        {
            try
            {
                using (var client = HttpClientSingletonWrapper.Instance)
                {
                    DisplayStartMessage();
                 
                    var response = await client.GetAsync(string.Format(@"/api/products/{0}", id));
                    
                    DisplayEndMessage();

                    if (response.IsSuccessStatusCode)
                    {
                        var product = await response.Content.ReadAsAsync<Product>();
                        Console.WriteLine("ID:{0} \tName:{1} \tPrice:${2} \tCategory:{3} \tDept:{4}", product.Id, product.Name, product.Price, product.Category, product.DepartmentId);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(ex.Message, ex.InnerException);
            }
        }

        public static void DisplayStartMessage()
        {
            Console.WriteLine("Retrieving results asynchronously. . . . .\r\n");
        }

        public static void DisplayEndMessage()
        {
            Console.WriteLine("Done.\n");
        }

        public static int DisplayMenu()
        {
            var choice = 0;

            Console.WriteLine("Please choose an option from the Menu:");
            Console.WriteLine("--------------------------------------\n\n");

            Console.WriteLine("......................................");
            Console.WriteLine("======================================");
            Console.WriteLine("1. . .Find product by ID asynchronously");
            Console.WriteLine("5. . .Exit Application  (Bye Bye Now..)");
            Console.WriteLine("======================================");
            Console.WriteLine("......................................\n\n");
            
            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine("The application encountered an error due to keyboard entry: {0}", ex.Message);
                choice = 0;
            }
            
            return choice;
        }
    }
}  
