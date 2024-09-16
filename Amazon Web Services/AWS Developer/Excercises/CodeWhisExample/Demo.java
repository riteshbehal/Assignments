    Public class Demo
    {
        // main method
        public static void main(String[] args)
        {
            System.out.println("Hello World!");
        }

        // method to add two numbers 
        public static int add(int a, int b)
        {
            return a + b;
        }

        // method to subtract two numbers
        public static int subtract(int a, int b)
        {
            return a - b;
        }

        // C# code to fetch data from Microsoft SQL 
        // using System.Data.SqlClient;
        public static void fetchFromSQL()
        {
            String connectionString = "Data Source=MYDBSERVER;Initial Catalog=M sd3zYDATABASE;User ID=MYUSERNAME;Password=MYPASSWORD";
            String query = "SELECT * FROM MYTABLE";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader["ColumnName1"] + " " + reader["ColumnName2"]);
                }
                reader.Close();
            }
        }

        // C# Code to create AWS S3 Bucket 
        // using Amazon.S3;
        // using Amazon.S3.Model;
        public static void createS3Bucket()
        {
            AmazonS3Client client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);
            string bucketName = "fautukapareshan";
            PutBucketRequest request = new PutBucketRequest
            {
                BucketName = bucketName,
                UseClientRegion = true
            };
            client.PutBucketAsync(request).Wait();
        }

        // C# Code to fetch data from AWS DynamoDB
        // using Amazon.DynamoDBv2;
        // using Amazon.DynamoDBv2.Model;


    }