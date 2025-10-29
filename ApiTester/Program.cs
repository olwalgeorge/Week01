using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace ApiTester
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5112/");
            
            try
            {
                Console.WriteLine("=== Testing Pizza API ===");
                Console.WriteLine();
                
                // Test GET all pizzas
                Console.WriteLine("GET /pizza - All Pizzas:");
                var response = await client.GetAsync("pizza");
                
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var pizzas = JsonSerializer.Deserialize<List<Pizza>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    
                    foreach (var pizza in pizzas!)
                    {
                        Console.WriteLine($"  ID: {pizza.Id}, Name: {pizza.Name}, Gluten Free: {pizza.IsGlutenFree}");
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
                
                Console.WriteLine();
                Console.WriteLine("=== End Test ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
    
    public class Pizza
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsGlutenFree { get; set; }
    }
}
