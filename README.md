# Process large volumes of messages with Azure Service Bus & Azure Function

This solution is focused on producing 5k or more messages per few seconds and process them with Azure function consumer within few seconds as well. I am using Azure SB premium and function premium. Azure Function autoscaling is set to scale out from 1 to 5. It was running with one instance only. It did spike in some executions to spin up a second instance but it was only for few minutes. It did auto scale back to 1 instance. Overall excellent results. This will be further tested to process 50k messages within 60 seconds range. 

# DataProducerApp1

This solution file has two projects

Please make sure to update ServiceBusConnectionString to your Azure Service Bus Instance. 

# Data Producer Console App
Data Producer has two files in it. By Default it will push 4000 msgs (10 kb) each within few seconds in batches. 

# Data Producer Consumer App - Azure Function 
Azure Function as Data Consumer App. You must create Azure Premium EP1 function. Set Scale out settings to enforce limits up to 5. 

Overall this solution will process all messages within 10 seconds. 

