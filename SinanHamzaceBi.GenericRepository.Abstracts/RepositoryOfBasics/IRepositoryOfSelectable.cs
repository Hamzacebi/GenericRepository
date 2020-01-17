using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SinanHamzaceBi.GenericRepository.Abstracts.RepositoryOfBasics
{
    /// <summary>
    /// Herhangi bir typeof(T) tipindeki tablo icin Base Select fonksiyonlarinin oldugu interface
    /// </summary>
    /// <typeparam name="T">Verileri listelenmek istenilen Tablo - Entity</typeparam>
    public interface IRepositoryOfSelectable<T> where T : class
    {
        #region Synchronous Functions

        /// <summary>
        /// Herhangi bir sart (Where) olmadan typeof(T) tipindeki tablo icerisinde olan tum kayitlari listelemeye yarayan fonksiyon
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> FetchAllRecords();

        /// <summary>
        /// typeof(T) tipinde olan tabloda ki kayitlari, istenilen sart / sartlara gore listelemek icin gerekli SQL sorgusunu olusturmaya yarayan fonksiyon
        /// </summary>
        /// <param name="whereConditions">Istenilen verileri elde edecek SQL sorgusu icin gerekli sart / sartlar </param>
        /// <returns></returns>
        IQueryable<T> FetchAllRecords(Expression<Func<T, bool>> whereConditions);

        /// <summary>
        /// typeof(T) tipinde olan tabloda ki tek bir satir veriyi listeleyen fonksiyon
        /// </summary>
        /// <param name="id">Elde edilmek istenilen veriye ait ID bilgisi (Guid, int, short, byte, string vb.)</param>
        /// <returns></returns>
        T FetchAnyRecord(object id);

        /// <summary>
        /// typeof(T) tipinde olan tabloda ki tek bir satir veriyi, istenilen sart / sartlara gore listeleyen fonksiyon
        /// </summary>
        /// <param name="whereConditions">Istenilen veriyi elde etmek icin gerekli sart / sartlar </param>
        /// <returns></returns>
        T FetchAnyRecord(Expression<Func<T, bool>> whereConditions);

        #endregion Synchronous Functions
    }
}
