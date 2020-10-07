// Copyright Bastian Eicher
// Licensed under the MIT License

using FluentAssertions;
using Grpc.Core;
using Xunit;

namespace GrpcRichError
{
    public class MetadataFacts
    {
        [Fact]
        public void RoundtripRichStatus()
        {
            var metadata = new Metadata {new Google.Rpc.Status {Message = "some message"}};
            var status = metadata.GetRichStatus();
            status.Should().NotBeNull();
            status!.Message.Should().Be("some message");
        }
    }
}
