using Azure;
using Azure.Data.Tables;


namespace DemoKeyValue;

public class DatoStradale : ITableEntity
{
    public string? PartitionKey { get; set; }
    public string? RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }

    public double Velocity { get; set; }
    public string? Plate { get; set; }

}

public class DatoStradaleAvanzato : ITableEntity
{
    public string? PartitionKey { get; set; }
    public string? RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }

    public string? Code { get; set; }

}
