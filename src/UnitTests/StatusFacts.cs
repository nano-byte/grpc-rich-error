// Copyright Bastian Eicher
// Licensed under the MIT License

using FluentAssertions;
using Google.Rpc;
using Grpc.Core;
using Xunit;

namespace GrpcRichError;

public class StatusFacts
{
    [Fact]
    public void StatusToException()
    {
        var exception = new Google.Rpc.Status
        {
            Code = (int)StatusCode.NotFound,
            Message = "some message"
        }.ToException();

        exception.Status.StatusCode.Should().Be(StatusCode.NotFound);
        exception.Status.Detail.Should().Be("some message");

        var richStatus = exception.GetRichStatus();
        richStatus.Should().NotBeNull();
        richStatus!.Code.Should().Be((int)StatusCode.NotFound);
        richStatus!.Message.Should().Be("some message");
    }

    [Fact]
    public void RoundtripException()
    {
        var exception = new Google.Rpc.Status
        {
            Details = {new ErrorInfo {Reason = "some reason"}}
        }.ToException();

        var message = exception.GetDetail<ErrorInfo>();
        message.Should().NotBeNull();
        message!.Reason.Should().Be("some reason");
    }
}
