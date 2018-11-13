using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

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

            //assert
            var hashPassword = Util.HashHelper.GetMD5HashData(password);

            Assert.Equal(expectedResult, hashPassword);
        }
    }
}
