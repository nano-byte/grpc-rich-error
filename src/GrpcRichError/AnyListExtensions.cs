// Copyright Bastian Eicher
// Licensed under the MIT License

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;

namespace GrpcRichError
{
    /// <summary>
    /// Extension methods for lists of <see cref="Any"/>.
    /// </summary>
    public static class AnyListExtensions
    {
        /// <summary>
        /// Adds a message to an <see cref="Any"/> list.
        /// </summary>
        public static void Add(this RepeatedField<Any> anyList, IMessage message)
            => anyList.Add(Any.Pack(message));

        /// <summary>
        /// Extracts a message of a specific type if available.
        /// </summary>
        /// <param name="anyList">The list to search.</param>
        /// <typeparam name="T">The type of the detail to extract.</typeparam>
        /// <returns>The extracted message if found; <c>null</c> if not found.</returns>
        [Pure]
        public static T? GetMessage<T>(this IEnumerable<Any> anyList)
            where T : class, IMessage<T>, new()
        {
            var descriptor = new T().Descriptor;
            return anyList.FirstOrDefault(x => x.Is(descriptor))?.Unpack<T>();
        }
    }
}
