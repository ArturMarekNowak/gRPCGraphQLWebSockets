# gRPC GraphQL WebSockets 

![Build](https://github.com/ArturMarekNowak/gRPCGraphQLWebSockets/actions/workflows/workflow.yml/badge.svg)

The objective of this project is to make a toy example of REST API, gRPC, GraphQL and Websockets all accessing and realizing the same objective: retrieving and sending a simple message to the same database.

## Table of contents
* [General info](#general-info)
* [Screenshots](#screenshots)
* [Technologies](#technologies)
* [Setup](#setup)
* [Features](#features)
* [Status](#status)
* [Inspiration](#inspiration)

## General info
Solution consists of two projects: 

1. gRPCGraphQLWebSockets.csproj - this project contains database along with REST API, gRPC, GraphQL and SignalR server
2. Second item - this project contains SignalR client

After project is build and run the four API's can be accessed via given URL's:

- http://localhost:5000/swagger/index.html - under this URL well-know <em>Swagger</em> documentation documents REST and gRPC endpoints
	- http://localhost:5000/rest 
	- http://localhost:5000/grpc
- http://localhost:5000/graphql - under this URL the <em>Banana Cake Pop</em> tool can be accessed which allows to test GraphQL with ease
- http://localhost:5000/signalr - under this URL the SignalR server operates. Unfortunately no "built-in" tool is implementated to test it. Hence I made a simple SignalR client. But I hope that it changes soon and the SignalR endpoints would be accessible from SwaggerUI.

Solution structure is as following:

    .
    ├── gRPCGraphQLWebSockets   # Server application for REST, GraphQL, SignalR and gRPC
    │	├── Database            # Directory where database context is stored along with SQLite database itself
    │	├── google     			# This directory contains .proto files required to run gRPC, placed in here beacuse otherwise it won't work
    │	├── GraphQL   			# GraphQL mutations, queries and models are stored in here
    │	├── gRPC        		# gRPC service.cs and service.proto files are stored in here
    │	├── Rest         		# REST controller, service and model
    │	├── SignalR    			# SignalR server hub is stored in here
    │	└── SharedModel      	# Models the same throughout the whole solution
    │        
    └── SignalRClient           # Simple SignalR client console application - one file and one .csproj
        └── ...               

## Screenshots
There will be more here, stay tuned!

## Technologies
* REST API 
* gRPC
* GraphQL
* SignalR
* SQLite

## Setup
If you have .NET6 and Visual Studio or JetBrains Rider just open .sln file and try to run the project.

## Code
Code itself i nothing ordinary in my opinion. Just a lot of dependency injection and it works as I wanted. Few things are worth noticing:

- GraphqlQL is so super cool, super powerful and super easy to code :D
- I have used a really <em>UGLY</em> naming convention where classes names start with API name for example: <em>RESTNewMessage</em>, <em>gRPCMessage</em> and so on. It is ugly but I neeeded to differentiate objects in project and wanted sustain object "descriptivity".

## Features
REST, SignalR, GraphQL and gRPC - all in one :D

## To-do list:
All is done :D

## Status
Project is: _in progress_

## Inspiration
I have heard a lot about how cool GraphQL and gRPC are so I have decided to make one toy example and encapsulate it in the same project with REST and SignalR in orderd to get the best possible comparison and visualization.

Since Microsoft documentation on gRPC+Swagger is realy "so so" these two guys were my inspiration in this case: 

https://github.com/grpc/grpc-dotnet/issues/167#issuecomment-830776140

https://github.com/bernardbr/dotnetgrpcrest

https://github.com/fl0wm0ti0n/GrpcWithHttpApiSwaggerAndDocker_Test
