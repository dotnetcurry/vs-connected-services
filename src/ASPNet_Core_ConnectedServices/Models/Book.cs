using Microsoft.WindowsAzure.Storage.Table;

namespace ASPNet_Core_ConnectedServices.Models
{
    public class Book : TableEntity
    {
        public Book()
        {
        }

        public Book(int bookid, string publisher)
        {
            this.RowKey = bookid.ToString();
            this.PartitionKey = publisher;
        }

        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public int Price { get; set; }
        public string Publisher { get; set; }
    }
}
