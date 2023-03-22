using Moq;
using SimpleChatroom.Domain.Commands;
using SimpleChatroom.Domain.Interfaces;
using SimpleChatroom.Domain.Models;
using SimpleChatroom.Domain.Services;

namespace SimpleChatroom.Tests.Domain.Services
{
    public class ChatroomServiceTests
    {
        private readonly Mock<IChatroomRepository> _repositoryMock;
        private readonly Mock<ICommandService> _commadServiceMock;
        private readonly ChatroomService _service;

        public ChatroomServiceTests()
        {
            _repositoryMock = new Mock<IChatroomRepository>();
            _commadServiceMock = new Mock<ICommandService>();

            _service = new ChatroomService(_repositoryMock.Object, _commadServiceMock.Object);
        }

        [Fact]
        public async Task ProcessMessage_Message_ShouldSaveMessage()
        {
            //Arrange
            var userId = $"{Guid.NewGuid}";
            var message = "Hello!";

            //Act
            await _service.ProcessMessage(userId, message);

            //Assert
            _commadServiceMock.Verify(x => x.ParseCommand(message), Times.Once);
            _commadServiceMock.Verify(x => x.SendCommand(It.IsAny<Command>()), Times.Never);
            _repositoryMock.Verify(x => x.AddMessage(It.IsAny<Message>()), Times.Once);
        }

        [Fact]
        public async Task ProcessMessage_Command_ShouldSendCommand()
        {
            //Arrange
            var userId = $"{Guid.NewGuid}";
            var message = "/stock=aapl.us";

            _commadServiceMock.Setup(x => x.ParseCommand(message)).ReturnsAsync(new Command());

            //Act
            await _service.ProcessMessage(userId, message);

            //Assert
            _commadServiceMock.Verify(x => x.ParseCommand(message), Times.Once);
            _commadServiceMock.Verify(x => x.SendCommand(It.IsAny<Command>()), Times.Once);
            _repositoryMock.Verify(x => x.AddMessage(It.IsAny<Message>()), Times.Never);
        }
    }
}
