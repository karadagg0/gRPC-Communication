# gRPC Demo App

This repository demonstrates various gRPC communication methods using C# and .NET, including unary, server streaming, client streaming, and bidirectional streaming.

## Features
- **Unary Call:** Simple request-response communication.
- **Server Streaming:** Server sends multiple responses for a single request.
- **Client Streaming:** Client sends multiple requests and gets a single response.
- **Bidirectional Streaming:** Both client and server communicate with streams.

## Technologies Used
- .NET 6
- gRPC
- C#
- Visual Studio 2022

## Getting Started

### Prerequisites
- .NET 6 SDK
- Visual Studio 2022 or another compatible IDE

### Setup Instructions
1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/grpc-demo-app.git
   ```
2. Navigate to the project folder:
   ```bash
   cd grpc-demo-app
   ```
3. Build the project:
   ```bash
   dotnet build
   ```
4. Run the server:
   ```bash
   dotnet run --project Server
   ```
5. Run the client:
   ```bash
   dotnet run --project Client
   ```

## Project Structure
```
grpc-demo-app
│   README.md
│   grpc-demo-app.sln
└───Server
│   │   Program.cs
│   │   Services
│   │   Protos
└───Client
    │   Program.cs
    │   Protos
```

## Proto Files
The project includes the following `proto` files:
- **Greeter.proto:** For unary calls.
- **GuidProducer.proto:** For server streaming.
- **SumValues.proto:** For client streaming.
- **NameProducer.proto:** For bidirectional streaming.

## Example Usage

### Unary Call
```bash
> dotnet run --project Client
Hello Test_Name
```

### Server Streaming
```bash
Server Stream:
GUID: 123e4567-e89b-12d3-a456-426614174000
GUID: 123e4567-e89b-12d3-a456-426614174001
```

### Client Streaming
```bash
Sum of Values: 450
```

### Bidirectional Streaming
```bash
Updated Name: James_Updated_42
Updated Name: Olivia_Updated_57
```



**Happy Coding!** 🎉
