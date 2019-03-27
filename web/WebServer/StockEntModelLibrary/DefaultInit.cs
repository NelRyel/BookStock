using StockEntModelLibrary.BookEnt;
using StockEntModelLibrary.CustumerEnt;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockEntModelLibrary
{
    //BookDb
    //DropCreateDatabaseAlways - пересоздаёт всю базу при каждом запуске
    //DropCreateDatabaseIfModelChanges - пересоздаёт если были изменения
    //CreateDatabaseIfNotExists - создаёт БД еси её нет

    class DefaultInit : DropCreateDatabaseIfModelChanges<StockDBcontext>
    {
        protected override void Seed(StockDBcontext context)
        {
            // base.Seed(context);
            //CustumerDefaultCreate
            #region
            Custumer custumerDefaultSuplier = new Custumer()
            {
                CustumerTitle = "Основной поставщик",
                Balance = 0,
                BuyerTrue_SuplierFalse = false,

            };
            context.Custumers.Add(custumerDefaultSuplier);
            CustumerDescription custumerDescriptionDefaultSuplier = new CustumerDescription()
            {
                Id=custumerDefaultSuplier.Id,
                FullName="Полное имя поставщика",
                Address="какойто адресс поставщика",
                Phone="21321412",
                Email="suplier@mail.ru"
            };
            context.CustumerDescriptions.Add(custumerDescriptionDefaultSuplier);
            context.SaveChanges();
            Custumer custumerDefaultBuyer = new Custumer()
            {
                CustumerTitle = "Основной Покупатель",
                Balance = 0,
                BuyerTrue_SuplierFalse = true,
                CustumerDescriptionId=1

            };
            context.Custumers.Add(custumerDefaultBuyer);
            CustumerDescription custumerDescriptionDefaultBuyer = new CustumerDescription()
            {
                Id = custumerDefaultBuyer.Id,
                FullName = "Полное имя покупателя",
                Address = "какойто адресс",
                Phone = "6547546546",
                Email = "hell@mail.ru"
            };
            context.CustumerDescriptions.Add(custumerDescriptionDefaultBuyer);
            #endregion

            Book book1 = new Book()
            {
                BookTitle = "Dark Souls. Зимняя злоба",
                BarcodeISBN = "9785171064006",
                Count = 1,
                PurchasePrice = 535,
                RetailPrice = 535*2,
                fullDescriptionId=1
                
            };
            context.Books.Add(book1);
            BookFullDescription bookFullDescription1 = new BookFullDescription()
            {
                Id = book1.Id,
                FirstYearBookPublishing = "2018",
                YearBookPublishing = "2018",
                Serie = "Dark Souls",
                Section= "ФАНТАСТИКА, ФЭНТЕЗИ, МИСТИКА",
                Description= "Андред из Итвейла – храбрый воин, заточённый в " +
                "царстве вечной зимы.Ведомый священным долгом,он пытается вырваться из " +
                "ледяной тюрьмы и отправиться на выполнение своей кровавой миссии." +
                "С боем он пробивает себе путь через мёрзлые пустоши,наполненные мертвецами,чтобы вернуть украденную реликвию своего рода. " +
                "Вторая потрясающе красивая и жуткая история от авторов графического романа - бестселлера «Dark Souls: Дыхание Андолуса» по мотивам популярной серии игр Dark Souls.",
                Author = "Манн Джордж",
                Publisher = "ООО \"Издательство АСТ\"",
                ImageUrl = "http://cdn.eksmo.ru/v2/ASE000000000834234/COVER/cover1__w600.jpg"
            };
            context.BookFullDescriptions.Add(bookFullDescription1);
            context.SaveChanges();

            
        }
    }
}
