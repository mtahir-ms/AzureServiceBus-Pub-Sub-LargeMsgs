// See https://aka.ms/new-console-template for more information
using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;


    // connection string to your Service Bus namespace
     string connectionString = "Endpoint=sb://testsbmoonis123.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=ES/i2ZX9EFBHfbj1K8rCUzgPaGWBL6EoZ+ASbNZxL0I=";

    // name of your Service Bus queue
     string queueName = "queue1";


        // create a Service Bus client 
        await using (var client = new ServiceBusClient(connectionString))
        {
            // create a sender for the queue
            ServiceBusSender sender = client.CreateSender(queueName);

            try
            {
                // create a message that you plan to send to the queue
                ServiceBusMessage message = new ServiceBusMessage("Hello, World!");

                Console.WriteLine($"Sending message: {message.Body}");

                // send the message to the queue
                await sender.SendMessageAsync(message);

                Console.WriteLine("Message sent successfully.");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }

    //Total of 4000 msgs will be posted to ServiceBus Queue. Each MSG size is 10kb
    int outloopcounter = 200;
    for (int o = 1; o <= outloopcounter; o++)
    {
         
        // create a batch 
        using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();
        // number of messages to be sent to the queue
        const int numOfMessages = 20;
        //large file should run as 20 msgs inside and outercounter as 200
        string filePath = "largedatafile1.json";

        //small file should run as 500 msgs inside and outercounter as 10
        //string filePath = "datainput.json";
        for (int i = 1; i <= numOfMessages; i++)
        {
            string jsonString = File.ReadAllText(filePath);

            // try adding a message to the batch
            if (!messageBatch.TryAddMessage(new ServiceBusMessage($"Message {i}-" + jsonString)))
            {
                // if it is too large for the batch
                throw new Exception($"The message {i} is too large to fit in the batch.");
            }
        }

        try
        {
            // Use the producer client to send the batch of messages to the Service Bus queue
            await sender.SendMessagesAsync(messageBatch);
            Console.WriteLine($"A batch of {numOfMessages} messages has been published to the queue.");
        }
        finally
        {
            // Calling DisposeAsync on client types is required to ensure that network
            // resources and other unmanaged objects are properly cleaned up.
           // await sender.DisposeAsync();
           // await client.DisposeAsync();
        }
    }
     await sender.DisposeAsync();
     await client.DisposeAsync();
}



Console.WriteLine("Press any key to end the application");
Console.ReadKey();
