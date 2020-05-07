namespace BooksApi
{
    public class BookstoreDatabaseSettings : IBookstoreDatabaseSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public string BooksCollectionName { get; set; }
    }

    public interface IBookstoreDatabaseSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
        string BooksCollectionName { get; set; }
    }
}