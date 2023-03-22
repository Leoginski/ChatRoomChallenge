using MassTransit;
using Moq;
using SimpleChatroom.Domain.Commands;
using SimpleChatroom.Domain.Services;

namespace SimpleChatroom.Tests.Domain.Services
{
    public class CommandServiceTests
    {
        private readonly Mock<IBus> _busMock;
        private readonly Mock<ISendEndpoint> _sendEndpointMock;
        private readonly CommandService _service;

        public CommandServiceTests()
        {
            _busMock = new Mock<IBus>();
            _sendEndpointMock = new Mock<ISendEndpoint>();
            _service = new CommandService(_busMock.Object);
        }

        [Fact]
        public async Task ParseCommand_ValidMessage_ParseValues()
        {
            //Arrange
            string message = "/stock=aapl.us";

            //Act
            var result = await _service.ParseCommand(message);

            //Assert
            Assert.Equal("stock", result.CommandName);
            Assert.Equal("aapl.us", result.Parameter);
        }

        [Fact]
        public async Task ParseCommand_InvalidMessage_ReturnsNull()
        {
            //Arrange
            string message = "/stockaapl.us";

            //Act
            var result = await _service.ParseCommand(message);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task SendCommand_ValidCommand_SendCommand()
        {
            //Arrange
            Command command = new();

            _busMock.Setup(x => x.GetSendEndpoint(It.IsAny<Uri>())).ReturnsAsync(_sendEndpointMock.Object);

            //Act
            await _service.SendCommand(command);

            //Assert
            _busMock.Verify(x => x.GetSendEndpoint(It.IsAny<Uri>()), Times.Once);
            _sendEndpointMock.Verify(x => x.Send(It.IsAny<Command>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
