// Copyright Bastian Eicher
// Licensed under the MIT License

using FluentAssertions;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Google.Rpc;
using Xunit;

namespace GrpcRichError;

public class AnyFacts
{
    [Fact]
    public void RoundtripMessage()
    {
        var list = new RepeatedField<Any> {new ErrorInfo {Reason = "some reason"}};

        var message = list.GetMessage<ErrorInfo>();
        message.Should().NotBeNull();
        message!.Reason.Should().Be("some reason");
    }
}
