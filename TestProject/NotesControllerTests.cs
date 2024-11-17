using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;

namespace NotesApi.Tests
{
    public class NotesControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly NotesController _controller;

        public NotesControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new NotesController(_mediatorMock.Object);
        }

        private void MockUser(string userId)
        {
            var claims = new List<Claim>
            {
                new Claim("userId", userId)
            };

            var identity = new ClaimsIdentity(claims, "test");
            var principal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = principal
            };

            _controller.ControllerContext.HttpContext = httpContext;
        }

        [Fact]
        public async Task Create_ReturnsOkStatus_WhenValidData()
        {
            MockUser("123"); 

            var title = "Test Title";
            var description = "Test Description";
            var recordFile = new Mock<IFormFile>(); 

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateNoteCommand>(), default))
                         .Returns(Task.FromResult(MediatR.Unit.Value));

            var result = await _controller.Create(title, description, recordFile.Object);

            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Update_ReturnsOkStatus_WhenValidData()
        {
            MockUser("123"); 
            
            var id = "1";
            var title = "Updated Title";
            var description = "Updated Description";
            var recordFile = new Mock<IFormFile>();

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateNoteCommand>(), default))
                         .Returns(Task.FromResult(MediatR.Unit.Value));

            var result = await _controller.Update(id, title, description, recordFile.Object);

            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsOkStatus_WhenValidId()
        {
            MockUser("123"); 

            var id = "1";

            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteNoteCommand>(), default))
                         .Returns(Task.FromResult(MediatR.Unit.Value));

            var result = await _controller.Delete(id);

            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
