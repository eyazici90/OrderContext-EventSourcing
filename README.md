

# OrderContext-EventSourcing
![enter image description here](https://github.com/eyazici90/OrderContext-EventSourcing/blob/master/docs/solution_structure.PNG)

This repository is practical EventSourcing implementation on Azure. It mostly aims to implement CQRS, EventSourcing basics towards Serverless way on Azure. [ImGalaxy](https://github.com/eyazici90/ImGalaxy) is used for the purpose of providing to use Azure Cosmos DB as stream based EventStore and whole concepts of DDD, CQRS, EventSourcing needs. [Change Feed](https://docs.microsoft.com/en-us/azure/cosmos-db/change-feed) is being used for projecting events from [EventStore](https://docs.microsoft.com/en-us/azure/cosmos-db/introduction).

## Prerequisites
 
 - .Net Core 2.2
 - Azure Cosmos DB Instance (You can use [Emulator](https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator) for testing purpose)
 
## Libraries
 - [ImGalaxy](https://github.com/eyazici90/ImGalaxy) for base structure of **Aggregate** modelling and Stream based persistance of **Azure Cosmos DB**
 - [MediatR](https://github.com/jbogard/MediatR) for seperating **command** and **queries**
 - [Microsoft.Azure.WebJobs.Extensions.CosmosDB](https://www.nuget.org/packages/Microsoft.Azure.WebJobs.Extensions.CosmosDB) for connecting Change Feed processor
 
 
## Big Picture

![enter image description here](https://github.com/eyazici90/OrderContext-EventSourcing/blob/master/docs/big_picture.jpg)

## Requests processing CQRS
Command and Query model are separated by applying  CQRS (Command Query Responsibility Segregation) technic.

![enter image description here](https://github.com/eyazici90/OrderContext-EventSourcing/blob/master/docs/command_workflow.jpg)


## Solution Description
*Command Side*
 - **OrderContext.Application** - Commands and CommandHandlers are within this layer. It basicly handles commands and call that related behavoir in the domain model.
 - **OrderContext.Command.API** - REST API application. Very thin, hosting Asp.Net Core application.
 
*Query Side*
 - **OrderContext.Projections**- Azure function which is connected to Azure Cosmos DB Change feed. It listens the change feed & project events for real model.
 - **OrderContext.Query.API**- REST API application. Builds InMemory read model for OrderContext also  includes query handler for client needs.
 
 *Shared*
 
 - **OrderContext.Domain**- Domain model around Order bounded context.
 - **OrderContext.Domain.Messages**- Domain events that can be happen in the Order context.
 - **OrderContext.Integration.Events**- Events which are getting published to EventBus to communicate with other bounded contexts.
 
 *Background Functions*
 
  *They act as async processes which are connected to Azure CosmosDB Change Feed*
 
 - **OrderContext.Integration.Publisher**- Azure function which is connected to Azure Cosmos DB Change feed. It tends to publish essential integration events to EventBus.
 - **OrderContext.Snapshotter**- Azure function which is connected to Azure Cosmos DB Change feed. It takes snapshots of domain model as the snapshot strategy is defined.

## Deploy

....
 

