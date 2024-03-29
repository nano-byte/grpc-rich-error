# gRPC Rich Error

[![Build](https://github.com/nano-byte/grpc-rich-error/workflows/Build/badge.svg?branch=master)](https://github.com/nano-byte/grpc-rich-error/actions?query=workflow%3ABuild)
[![NuGet](https://img.shields.io/nuget/v/GrpcRichError.svg)](https://www.nuget.org/packages/GrpcRichError/)
[![API documentation](https://img.shields.io/badge/api-docs-orange.svg)](https://grpc-rich-error.nano-byte.net/)  
Adds support for the [richer gRPC error model](https://grpc.io/docs/guides/error/#richer-error-model) to [gRPC for .NET](https://github.com/grpc/grpc-dotnet) via extension methods.

## Usage

Add a reference to the [`GrpcRichError`](https://www.nuget.org/packages/GrpcRichError/) package to your project and use the namespace:

```csharp
using GrpcRichError;
```

To throw an RPC exception with rich error details:

```csharp
throw new Google.Rpc.Status
{
    Code = (int)StatusCode.NotFound,
    Message = "some message",
    Details =
    {
        new ErrorInfo
        {
            Domain = "example.com",
            Reason = "some reason"
        },
        new BadRequest
        {
            // ...
        }
    }
}.ToException();
```

To extract rich error details from an RPC exception:

```csharp
try
{
    // ...
}
catch (RpcException ex)
{
    var info = ex.GetDetail<ErrorInfo>();
    if (info != null)
    {
        // ...
    }
}
```

## Building

The source code is in [`src/`](src/), config for building the API documentation is in [`doc/`](doc/) and generated build artifacts are placed in `artifacts/`. The source code does not contain version numbers. Instead the version is determined during CI using [GitVersion](https://gitversion.net/).

To build run `.\build.ps1` or `./build.sh` (.NET SDK is automatically downloaded if missing using [0install](https://0install.net/)).

## Contributing

We welcome contributions to this project such as bug reports, recommendations and pull requests.

This repository contains an [EditorConfig](http://editorconfig.org/) file. Please make sure to use an editor that supports it to ensure consistent code style, file encoding, etc.. For full tooling support for all style and naming conventions consider using JetBrains' [ReSharper](https://www.jetbrains.com/resharper/) or [Rider](https://www.jetbrains.com/rider/) products.
