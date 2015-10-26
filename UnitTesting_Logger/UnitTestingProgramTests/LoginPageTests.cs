using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTesting;

namespace UnitTestingProgramTests
{
    [TestFixture]
    public class LoginPageTests
    {
        [Test]
        public void should_login_with_valid_user_name_and_id()
        {
            //arrange
            //TODO: mock object or substitute real life objects with some strubs or mocks / or fake object
            var validatorMock = Substitute.For<IValidator>();
            var requestMock = Substitute.For<IRequest>();
            var logger = Substitute.For<ILogger>();

            //validate method should return true in any cases
            validatorMock.Validate(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

            //LoginPage is real instance
            var page = new LoginPage(validatorMock, requestMock, logger);
            string userName = Guid.NewGuid().ToString();
            string userPassword = Guid.NewGuid().ToString();

            var testPack = userName + userPassword;
            var message = "Send request with user {0} and password {1}";

            //act
            var res = page.DoLogin(userName, userPassword);

            //assert
            Assert.AreEqual(LoginResult.Ok, res);
            validatorMock.Received().Validate(userName, userPassword);
            requestMock.Received().Send(testPack + "0xABCDE");
            logger.Received().Log(message, userName, userPassword);
        }

        [Test]
        public void should_validate_with_valid_userName_userPassword()
        {
            //arrange
            var validator = new Validator();
            string userName = "";
            string userPassword = Guid.NewGuid().ToString();

            bool actual = true;

            if (string.IsNullOrWhiteSpace(userName)
                || string.IsNullOrWhiteSpace(userPassword))
            {
                actual = false;
            }
            else
            {
                //userName shouldn't have this characters
                var invalidSymbols = new char[] { '@', '%', '#' };

                foreach (var s in invalidSymbols)
                {
                    if (userName.Contains(s))
                    {
                        actual = false;
                        break;
                    }
                }
            }

            //act
            var res = validator.Validate(userName, userPassword);

            //assert
            Assert.AreEqual(actual, res);            
        }

        [Test]
        public void should_request_get_request_count()
        {
            //arrange
            Random rand = new Random();
            int expected = rand.Next(10);
            var request = new Request(expected);

            //act
            var res = request.GetRequestCount();            
            
            //assert
            Assert.AreEqual(res, expected);
        }

        [Test]
        public void should_request_send_with_any_string_packet()
        {
            using (StringWriter sw = new StringWriter())
            {
                //arrange
                Console.SetOut(sw);

                var request = new Request(new Random().Next(10));
                int beforeCount = request.GetRequestCount(); 

                string packet = Guid.NewGuid().ToString();
                string expected = string.Format("Packet data: {0}{1}", packet, Environment.NewLine);

                //act
                request.Send(packet);
                int afterCount = request.GetRequestCount();

                //assert
                Assert.AreEqual(afterCount - beforeCount, 1);
                Assert.AreEqual(expected, sw.ToString());

                //after test
                ConsoleInitialize();
            }
        }

        [Test]
        public void should_logger_log_to_console_with_console_write_line_with_any_message()
        {
            using(StringWriter sw = new StringWriter())
            {
                //arrange
                Console.SetOut(sw);

                var logger = new Logger();
                string msg = "Message {0}";
                string param = Guid.NewGuid().ToString();

                string expected = string.Format(msg+"{1}", param, Environment.NewLine);
                
                //act
                logger.Log(msg, param);

                //assert
                Assert.AreEqual(expected, sw.ToString());

                //after test
                ConsoleInitialize();
            }
        }
                
        private void ConsoleInitialize()
        {
            StreamWriter standartOutput = new StreamWriter(Console.OpenStandardOutput());
            standartOutput.AutoFlush = true;
            Console.SetOut(standartOutput);
        }
    }
}
