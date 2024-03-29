---
title: Home
---

# gRPC Rich Error

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
