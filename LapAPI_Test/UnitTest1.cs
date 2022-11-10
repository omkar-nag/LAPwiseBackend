using Moq;
using LapAPI.Controllers;
using LapAPI.Models;
using LapAPI.BusinessLayer.UserRepository;
using NuGet.Protocol.Core.Types;
using Microsoft.EntityFrameworkCore;
using Xunit;

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LapAPI_Test
{
    public class UnitTest1
    {
        private readonly Mock<IUsersRepository> UserRep;

        public UnitTest1()
        {
            UserRep = new Mock<IUsersRepository>();


        }
        [Fact]

        public void GetByID_Success()
        {
            //arrange
            var usersData = GetUsersData();
            UserRep.Setup(x => x.GetById(1)).Returns(usersData[0]);
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
            var usersData = Userdata();
            Users x = null;
            UserRep.Setup(x => x.GetById(2)).Returns(x);
            var userController = new UserController(UserRep.Object);

            //act
            var productResult = userController.GetUser(2);


            //assert
            Assert.Null(productResult);

        }

        private List<Users> GetUsersData()
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

            }; return usersData;
        }

        [Fact]
        private void Update()
        {
            var userData = Userdata();
            UserRep.Setup(x => x.Update(1, userData)).ReturnsAsync(userData);
            var userController = new UserController(UserRep.Object);

            //act
            var productResult = userController.PutUser(1, userData);


            Assert.IsType<OkResult>(productResult);
            //assert
            Assert.NotNull(productResult);


        }
        private Users Userdata()
        {
            Users userData = new Users
            {

                Id = 1,
                FirstName = "Vinnnita",
                LastName = "Abburi",
                Email = "vinitaabburi222@gmail.com",
                Password = "Vinni@1234",
                UserName = "Vinni"
            };
            return userData;
        }


    }
}
