using System;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Collections.Generic;

#region Global Custom Usings
using Microsoft.EntityFrameworkCore;
using SinanHamzaceBi.GenericRepository.Abstracts.RepositoryOfBasics;
#endregion Global Custom Usings

namespace SinanHamzaceBi.GenericRepository.Concretes.RepositoryOfBasics
{
    /// <summary>
    /// SinanHamzaceBi.GenericRepository.Abstract NuGet'inde olan IRepositoryOfSelectable, IRepositoryOfInsertable, IRepositoryOfUpdatable
    /// ve IRepositoryOfDeletable adli interface'ler icerisinde olan Synchronous ve Asynchronous fonksiyonlarin implementationlarinin
    /// yapildigi Base Class
    /// </summary>
    /// <typeparam name="T">Base fonksiyonlarin uzerinde islem yapacak oldugu typeof(T) Entity - Tablo </typeparam>
    public abstract class RepositoryBase<T> : IRepositoryOfSelectable<T>, IRepositoryOfInsertable<T>,
                                              IRepositoryOfUpdatable<T>, IRepositoryOfDeletable<T>
                                              where T : class
    {
        #region Global Properties

        protected DbSet<T> DbSet { get; private set; }
        protected DbContext DbContext { get; private set; }

        #endregion Global Properties


        #region Constructor(s)

        protected RepositoryBase(DbContext dbContext)
        {
            this.DbContext = dbContext ?? throw new ArgumentNullException(message: "DbContext object cannot be empty!",
                                                                          innerException: null);
            this.DbSet = this.DbContext.Set<T>();
        }

        #endregion Constructor(s)


        #region Select Functions

        #region Synchronous Functions

        /// <summary>
        /// Herhangi bir sart (Where) olmadan typeof(T) tipindeki tablo icerisinde olan tum kayitlari listelemeye yarayan fonksiyon
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> IRepositoryOfSelectable<T>.FetchAllRecords()
        {
            try
            {
                return this.DbSet.AsEnumerable();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// typeof(T) tipinde olan tabloda ki kayitlari, istenilen sart / sartlara gore listelemek icin gerekli SQL sorgusunu olusturmaya yarayan fonksiyon
        /// </summary>
        /// <param name="whereConditions">Istenilen verileri elde edecek SQL sorgusu icin gerekli sart / sartlar </param>
        /// <returns></returns>
        IQueryable<T> IRepositoryOfSelectable<T>.FetchAllRecords(Expression<Func<T, bool>> whereConditions)
        {
            return this.DbSet.Where(predicate: whereConditions)
                             .AsQueryable();
        }

        /// <summary>
        /// typeof(T) tipinde olan tabloda ki tek bir satir veriyi listeleyen fonksiyon
        /// </summary>
        /// <param name="id">Elde edilmek istenilen veriye ait ID bilgisi (Guid, int, short, byte, string vb.)</param>
        /// <returns></returns>
        T IRepositoryOfSelectable<T>.FetchAnyRecord(object id)
        {
            try
            {
                return this.DbSet.Find(keyValues: id);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// typeof(T) tipinde olan tabloda ki tek bir satir veriyi, istenilen sart / sartlara gore listeleyen fonksiyon
        /// </summary>
        /// <param name="whereConditions">Istenilen veriyi elde etmek icin gerekli sart / sartlar </param>
        /// <returns></returns>
        T IRepositoryOfSelectable<T>.FetchAnyRecord(Expression<Func<T, bool>> whereConditions)
        {
            try
            {
                return this.DbSet.SingleOrDefault(predicate: whereConditions);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        #endregion Synchronous Functions

        #endregion Select Functions


        #region Insert Functions

        #region Synchronous Functions

        /// <summary>
        /// typeof(T) tipinde ki tabloya kayit eklemek icin kullanilan fonksiyon
        /// </summary>
        /// <param name="recordToInsert">Eklenmek istenilen kayita ait typeof(T) tipindeki veriler</param>
        /// <returns></returns>
        T IRepositoryOfInsertable<T>.InsertRecord(T recordToInsert)
        {
            return this.AttachItemToDbSet(attachState: EntityState.Added, attachToItem: recordToInsert);
        }

        #endregion Synchronous Functions

        #endregion Insert Functions


        #region Update Functions

        #region Synchronous Functions

        /// <summary>
        /// typeof(T) tipinde ki tabloya ait bir satir verinin guncellenmesi icin kullanilan fonksiyon
        /// </summary>
        /// <param name="recordToUpdate">Guncellenmek istenilen kayita ait typeof(T) tipindeki guncel veriler</param>
        /// <returns></returns>
        T IRepositoryOfUpdatable<T>.UpdateRecord(T recordToUpdate)
        {
            return this.AttachItemToDbSet(attachToItem: recordToUpdate, attachState: EntityState.Modified);
        }

        #endregion Synchronous Functions

        #endregion Update Functions


        #region Delete Functions

        #region Synchronous Functions

        /// <summary>
        /// typeof(T) tipindeki tabloya ait bir satir veriyi kalici olarak silmeyi saglayan fonkisyon
        /// </summary>
        /// <para>NOT : Verinin tablodan silinmesi icin bu fonksiyonu cagirdiginiz yerde SaveChanges() fonksiyonunu cagirmaniz gerekmektedir.</para>
        /// <param name="idOfTheItemToBeDeleted">Kalici olarak silinmek istenilen kayita ait ID bilgisi (Guid, int, short, string vb.)</param>
        /// <returns></returns>
        T IRepositoryOfDeletable<T>.DeletePermanentRecord(object idOfTheItemToBeDeleted)
        {
            try
            {
                T findTheItemToDelete = this.DbSet.Find(keyValues: idOfTheItemToBeDeleted);
                return this.AttachItemToDbSet(attachToItem: findTheItemToDelete, attachState: EntityState.Deleted);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        #endregion Synchronous Functions

        #endregion Delete Functions


        #region Private Function

        /// <summary>
        /// Insert - Update - Delete durumlari icin degisken olan EntityState degeri sebebiyle tekrar eden fonksiyon tek hale getirildi.
        /// </summary>
        /// <param name="attachToItem">DbSet uzerine eklenmek istenilen typeof(T) tipindeki entity</param>
        /// <param name="attachState">DbSet uzerine eklenen entity icin yapilmak istenilen islem tipi</param>
        /// <returns></returns>
        private T AttachItemToDbSet(T attachToItem, EntityState attachState)
        {
            var attachedItem = this.DbSet.Attach(entity: attachToItem);
            attachedItem.State = attachState;
            return attachedItem.Entity;
        }


        #endregion Private Function
    }
}