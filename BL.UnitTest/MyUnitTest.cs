using Data.Repositories.Interfaces;
using Model.Models;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BL.Managers;

namespace BL.UnitTest
{
    public class MyUnitTest
    {
        [Fact]
        public void CanHash()
        {
            //setup
            var password = "123";
            var expectedResult = "3244185981728979115075721453575112";
            //run
            var hashPassword = Util.HashHelper.GetMD5HashData(password);
            
            //assert
            Assert.Equal(expectedResult, hashPassword);
        }
        
        [Theory]
        [InlineData("123", "3244185981728979115075721453575112")]
        public void MultipleCanHash(string password,string expectedResult)
        {
            //setup

            //run
            var hashPassword = Util.HashHelper.GetMD5HashData(password);

            Assert.Equal(expectedResult, hashPassword);
        }

        [Fact]
        public void TestAddToCourse()
        {
            var studentRepo = Substitute.For<IStudentRepository>();
            var studentCourseRepo = Substitute.For<IStudentCourseRepository>();
            var courseRepo = Substitute.For<ICourseRepository>();
            var student = new Student
            {
                Id = 1,
                Credit = 16
            };
            var course = new Course
            {
                Id = 1,
            };
            
            studentRepo.GetById(1).Returns(student);

            var studentManager = new StudentManager(studentRepo,studentCourseRepo,courseRepo);
            studentManager.AddToCourse(1, course);

            Assert.Equal(12, student.Credit);
        }
    }
}
