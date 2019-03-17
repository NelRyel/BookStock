using ManagerLibrary;
using StockEntModelLibrary;
using StockEntModelLibrary.CustumerEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //StockDBcontext context = new StockDBcontext();
            //IQueryable<Custumer> custumers = context.Custumers.Where(i => i.IsDelete == false);
            //foreach (var item in custumers)
            //{
            //    Console.WriteLine(item.CustumerTitle);
            //}

            CustumerManager custumerManager = new CustumerManager();
            var custumers = custumerManager.GetAllCustumers();
            foreach (var item in custumers)
            {
                Console.WriteLine(item.CustumerTitle);
            }



            Console.WriteLine("done");

            Custumer c = new Custumer();
            c.CustumerTitle = "shit";
            c.BuyerTrue_SuplierFalse = true;
            c.Balance = 0;

            custumerManager.CreateCustumer(c);

            var custumers2 = custumerManager.GetAllCustumers();
            foreach (var item in custumers2)
            {
                Console.WriteLine(item.CustumerTitle);
            }
            Console.WriteLine("done again");
            Console.ReadKey();

        }
    }
}
