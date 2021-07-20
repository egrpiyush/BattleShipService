# BattleShip Service

## Architecture
### This service is based on the Clean Architecture. And uses, Domain Driven Design (DDD), Command Query Repository Segregation (CQRS).

#### Design: 
This service is a WebAPI project. For abstraction within Presentation (WebAPI, Azure Functions) and Application layer it uses Command Query Repository Pattern (CQRS). The most important intention behind using CQRS here is to abstract query/command handling from invocation and to keep the function layer clean.

#### Interfaces: 
IBoard: This interface is used as a singleton reference and represents a board. For simplicity, I am keeping track of the BattleShips created and making sure they are within board bounds. All validation to ensure correct BattleShip configuration is in the class AddBattleShipCommandValidator.

#### Arrangements of projects is as follows -
### Domain: 
the inner most layer (standalone) Common: common project for AutoFac wild card implementation. Application: This project comprises of the business logic ie. Queries and Commands. Its depends on Domain layer. Each query in this layer has it's own model classes defined. Application.Tests: From the perspective of structure, this project is a mirror of Application project. It has unit test classes for queries in the Application project. Functions/WebAPIs: this is the outmost layer and exposes the required functions. Validation For service validation FluentValidation is used. All Commands/Queries have their associated validators.

#### Useful links -
##### Clean Architecture - https://www.thinktocode.com/2018/08/16/onion-architecture/
##### Domain Driven Design (DDD) - https://martinfowler.com/tags/domain%20driven%20design.html
##### Command Query Repository Segregation (CQRS) - https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs

## How to build
Please set RestApi project as the starting project.

## Swagger link: https://localhost:5001/swagger/index.html#/BattleShip
