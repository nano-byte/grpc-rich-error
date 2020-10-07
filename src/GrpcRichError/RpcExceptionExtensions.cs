// Copyright Bastian Eicher
// Licensed under the MIT License

using System.Diagnostics.Contracts;
using Google.Protobuf;
using Grpc.Core;

namespace GrpcRichError
{
    /// <summary>
    /// Extension methods for <see cref="RpcException"/>.
    /// </summary>
    public static class RpcExceptionExtensions
    {
        /// <summary>
        /// Returns the rich error model if available.
        /// </summary>
        /// <returns>The rich error model if found; <c>null</c> if not found.</returns>
        [Pure]
        public static Google.Rpc.Status? GetRichStatus(this RpcException exception)
            => exception.Trailers.GetRichStatus();

        /// <summary>
        /// Extracts an error detail of a specific type if available.
        /// </summary>
        /// <param name="exception">The error to extract the detail from.</param>
        /// <typeparam name="T">The type of the detail to extract.</typeparam>
        /// <returns>The extracted error detail if found; <c>null</c> if not found.</returns>
        [Pure]
        public static T? GetDetail<T>(this RpcException exception)
            where T : class, IMessage<T>, new()
            => exception.GetRichStatus()?.Details.GetMessage<T>();
    }
}
