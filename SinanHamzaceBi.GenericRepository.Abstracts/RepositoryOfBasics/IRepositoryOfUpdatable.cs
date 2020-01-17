namespace SinanHamzaceBi.GenericRepository.Abstracts.RepositoryOfBasics
{
    /// <summary>
    /// Herhangi bir typeof(T) tipindeki tablo icin Base Update fonksiyonlarinin oldugu interface
    /// </summary>
    /// <typeparam name="T">Verileri guncellenmek istenilen Tablo - Entity</typeparam>
    public interface IRepositoryOfUpdatable<T> where T : class
    {
        #region Synchronous Function(s)

        /// <summary>
        /// typeof(T) tipinde ki tabloya ait bir satir verinin guncellenmesi icin kullanilan fonksiyon
        /// </summary>
        /// <param name="recordToUpdate">Guncellenmek istenilen kayita ait typeof(T) tipindeki guncel veriler</param>
        /// <returns></returns>
        T UpdateRecord(T recordToUpdate);

        #endregion
    }
}
