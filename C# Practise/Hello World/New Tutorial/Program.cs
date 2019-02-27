using System;

namespace New_Tutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What is your name?");
            var name = Console.ReadLine();

            Console.WriteLine("What is your age");
            var age = Console.ReadLine();

            Console.WriteLine("What month were you born?");
            var month = Console.ReadLine();

            Console.WriteLine("What is the password");
            var password = Console.ReadLine();

            Console.WriteLine("Your name is: {0}", name);
            Console.WriteLine("Your age is: {0}", age);
            Console.WriteLine("Month born: {0}", month);

            if(month == "march")
            {
                Console.WriteLine("You are Aries");
            }
            else if(month == "april")
            {
                Console.WriteLine("You are Taurus");
            }
            else if(month == "january")
            {
                Console.WriteLine("You are capricorn");
            }

            if(password == "Secret")
            {
                Console.WriteLine("Password Accepted");
            }
            else if(password != "Secret")
            {
                Console.WriteLine("Password Wrong");
            }
        }
    }
}
