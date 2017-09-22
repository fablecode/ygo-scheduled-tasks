![alt text](https://fablecode.visualstudio.com/_apis/public/build/definitions/5e161f07-a46a-4666-8db7-13a264516d97/5/badge?maxAge=0 "Visual studio team services build status") 

# Ygo-scheduled-tasks
A set of scheduled tasks for aggregate Yu-Gi-Oh related data from various sources.

## Why?
To provide access to the latest [Yu-Gi-Oh](http://www.yugioh-card.com/uk/)  banlist & card errata in a simple JSON format, via [ygo-api](https://github.com/fablecode/ygo-api).

## Prerequisite
1. Setup the [Ygo database](https://github.com/fablecode/ygo-database)
2. For the web api, download and run [Ygo-api](https://github.com/fablecode/ygo-api)

## Installing
```
 $ https://github.com/fablecode/ygo-scheduled-tasks.git
```

## ApiUrl
Modify the **apiUrl** in the app.config.You can get the web api url by running the solution in **step 2**.

## Built With
* [Visual Studio 2017](https://www.visualstudio.com/downloads/)
* [.NET 4.6.2](https://www.microsoft.com/en-gb/download/details.aspx?id=53345)
* [Onion Architecture](http://jeffreypalermo.com/blog/the-onion-architecture-part-1/) and [CQRS](https://martinfowler.com/bliki/CQRS.html)
* [Structuremap](https://github.com/structuremap/structuremap)
* [Topshelf](https://github.com/Topshelf/Topshelf)
* [Topshelf.StructureMap](https://github.com/swimtver/Topshelf.StructureMap)
* [TPL Dataflow Library](https://www.nuget.org/packages/Microsoft.Tpl.Dataflow/) for message passing **dataflow** and **pipelining**.
* [Mediatr](https://www.nuget.org/packages/MediatR/) for CQRS and the Mediator Design Pattern. Mediator design pattern defines how a set of objects interact with each other. You can think of a Mediator object as a kind of traffic-coordinator, it directs traffic to appropriate parties.
* [Wikia](https://github.com/fablecode/wikia)
* [Fluent Validations](https://www.nuget.org/packages/FluentValidation)
* [Fluent Assertions](https://www.nuget.org/packages/FluentAssertions)
* [NUnit](https://github.com/nunit/nunit)
* [Visual Studio Team Services](https://www.visualstudio.com/team-services/release-management/) for CI and deployment.

## TPL Dataflow flow
 Below is a TPL dataflow pipeline for processing **Articles Batches** 
 
![TPL Dataflow pipeline for Article Batches](/assets/images/tpl%20dataflow.png?raw=true "TPL Dataflow pipeline for Article Batches")
 
 The solution will have a single point of input. **Article Batch Processor** to processor the batches and place article in queue. Depending on the article category, one of the **Processors** will process the article. Lastly, article data is then persisted to storage (SQL Server).
 
## License
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE) file for details.
