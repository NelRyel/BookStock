using StockEntModelLibrary;
using StockEntModelLibrary.CustumerEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLibrary
{
  public  class CustumerManager
    {
      private static StockDBcontext stockDBcontext = new StockDBcontext();

        //public void CreateCustumer(string CustumerTitle, bool BuyeBuyerTrue_SuplierFalse,
        //    string FullName, string Address, string Phone, string Email
        //    )
        //{
        //    Custumer custumer = new Custumer();
        //    CustumerDescription custumerDescription = new CustumerDescription();
        //    custumer.CustumerTitle = CustumerTitle;
        //    custumer.BuyerTrue_SuplierFalse = BuyeBuyerTrue_SuplierFalse;
        //    custumer.Balance = 0;

        //    custumerDescription.Id = custumer.Id;
        //    custumerDescription.FullName = FullName;
        //    custumerDescription.Address = Address;
        //    custumerDescription.Phone = Phone;
        //    custumerDescription.Email = Email;

        //    stockDBcontext.Custumers.Add(custumer);
        //    stockDBcontext.CustumerDescriptions.Add(custumerDescription);
        //    stockDBcontext.SaveChanges();

        //}
        public void CreateCustumer(Custumer custumer)
        {
                stockDBcontext.Custumers.Add(custumer);
                stockDBcontext.SaveChanges();
        }
        public void CreateCustumerDescription(CustumerDescription custumerDescription)
        {
          
                stockDBcontext.CustumerDescriptions.Add(custumerDescription);
                stockDBcontext.SaveChanges();
        }



        public Custumer GetCustumerById(int id)
        {
          
                Custumer custumer = stockDBcontext.Custumers.Find(id);
                return custumer;
           
        }
        public CustumerDescription GetCustumerDescriptionById(int id)
        {
          
                CustumerDescription custumerDescription = stockDBcontext.CustumerDescriptions.Find(id);
                return custumerDescription;
            
        }

        public IEnumerable<Custumer> GetAllCustumers()//получить всех клиентов
        {
            IEnumerable<Custumer> custumers = stockDBcontext.Custumers.Where(i => i.IsDelete == false);
                return custumers;
             
        }

        public IEnumerable<Custumer> GetAllCustumerBuyers()//получить Покупателей
        {

            IEnumerable<Custumer> custumers = stockDBcontext.Custumers.Where(i => i.IsDelete == false).Where(s => s.BuyerTrue_SuplierFalse == true);
                return custumers;
           
        }
        public IEnumerable<Custumer> GetAllCustumerSupliers()//получить Поставщиков
        {

            IEnumerable<Custumer> custumers = stockDBcontext.Custumers.Where(i => i.IsDelete == false).Where(s => s.BuyerTrue_SuplierFalse == false);
                return custumers;
           
        }

        public void EditCustumer(int id, string CustumerTitle, bool BuyeBuyerTrue_SuplierFalse)
        {
            Custumer custumer = stockDBcontext.Custumers.Find(id);

            custumer.CustumerTitle = CustumerTitle;
            custumer.BuyerTrue_SuplierFalse = BuyeBuyerTrue_SuplierFalse;
            stockDBcontext.SaveChanges();
        }

        public void EditCustumerDesc(int id, string FullName, string address, string phone, string Email)
        {
            CustumerDescription cd = stockDBcontext.CustumerDescriptions.Find(id);
            cd.FullName = FullName;
            cd.Address = address;
            cd.Phone = phone;
            cd.Email = Email;
            stockDBcontext.SaveChanges();

        }

        //public void EditCustumer(int id, string CustumerTitle, bool BuyeBuyerTrue_SuplierFalse,
        //    string FullName, string Address, string Phone, string Email)
        //{
        //        Custumer custumer = stockDBcontext.Custumers.Find(id);
        //        CustumerDescription custumerDescription = stockDBcontext.CustumerDescriptions.Find(custumer.Id);

        //        custumer.CustumerTitle = CustumerTitle;
        //        custumer.BuyerTrue_SuplierFalse = BuyeBuyerTrue_SuplierFalse;
        //        custumer.Balance = 0;
        //        custumerDescription.FullName = FullName;
        //        custumerDescription.Address = Address;
        //        custumerDescription.Phone = Phone;
        //        custumerDescription.Email = Email;


        //        stockDBcontext.SaveChanges();

        //}

        public void ChangeIsDelete(int id, bool IsDelete)
        {
           
                Custumer custumer = stockDBcontext.Custumers.Find(id);
                custumer.IsDelete = (IsDelete == true) ? custumer.IsDelete = false : custumer.IsDelete = true;
                //switch(IsDelete)
                // {
                //     case true:
                //         custumer.IsDelete = false;
                //         break;
                //     case false:
                //         custumer.IsDelete = true;
                //         break;
                // }
                stockDBcontext.SaveChanges();
           
        }

    }
}
