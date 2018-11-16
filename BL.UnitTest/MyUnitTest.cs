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

    }
}
