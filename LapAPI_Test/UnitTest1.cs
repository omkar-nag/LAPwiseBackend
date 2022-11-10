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

        public void GetByID_Success()
        {
            //arrange
            var usersData = GetUserssData();
            UserRep.Setup(x => x.GetById(1)).ReturnsAsync(usersData[0]);
            var userController = new UserController(UserRep.Object);

            //act
            var productResult = userController.GetUser(1);


            //assert
            Assert.NotNull(productResult);
            Assert.Equal(usersData[0].Id, productResult.Id);
            Assert.True(usersData[0].Id == productResult.Id);
        }
        [Fact]
        public void GetByID_Failure()
        {
            //arrange
            var usersData = GetUserssData();
            Users x = null;
            UserRep.Setup(x => x.GetById(2)).ReturnsAsync(x);
            var userController = new UserController(UserRep.Object);

            //act
            var productResult = userController.GetUser(2);


            //assert
            Assert.Null(productResult.Result);
        }
        [Fact]

        public void Update_Success()
        {
            Users user = new Users();
            user.Id = 1;
            user.FirstName = "Vinita";
            user.LastName = "Abburi";
            user.Email = "vinitaabburi222@gmail.com";
            user.Password = "Vinni@1234";
            user.UserName = "Vinni";
            UserRep.Setup(x => x.Update(1, user)).ReturnsAsync(user);
            var userController = new UserController(UserRep.Object);

            //act
            var productResult = userController.PutUser(1, user);


            Assert.IsType<OkResult>(productResult.Result);
            //assert
            Assert.NotNull(productResult);


        }
        [Fact]

        public void Update_Failure()
        {
            Users user = new Users();
            user.Id = 1;
            user.FirstName = "Vinita";
            user.LastName = "Abburi";
            user.Email = "vinitaabburi222@gmail.com";
            user.Password = "Vinni@1234";
            user.UserName = "Vinni";
            UserRep.Setup(x => x.Update(2, user)).Throws<ItemUpdateException>();
            var userController = new UserController(UserRep.Object);

            //act
            var productResult = userController.PutUser(2, user);


            Assert.IsType<BadRequestResult>(productResult.Result);
            //assert


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
