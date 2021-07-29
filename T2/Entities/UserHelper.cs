using System.Text.RegularExpressions;

namespace T2 
{ 
    public static class UserHelper
    {
        public static bool CheckFirstName(string firstName)
        {
            return Regex.IsMatch(firstName, "^([a-zA-Z]{3,})");
        }

        public static bool CheckLastName(string lastName)
        {
            return Regex.IsMatch(lastName, "^([a-zA-Z]{5,})");
        }

        public static bool CheckMail(string mail)
        {
            return Regex.IsMatch(mail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}
