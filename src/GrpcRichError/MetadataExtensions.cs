// Copyright Bastian Eicher
// Licensed under the MIT License

using System.Diagnostics.Contracts;
using System.Linq;
using Google.Protobuf;
using Grpc.Core;

namespace GrpcRichError
{
    /// <summary>
    /// Extension methods for <see cref="Metadata"/>.
    /// </summary>
    public static class MetadataExtensions
    {
        /// <summary>
        /// Adds a binary message to the headers.
        /// </summary>
        public static void Add(this Metadata metadata, string key, IMessage message)
            => metadata.Add(key, message.ToByteArray());

        /// <summary>
        /// Returns a message header if available
        /// </summary>
        /// <returns>The deserialized message if found; <c>null</c> if not found.</returns>
        [Pure]
        public static T? GetMessage<T>(this Metadata metadata, string key)
            where T : class, IMessage<T>, new()
        {
            var descriptor = new T().Descriptor;
            var entry = metadata.FirstOrDefault(x => x.Key == key);
            return entry == null ? null : (T)descriptor.Parser.ParseFrom(entry.ValueBytes);
        }

        private const string RichStatusHeaderKey = "grpc-status-details-bin";

        /// <summary>
        /// Adds a rich error model to the headers.
        /// </summary>
        public static void Add(this Metadata metadata, Google.Rpc.Status status)
            => metadata.Add(RichStatusHeaderKey, status);

        /// <summary>
        /// Returns the rich error model if available.
        /// </summary>
        /// <returns>The rich error model if found; <c>null</c> if not found.</returns>
        [Pure]
        public static Google.Rpc.Status? GetRichStatus(this Metadata metadata)
            => metadata.GetMessage<Google.Rpc.Status>(RichStatusHeaderKey);
    }
}
