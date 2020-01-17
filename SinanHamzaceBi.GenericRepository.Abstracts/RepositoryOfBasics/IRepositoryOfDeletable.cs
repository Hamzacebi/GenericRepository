namespace SinanHamzaceBi.GenericRepository.Abstracts.RepositoryOfBasics
{
    /// <summary>
    /// Herhangi bir typeof(T) tipindeki tablo icin Base Delete fonksiyonlarinin oldugu interface
    /// </summary>
    /// <typeparam name="T">Verisi silinmek istenilen Tablo - Entity</typeparam>
    public interface IRepositoryOfDeletable<T> where T : class
    {
        #region Synchronous Function(s)

        /// <summary>
        /// typeof(T) tipindeki tabloya ait bir satir veriyi kalici olarak silmeyi saglayan fonkisyon
        /// </summary>
        /// <param name="idOfTheItemToBeDeleted">Kalici olarak silinmek istenilen kayita ait ID bilgisi (Guid, int, short, string vb.)</param>
        /// <returns></returns>
        bool DeletePermanentRecord(object idOfTheItemToBeDeleted);

        #endregion Synchronous Function(s)
    }
}
