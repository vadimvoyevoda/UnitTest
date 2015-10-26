using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting
{
    public enum LoginResult
    {
         Ok,
         Error
    }

    public interface IRequest
    {
        void Send(string packet);
        int GetRequestCount();
    }

    public interface ILogger
    {
        void Log(string message, params string[] arr);
    }

    public interface IValidator
    {
        bool Validate(string userName, string userPassword);
    }

    public class Validator : IValidator
    {
        public bool Validate(string userName, string userPassword)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(userPassword))
                return false;

            var invalidSymbols = new char[]{'@', '%', '#'};
            foreach(var s in invalidSymbols)
            {
                if (userName.Contains(s))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class Request : IRequest
    {
        private int requestCount;

        public Request()
        {
        }

        public Request(int number)
        {
            requestCount = number;
        }

        public void Send(string packet)
        {
            requestCount++;
            Console.WriteLine("Packet data: {0}", packet.ToString());
        }

        public int GetRequestCount()
        {
            return requestCount;
        }
    }


    public class Logger : ILogger
    {
        public void Log(string message, params string[] arr)
        {
            Console.WriteLine(message, arr);
        }
    }

    public class LoginPage
    {
        private readonly IValidator validator;
        private readonly IRequest request;
        private readonly ILogger logger;

        public LoginPage(IValidator validator, IRequest request, ILogger logger)
        {
            this.validator = validator;
            this.request = request;
            this.logger = logger;
        }

        public LoginResult DoLogin(string userName, string userPassword)
        {
            if (validator.Validate(userName, userPassword))
            {
                string packet = userName + userPassword;
               // var raw = Encoding.ASCII.GetBytes(packet);
                packet += "0xABCDE";
                request.Send(packet);
                logger.Log("Send request with user {0} and password {1}", userName, userPassword);
                return LoginResult.Ok;
            }
            return LoginResult.Error;
        }
    }
}
