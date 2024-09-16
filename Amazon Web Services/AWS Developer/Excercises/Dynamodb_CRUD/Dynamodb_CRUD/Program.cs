using System.Linq.Expressions;
using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using Document = Amazon.DynamoDBv2.DocumentModel.Document;

internal class Program
{
    private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();
    private static string tableName = "ProductCatalog";
    // The sample uses the following id PK value to add book item.
    private static int sampleBookId = 555;
    private static void Main(string[] args)
    {
        Table productCatalog = Table.LoadTable(client, tableName);
        CreateBookItem(productCatalog);
        RetrieveBook(productCatalog);
        // Couple of sample updates.
        UpdateMultipleAttributes(productCatalog);
        UpdateBookPriceConditionally(productCatalog);

        // Delete.
        ///DeleteBook(productCatalog);
        Console.WriteLine("To continue, press Enter");
        Console.ReadLine();
    }

    // Creates a sample book item.
    public static void CreateBookItem(Table productCatalog)
    {
        Console.WriteLine("\n*** Executing CreateBookItem() ***");
        var book = new Amazon.DynamoDBv2.DocumentModel.Document();
        book["Id"] = sampleBookId;
        book["Title"] = "Book " + sampleBookId;
        book["Price"] = 19.99;
        book["ISBN"] = "111-1111111111";
        book["Authors"] = new List<string> { "Author 1", "Author 2", "Author 3" };
        book["PageCount"] = 500;
        book["Dimensions"] = "8.5x11x.5";
        book["InPublication"] = new DynamoDBBool(true);
        book["InStock"] = new DynamoDBBool(false);
        book["QuantityOnHand"] = 0;

        productCatalog.PutItemAsync(book);
    }

    private static void RetrieveBook(Table productCatalog)
    {
        Console.WriteLine("\n*** Executing RetrieveBook() ***");
        // Optional configuration.
        GetItemOperationConfig config = new GetItemOperationConfig
        {
            AttributesToGet = new List<string> { "Id", "ISBN", "Title", "Authors", "Price" },
            ConsistentRead = true
        };
        Document document = productCatalog.GetItemAsync(sampleBookId, config).Result;
        Console.WriteLine("RetrieveBook: Printing book retrieved...");
        PrintDocument(document);
    }

    private static void UpdateMultipleAttributes(Table productCatalog)
    {
        Console.WriteLine("\n*** Executing UpdateMultipleAttributes() ***");
        Console.WriteLine("\nUpdating multiple attributes....");
        int partitionKey = sampleBookId;

        var book = new Document();
        book["Id"] = partitionKey;
        // List of attribute updates.
        // The following replaces the existing authors list.
        book["Authors"] = new List<string> { "Author x", "Author y" };
        book["newAttribute"] = "New Value";
        book["ISBN"] = null; // Remove it.

        // Optional parameters.
        UpdateItemOperationConfig config = new UpdateItemOperationConfig
        {
            // Get updated item in response.
            ReturnValues = ReturnValues.AllNewAttributes
        };
        Document updatedBook = productCatalog.UpdateItemAsync(book, config).Result;
        Console.WriteLine("UpdateMultipleAttributes: Printing item after updates ...");
        PrintDocument(updatedBook);
    }

    private static void UpdateBookPriceConditionally(Table productCatalog)
    {
        Console.WriteLine("\n*** Executing UpdateBookPriceConditionally() ***");

        int partitionKey = sampleBookId;

        var book = new Document();
        book["Id"] = partitionKey;
        book["Price"] = 29.99;

        // For conditional price update, creating a condition expression.
        Amazon.DynamoDBv2.DocumentModel.Expression expr = new Amazon.DynamoDBv2.DocumentModel.Expression();
        expr.ExpressionStatement = "Price = :val";
        expr.ExpressionAttributeValues[":val"] = 19.99;

        // Optional parameters.
        UpdateItemOperationConfig config = new UpdateItemOperationConfig
        {
            ConditionalExpression = expr,
            ReturnValues = ReturnValues.AllNewAttributes
        };
        Document updatedBook = productCatalog.UpdateItemAsync(book, config).Result;
        Console.WriteLine("UpdateBookPriceConditionally: Printing item whose price was conditionally updated");
        PrintDocument(updatedBook);
    }

    private static void DeleteBook(Table productCatalog)
    {
        Console.WriteLine("\n*** Executing DeleteBook() ***");
        // Optional configuration.
        DeleteItemOperationConfig config = new DeleteItemOperationConfig
        {
            // Return the deleted item.
            ReturnValues = ReturnValues.AllOldAttributes
        };
        Document document = productCatalog.DeleteItemAsync(sampleBookId, config).Result;
        Console.WriteLine("DeleteBook: Printing deleted just deleted...");
        PrintDocument(document);
    }

    private static void PrintDocument(Document updatedDocument)
    {
        foreach (var attribute in updatedDocument.GetAttributeNames())
        {
            string stringValue = null;
            var value = updatedDocument[attribute];
            if (value is Primitive)
                stringValue = value.AsPrimitive().Value.ToString();
            else if (value is PrimitiveList)
                stringValue = string.Join(",", (from primitive
                                in value.AsPrimitiveList().Entries
                                                select primitive.Value).ToArray());
            Console.WriteLine("{0} - {1}", attribute, stringValue);
        }
    }
}

