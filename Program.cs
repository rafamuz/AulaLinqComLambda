using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AulaLinqComLambda.Entities;

namespace AulaLinqComLambda
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Category c1 = new Category() { Id = 1, Name = "Tools", Tier = 2 };
            Category c2 = new Category() { Id = 2, Name = "Computers", Tier = 1 };
            Category c3 = new Category() { Id = 3, Name = "Eletronics", Tier = 1 };

            List<Product> products = new List<Product>()
            {
                new Product() { Id=1, Name="Computer", Price=1100.00, Category = c2 },
                new Product() { Id=2, Name="Hammer", Price=90.00, Category = c1 },
                new Product() { Id=3, Name="TV", Price=1700.00, Category = c3 },
                new Product() { Id=4, Name="Notebook", Price=1300.00, Category = c2 },
                new Product() { Id=5, Name="Saw", Price=80.00, Category = c1 },
                new Product() { Id=6, Name="Tablet", Price=700.00, Category = c2 },
                new Product() { Id=7, Name="Camera", Price=700.00, Category = c3 },
                new Product() { Id=8, Name="Printer", Price=350.00, Category = c3 },
                new Product() { Id=9, Name="MacBook", Price=1800.00, Category = c2 },
                new Product() { Id=10, Name="Sound Bar", Price=700.00, Category = c3 },
                new Product() { Id=11, Name="Level", Price=70.00, Category = c1 },
            };
            
            var r1 = products.Where(p => p.Category.Tier == 1 && p.Price < 900.00);
            Print("TIER 1 AND PRICE < 900.00:", r1);

            var r2 = products.Where(p => p.Category.Name == "Tools").Select(p => p.Name);
            Print("PRODUCTS NAMES FROM TOOLS CATEGORY:", r2);

            var r3 = products
                .Where(p => p.Name[0] == 'C')
                .Select(p => new {p.Name, 
                           p.Price, 
                           CategoryName = p.Category.Name });//ANONYMOUS OBJECT

            Print("NAMES STARTED WITH 'C' AND ANONYMOUS OBJECT:", r3);

            var r4 = products.Where(p => p.Category.Tier == 1)
                .OrderBy(p => p.Price)
                .ThenBy(p => p.Name);
            Print("ALL PRODUCTS FROM TIER 1 ORDERED BY PRICE AND NAME:", r4);

            var r5 = r4.Skip(2).Take(4);
            Print("ALL PRODUCTS FROM TIER 1 ORDERED BY PRICE AND NAME SKIP 2 TAKE 4:", r5);

            var r6 = products.First();
            Console.WriteLine("First: " + r6);
            var r7 = products.Where(p => p.Price > 3000).FirstOrDefault(); //IF FIRST(), THROWS EXCEPTION.
            Console.WriteLine("First: " + r7);

            var r8 = products.Where(p => p.Id == 3).SingleOrDefault();//USE WHEN YOU ARE SURE IT RETURNS ONLY 1 ELEMENT (OR NONE)
            Console.WriteLine("Single or Default Test1: " + r8);

            var r9 = products.Where(p => p.Id == 400).SingleOrDefault();
            Console.WriteLine("Single or Default Test2: " + r9);
            Console.WriteLine();

            //USING PREDEFINED AGREGATION FUNCTIONS:
            var r10 = products.Max(p => p.Price);
            Console.WriteLine("MAX PRICE PRODUCT: " + r10);
            var r11 = products.Min(p => p.Price);
            Console.WriteLine("MIN PRICE PRODUCT: " + r11);

            var r12 = products.Where(p => p.Category.Id == 1).Sum(p => p.Price);
            Console.WriteLine("Category 1 Sum Prices: " + r12);

            var r13 = products.Where(p => p.Category.Id == 1).Average(p => p.Price);
            Console.WriteLine("Category 1 Average Prices: " + r13);

            var r14 = products.Where(p => p.Category.Id == 5).Select(p => p.Price).DefaultIfEmpty().Average();//Id Category does not exist. So it should be trated over exceptions.
            Console.WriteLine("Category 5 Average Prices: " + r14);            
            
            //USING YOUR OWN AGREGATION FUNCTIONS:

            //The '0.0' down in the aggregate function is to prevent if the result
            //of the p.Category.Id == 1 returns no items...for example p.Category.Id==5
            //So it will use the default value 0.0 or any other you use as argument.
            var r15 = products
                .Where(p => p.Category.Id == 1)
                .Select(p => p.Price)
                .Aggregate(0.0, (x, y) => x + y);
            Console.WriteLine("Category 1 Sum Prices: " + r15);
            Console.WriteLine();
            
            var r16 = products.GroupBy(p => p.Category);
            foreach (IGrouping<Category, Product> group in r16)
            {
                Console.WriteLine("Category " + group.Key.Name + ":");
                foreach (Product product in group)
                {
                    Console.WriteLine("Product: " + product);
                }
                Console.WriteLine();
            }
        }

        static void Print<T>(string message, IEnumerable<T> collection)
        {
           Console.WriteLine(message);
           foreach (T obj in collection)
           {
                Console.WriteLine(obj);
           }
           Console.WriteLine();
        }
    }
}
