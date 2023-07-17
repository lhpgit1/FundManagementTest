// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using WorkingWithEFCore.Models;

static void ListProducts() 
{
    using (NorthwindDbContext db= new ())

    {
     {
        Console.WriteLine("{0,3} { 1,-35}",
        "ProductId", "ProductName");

       foreach(Product p in db.Products.OrderByDescending(product=>product.ProductId)){
            Console.WriteLine("{0:000} {1,-35}{2,8:$#,##0.00} }",
                     p.ProductId , p.ProductName ,p.Discontinued );
        }
     }
  }
}
ListProducts();
    

//Console.WriteLine("Hello, World!");


