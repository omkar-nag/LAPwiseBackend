using System.Xml.Serialization;
using LapAPI.BusinessLayer.NotesRepository;
using LapAPI.BusinessLayer.UserRepository;
using LapAPI.Controllers;
using LapAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Profile_Test
{
    public class UnitTest1
    {
        //Backup
        private readonly Mock<IUsersRepository> UserRep;
        public UnitTest1()
        {
            UserRep = new Mock<IUsersRepository>();
        }
        [Fact]

        public async void GetByID_ShouldResturnUserObject_WhenUserExists()
        {
            //arrange
            var usersData = GetUserssData();
            UserRep.Setup(x => x.GetById(1)).ReturnsAsync(usersData[0]);
            var userController = new UserController(UserRep.Object);

            //act
            var userResult = await userController.GetUser(1);

            var actual = (userResult.Result as OkObjectResult).Value as Users;
            //assert
            Assert.NotNull(actual);
            Assert.IsType<Users>(actual);
            Assert.Equal<Users>(usersData[0], actual);
            
        }
        [Fact]
        public async void GetByID_ShouldReturnsNotFound_WhenUserExists()
        { //arrange
            var usersData = GetUserssData();
            var tempUser = usersData[0];
            tempUser.Id = 2;
            UserRep.Object.Insert(tempUser);
            UserRep.Setup(x => x.GetById(1)).Throws<ItemNotFoundException>();
            var userController = new UserController(UserRep.Object);

            //act
            var userResult = await userController.GetUser(1);

            var actual = (userResult.Result as NotFoundObjectResult);
            //assert

            Assert.IsType<NotFoundObjectResult>(actual);

        }
        [Fact]

        public async void Update_ShouldReturnsUpdatedUserObject_WhenUserExists()
        {
            Users user = GetUserssData()[0];
            
            UserRep.Object.Insert(user);
            user.Id = 1;
            user.FirstName = "Vinita";
            user.LastName = "aaa";
            user.Email = "vinitaabburi222@gmail.com";
            user.Password = "Vinni@1234";
            user.UserName = "Vinni";
            UserRep.Setup(x => x.Update(1, user)).ReturnsAsync(user);
            UserRep.Object.Insert(user);
            var userController = new UserController(UserRep.Object);

            //act
            var userResult = await userController.PutUser(1, user);

            var actual = (userResult.Result as OkObjectResult).Value as Users;

            Assert.IsType<Users>(actual);
            //assert
            Assert.NotNull(userResult);
            Assert.Equal<Users>(user, actual);

        }
        [Fact]

        public async void Update_ShouldReturnsBadRequestObject_WhenUserDoesNotExists()
        {
            Users user = GetUserssData()[0];

            UserRep.Object.Insert(user);
            user.Id = 1;
            user.FirstName = "Vinita";
            user.LastName = "aaa";
            user.Email = "vinitaabburi222@gmail.com";
            user.Password = "Vinni@1234";
            user.UserName = "Vinni";
            UserRep.Object.Insert(user);
            UserRep.Setup(x => x.Update(2, user)).Throws<ItemUpdateException>();

            var userController = new UserController(UserRep.Object);

            //act
            var userResult = await userController.PutUser(2, user);

            var actual = (userResult.Result as BadRequestObjectResult);
            //assert
            Assert.IsType<BadRequestObjectResult>(actual);
 


        }

        private List<Users> GetUserssData()
        {
            List<Users> usersData = new List<Users>
                {
                    new Users
                    {
                        Id = 1,
                        FirstName = "Vinita",
                        LastName = "Abburi",
                        Email = "vinitaabburi222@gmail.com",
                        Password = "Vinni@1234",
                        UserName = "Vinni"
                    },

                };
            return usersData;
        }

    }
}
