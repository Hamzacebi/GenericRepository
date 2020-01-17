namespace SinanHamzaceBi.GenericRepository.Abstracts.RepositoryOfBasics
{
    /// <summary>
    /// Herhangi bir typeof(T) tipindeki tablo icin Base Insert fonksiyonlarinin oldugu interface
    /// </summary>
    /// <typeparam name="T">Veri eklemek istenilen Tablo - Entity</typeparam>
    public interface IRepositoryOfInsertable<T> where T : class
    {
        #region Synchronous Function(s)

        /// <summary>
        /// typeof(T) tipinde ki tabloya kayit eklemek icin kullanilan fonksiyon
        /// </summary>
        /// <param name="recordToInsert">Eklenmek istenilen kayita ait typeof(T) tipindeki veriler</param>
        /// <returns></returns>
        T InsertRecord(T recordToInsert);

        #endregion Synchronous Function(s)
    }
}
