# DataProducerApp1

This solution file has two projects

Please make sure to update ServiceBusConnectionString to your Azure Service Bus Instance. 

# Data Producer Console App
Data Producer has two files in it. By Default it will push 4000 msgs (10 kb) each within few seconds in batches. 

# Data Producer Consumer App - Azure Function 
Azure Function as Data Consumer App. You must create Azure Premium EP1 function. Set Scale out settings to enforce limits up to 5. 

Overall this solution will process all messages within 10 seconds. 

