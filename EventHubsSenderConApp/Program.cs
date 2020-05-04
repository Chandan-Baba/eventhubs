using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using System;
using System.Text;
using System.Threading.Tasks;

namespace EventHubsSenderConApp
{
  class Program
  {
    private const string connectionString = "Endpoint=sb://cbeventhub.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=2S9+0XKerQRTrkPJrHz07Wa3LeBqfdn0nkQQyMPnWoM=";
    private const string eventHubName = "myeventhub1";
    static async Task Main(string[] args)
    {
      // Create a producer client that you can use to send events to an event hub
      await using (var producerClient = new EventHubProducerClient(connectionString, eventHubName))
      {
        // Create a batch of events 
        using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

        // Add events to the batch. An event is a represented by a collection of bytes and metadata.
        eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("First Event")));
        eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("Second Event")));
        eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes("Third Event")));

        // Use the producer client to send the batch of events to the event hub
        await producerClient.SendAsync(eventBatch);
        Console.WriteLine("A batch of 3 events has been published... press any key to exit");
        Console.ReadKey(true);
      }
    }
  }
}
