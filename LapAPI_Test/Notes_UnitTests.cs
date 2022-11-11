using System.Xml.Serialization;
using Castle.Core.Logging;
using LapAPI.BusinessLayer.NotesRepository;
using LapAPI.BusinessLayer.UserRepository;
using LapAPI.Controllers;
using LapAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NuGet.Protocol;

namespace Profile_Test
{
    public class Notes_UnitTest : CustomControllerBase
    {
        //Backup
        private readonly Mock<INotesRepository> NotesRep;
        public Notes_UnitTest()
        {
            NotesRep = new Mock<INotesRepository>();
        }
        [Fact]
        public void Post_Success()
        {
            //arrange
            var notesData = GetNotesData()[0];
            notesData.Id = -1;
            NotesRep.Setup(x => x.PostNotes(notesData)).Returns(() => notesData);
            var notesController = new NotesController(NotesRep.Object);

            //act
            var notesResult = notesController.PostNotes(notesData);

            var actual = (notesResult.Result as OkObjectResult).Value as Notes;

            //assert
            Assert.NotNull(actual);
            Assert.IsType<Notes>(actual);
            Assert.Equal<Notes>(notesData, actual);
        }
        [Fact]
        public void Post_Failure()
        {
            //arrange
            var notesData = GetNotesData()[0];
            NotesRep.Setup(x => x.PostNotes(notesData)).Throws<InvalidOperationException>();
            var notesController = new NotesController(NotesRep.Object);

            //act
            var notesResult = notesController.PostNotes(notesData);

            var actual = (notesResult.Result as BadRequestResult);

            //assert
            Assert.IsType<BadRequestResult>(actual);

        }
        [Fact]
        public void PutNotes_Success()
        {
            var notesData = GetNotesData()[0];

            NotesRep.Setup(x => x.PutNotes(notesData)).Returns(notesData);
            NotesRep.Object.PostNotes(notesData);
            var notesController = new NotesController(NotesRep.Object);

            //act
            var notesResult = notesController.PutNotes(notesData);

            var actual = (notesResult.Result as OkObjectResult).Value as Notes;

            //assert
            Assert.IsType<Notes>(actual);
            Assert.Equal<Notes>(actual, notesData);
        }
        [Fact]
        public void PutNotes_Failure()
        {
            var notesData = GetNotesData()[0];
            notesData.UserId = 2;
            NotesRep.Setup(x => x.PutNotes(notesData)).Throws<ItemNotFoundException>();

            var notesController = new NotesController(NotesRep.Object);

            //act
            var notesResult = notesController.PutNotes(notesData);

            var actual = (notesResult.Result as NotFoundObjectResult);

            //assert
            Assert.IsType<NotFoundObjectResult>(actual);
        }
        [Fact]
        public async void DeleteNote_Success()
        {
            var notesData = GetNotesData()[0];

            NotesRep.Setup(x => x.DeleteNotes(1)).ReturnsAsync(() => notesData);
            NotesRep.Object.PostNotes(notesData);
            var notesController = new NotesController(NotesRep.Object);

            //act
            var notesResult = await notesController.DeleteNotes(1);

            var actual = (notesResult.Result as OkObjectResult);

            //assert
            Assert.IsType<OkObjectResult>(actual);
        }
        

        

        private List<Notes> GetNotesData()
        {
            int i = 1;
            List<Notes> notesData = new List<Notes>
                {
                    new Notes
                    {
                        Id = i++,
                        Title = "one",
                        Content = "one_content",
                        UserId = 1
                    },new Notes
                    {
                        Id = i++,
                        Title = "one",
                        Content = "one_content",
                        UserId = 1
                    },new Notes
                    {
                        Id = i++,
                        Title = "one",
                        Content = "one_content",
                        UserId = 1
                    },

                };
            return notesData;
        }

    }
}
