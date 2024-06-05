namespace E_Commerce.Common
{
    public class Credentials
    {
        public static readonly string databaseName = Environment.GetEnvironmentVariable("databaseName");
        public static readonly string containerName = Environment.GetEnvironmentVariable("containerName");
        public static readonly string CosmosEndPoint = Environment.GetEnvironmentVariable("cosmosUrl");
        public static readonly string PrimaryKey = Environment.GetEnvironmentVariable("primaryKey");
        public static readonly string ProductDocumentType = "product";
        public static readonly string CustomerDocumentType = "customer";
        public static readonly string OrderDocumentType = "order";
    }
}
