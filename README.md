# OrderContext-EventSourcing
![enter image description here](https://raw.githubusercontent.com/eyazici90/OrderContext-EventSourcing/master/docs/solution_structure.PNG?token=AKPSEUNJ5LWCUIGYAZKZ7AC5MUYLU)

This repository is practical EventSourcing implementation on Azure. It mostly aims to implement CQRS, EventSourcing basics towards Serverless way on Azure. [ImGalaxy](https://github.com/eyazici90/ImGalaxy) is used for the purpose of providing to use Azure Cosmos DB as stream based EventStore and whole concepts of DDD, CQRS, EventSourcing needs. [Change Feed](https://docs.microsoft.com/en-us/azure/cosmos-db/change-feed) is being used for projecting events from [EventStore](https://docs.microsoft.com/en-us/azure/cosmos-db/introduction).

## Prerequisites
 
 - .Net Core 2.2
 - Azure Cosmos DB Instance (You can use [Emulator](https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator) for testing purpose)
 
## Libraries
 - [ImGalaxy](https://github.com/eyazici90/ImGalaxy) for base structure of **Aggregate** modelling and Stream based persistance of **Azure Cosmos DB**
 - [MediatR](https://github.com/jbogard/MediatR) for seperating **command** and **queries**
 - [Microsoft.Azure.WebJobs.Extensions.CosmosDB](https://www.nuget.org/packages/Microsoft.Azure.WebJobs.Extensions.CosmosDB) for connecting Change Feed processor

## Domain Model
![enter image description here](https://raw.githubusercontent.com/eyazici90/OrderContext-EventSourcing/master/docs/domain_model.jpg?token=AKPSEUOW2RFSRD2YRJFC2NK5MUYE2)
 
## Big Picture
![enter image description here](https://raw.githubusercontent.com/eyazici90/OrderContext-EventSourcing/master/docs/big_picture.jpg?token=AKPSEUNE7BQDHL5XQDKXQLS5MUYGS)
