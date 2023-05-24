using Azure.Data.Tables;
using DemoKeyValue;
using StackExchange.Redis;
using Newtonsoft.Json;


var connectionString = "CNNSTRING";
TableServiceClient tableServiceClient = new TableServiceClient(connectionString);

TableClient tableClient = tableServiceClient.GetTableClient("DatiStradali");
await tableClient.CreateIfNotExistsAsync();


var rediscn = "CNNSTRING";
var dato1 = new DatoStradale
{
    PartitionKey = "A2",
    RowKey = Guid.NewGuid().ToString(),
    Plate = "1234",
    Velocity = 150.0,
    Timestamp = DateTime.Now
};

//var dato2 = new DatoStradaleAvanzato
//{
//    PartitionKey = "A3",
//    RowKey = Guid.NewGuid().ToString(),
//    Code = "1234"
//};

//await tableClient.AddEntityAsync<DatoStradale>(dato1);
//await tableClient.AddEntityAsync<DatoStradaleAvanzato>(dato2);


// Lettura di una singol riga
//var dato = await tableClient.GetEntityAsync<DatoStradale>(
//    rowKey: "92e8a7c4-9131-4523-8562-f5ee4c0aa6a1",
//    partitionKey: "A2");

//if(dato != null)
//{
//    Console.WriteLine($"{dato.Value.Plate}");
//}

//Leggere tutti i record con A2


var result = tableClient.QueryAsync<DatoStradale>(x => x.PartitionKey == "A2");

await foreach (var item in result)
{
    Console.WriteLine($"{item.Velocity}");
}

//Console.WriteLine("Scrittura Terminata");

var cacheConnection = ConnectionMultiplexer.Connect(rediscn);
IDatabase database = cacheConnection.GetDatabase();

//var json = JsonConvert.SerializeObject(dato1);

//var operation =await database.StringSetAsync("dato1", json);
//Console.WriteLine(operation);

var cachedDataJson = await database.StringGetAsync("dato1");

var myData = JsonConvert.DeserializeObject<DatoStradale>(cachedDataJson!);
Console.WriteLine($"{myData.Velocity}");