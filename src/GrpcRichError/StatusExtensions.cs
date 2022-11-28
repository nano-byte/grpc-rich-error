using System.Diagnostics.Contracts;
using Google.Protobuf;
using Grpc.Core;

namespace GrpcRichError;

/// <summary>
/// Extension methods for <see cref="Google.Rpc.Status"/>.
/// </summary>
public static class StatusExtensions
{
    /// <summary>
    /// Extracts an error detail of a specific type if available.
    /// </summary>
    /// <param name="status">The status to extract the detail from.</param>
    /// <typeparam name="T">The type of the detail to extract.</typeparam>
    /// <returns>The extracted error detail if found; <c>null</c> if not found.</returns>
    [Pure]
    public static T? GetDetail<T>(this Google.Rpc.Status status)
        where T : class, IMessage<T>, new() => status.Details?.GetMessage<T>();

    /// <summary>
    /// Builds a <see cref="RpcException"/> from a rich error model.
    /// </summary>
    [Pure]
    public static RpcException ToException(this Google.Rpc.Status status)
        => new RpcException(
            new Status((StatusCode)status.Code, status.Message),
            new Metadata {status});
}
