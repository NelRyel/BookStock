using StockEntModelLibrary.BookEnt;
using StockEntModelLibrary.CustumerEnt;
using StockEntModelLibrary.Document;
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

    class DefaultInit : DropCreateDatabaseAlways<StockDBcontext>
    {
        protected override void Seed(StockDBcontext context)
        {
            // base.Seed(context);
            //CustumerDefaultCreate
            #region
            Custumer custumerDefaultSuplier = new Custumer()
            {
                Id = 1,
                CustumerTitle = "Основной Поставщик",
                Balance = 0,
                BuyerTrue_SuplierFalse = false,
                CustumerDescriptionId=1

            };
            context.Custumers.Add(custumerDefaultSuplier);
            CustumerDescription custumerDescriptionDefaultSuplier = new CustumerDescription()
            {
                Id=1,
                FullName="Полное имя поставщика",
                Address="какойто адресс поставщика",
                Phone="21321412",
                Email="suplier@mail.ru"
            };
            context.CustumerDescriptions.Add(custumerDescriptionDefaultSuplier);
            context.SaveChanges();
            Custumer custumerDefaultBuyer = new Custumer()
            {
                Id=2,
                CustumerTitle = "Основной Покупатель",
                Balance = 0,
                BuyerTrue_SuplierFalse = true,
                CustumerDescriptionId=2

            };
            context.Custumers.Add(custumerDefaultBuyer);
            CustumerDescription custumerDescriptionDefaultBuyer = new CustumerDescription()
            {
                Id = 2,
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
            Book book2 = new Book()
            {
                BookTitle = "Врата Штейна. Том 1",
                BarcodeISBN = "9785919960638",
                Count = 1,
                PurchasePrice = 140,
                RetailPrice = 140 * 2,
                fullDescriptionId = 2
            };
            Book book3 = new Book()
            {
                BookTitle= "Врата Штейна. Том 2",
                BarcodeISBN= "9785919960720",
                Count=0,
                PurchasePrice=142,
                RetailPrice=142*2,
                fullDescriptionId=3
                
            };


            context.Books.Add(book1);
            context.Books.Add(book2);
            context.Books.Add(book3);
            context.SaveChanges();
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
            BookFullDescription bookFullDescription2 = new BookFullDescription()
            {
                Id = book2.Id,
                FirstYearBookPublishing= "2015",
                YearBookPublishing = "2018",
                Serie = "Манга",
                Section = "Триллер, Фантастика",
                Description= "Окабэ Ринтаро — или просто Окарин — молодой студент, который до сих пор " +
                "живет в мире детских фантазий. Он занимается придумыванием различных " +
                "бесполезных изобретений вместе с двумя товарищами по клубу, который они гордо называют Лабораторией Гаджетов Будущего. " +
                "Но однажды он встречает девушку-гения, Курису Макисэ, и случайно изобретает машину времени, способную посылать " +
                "сообщения в прошлое. Привычная повседневность начинает потихоньку расходиться по швам, " +
                "а главный герой оказывается вовлечён в заговор мирового масштаба!..",
                Author = "Сарати Е.",
                Publisher = "ЭксЭл Медиа",
                ImageUrl= "https://img-gorod.ru/web/247/2470072/jpg/2470072_detail.jpg"

            };
            BookFullDescription bookFullDescription3 = new BookFullDescription()
            {
                Id=book3.Id,
                FirstYearBookPublishing="2015",
                Serie="Манга",
                Section= "Триллер, Фантастика",
                Description = "Окабэ Ринтаро — или просто Окарин — молодой студент, который до сих пор " +
                "живет в мире детских фантазий. Он занимается придумыванием различных " +
                "бесполезных изобретений вместе с двумя товарищами по клубу, который они гордо называют Лабораторией Гаджетов Будущего. " +
                "Но однажды он встречает девушку-гения, Курису Макисэ, и случайно изобретает машину времени, способную посылать " +
                "сообщения в прошлое. Привычная повседневность начинает потихоньку расходиться по швам, " +
                "а главный герой оказывается вовлечён в заговор мирового масштаба!..",
                Author = "Сарати Е.",
                Publisher = "ЭксЭл Медиа",
                ImageUrl = "https://img-gorod.ru/web/248/2488088/jpg/2488088_detail.jpg"

            };

            context.BookFullDescriptions.Add(bookFullDescription1);
            context.BookFullDescriptions.Add(bookFullDescription2);
            context.BookFullDescriptions.Add(bookFullDescription3);
            PurchaseDoc purchaseDoc1 = new PurchaseDoc()
            {
                //Id = custumerDefaultSuplier.Id,
                Custumer = custumerDefaultSuplier,
                DateCreate = DateTime.Today,
                DateOfLastChangeStatus = DateTime.Today,
                Status = StaticDatas.DocStatuses.Непроведен.ToString(),
                Comment = "",
                FullSum = 535,
                CustumerId = custumerDefaultSuplier.Id

            };
            context.PurchaseDocs.Add(purchaseDoc1);
            context.SaveChanges();

            PurchaseDocRec purchaseDocRec1 = new PurchaseDocRec()
            {
                LineNumber = 1,
                Count = 1,
                PurchasePrice = 535,
                RetailPrice = 1070,
                SumPrice = 535,
                PurchaseDoc = purchaseDoc1,
                PurchaseDocId = purchaseDoc1.Id,
                Book = book1,
                BookId = book1.Id
            };

            context.PurchaseDocRecs.Add(purchaseDocRec1);
            context.SaveChanges();


            SaleDoc saleDoc1 = new SaleDoc()
            {
                Custumer = custumerDefaultBuyer,
                DateCreate = DateTime.Today,
                DateOfLastChangeStatus = DateTime.Today,
                Status = StaticDatas.DocStatuses.Непроведен.ToString(),
                Comment = "",
                FullSum=535,
                CustumerId = custumerDefaultBuyer.Id
            };

            context.SaleDocs.Add(saleDoc1);
            context.SaveChanges();

            SaleDocRec saleDocRec1 = new SaleDocRec()
            {
                LineNumber = 1,
                Count = 1,
                RetailPrice = 535,
                SaleDoc = saleDoc1,
                SaleDocId = saleDoc1.Id,
                Book = book1,
                BookId = book1.Id
            };

            context.SaleDocRecs.Add(saleDocRec1);
            context.SaveChanges();

            context.SaveChanges();

            
        }
    }
}
