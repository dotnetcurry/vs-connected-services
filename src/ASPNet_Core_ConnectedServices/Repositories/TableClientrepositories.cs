using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using ASPNet_Core_ConnectedServices.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace ASPNet_Core_ConnectedServices.Repositories
{
    //1.
    public interface ITableRepositories
    {
        void CreateBook(Book bk);
        List<Book> GetBooks();
        Book GetBook(string pKey, string rKey);
    }

    public class TableClientOperationsService : ITableRepositories
    {
        //2.
        CloudStorageAccount storageAccount;
        //3.
        CloudTableClient tableClient;
        //4.
        IConfigurationRoot configs;
        //5.
        public TableClientOperationsService(IConfigurationRoot c)
        {
            this.configs = c;
            var connStr =  this.configs.GetSection("MicrosoftAzureStorage:lmstms_AzureStorageConnectionString");
            storageAccount = CloudStorageAccount.Parse(connStr.Value);
            tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("Book");
            table.CreateIfNotExists();
        }
        //6.
        public void CreateBook(Book bk)
        {
            Random rnd = new Random();
            bk.BookId = rnd.Next(100);
            bk.RowKey = bk.BookId.ToString();
            bk.PartitionKey = bk.Publisher;
            CloudTable table = tableClient.GetTableReference("Book");
            TableOperation insertOperation = TableOperation.Insert(bk);
            table.Execute(insertOperation);
        }
        //7.
        public Book GetBook(string pKey, string rKey)
        {
            Book entity = null;
            CloudTable table = tableClient.GetTableReference("Book");
            TableOperation tableOperation = TableOperation.Retrieve<Book>(pKey, rKey);
            entity = table.Execute(tableOperation).Result as Book;
            return entity;
        }
        //8.
        public List<Book> GetBooks()
        {
            List<Book> Books = new List<Book>();
            CloudTable table = tableClient.GetTableReference("Book");

            TableQuery<Book> query = new TableQuery<Book>();

            foreach (var item in table.ExecuteQuery(query))
            {
                Books.Add(new Book()
                {
                    BookId = item.BookId,
                    BookName = item.BookName,
                    Author = item.Author,
                    Publisher = item.Publisher,
                    Price = item.Price 
                });
            }

            return Books;
        }
    }
}
