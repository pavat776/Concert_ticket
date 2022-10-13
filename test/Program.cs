namespace Test
{

    class Program
    {
        private static dbconnection db = new dbconnection();
        private static seatS se = new seatS();
        private static fu bo = new fu();
        static void Main(string[] args)
        {
            Console.Clear();
            int op = 0;
            while (true)
            {
                op = 0;
                Console.WriteLine("Welcome! please select option:");
                Console.WriteLine("\t[1] : login");
                Console.WriteLine("\t[2] : register");
                Console.WriteLine("\t[3] : exit");
                Console.Write("> ");
                try
                {
                    op = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("------------------------------------------------");
                }
                while (true)
                {
                    if (op == 1 || op == 2 || op == 3)
                    {
                        break;
                    }
                    Console.WriteLine("Please select option only 1, 2 or 3:");
                    Console.WriteLine("\t[1] : login");
                    Console.WriteLine("\t[2] : register");
                    Console.WriteLine("\t[3] : exit");
                    Console.Write("> ");
                    try
                    {
                        op = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("------------------------------------------------");
                    }
                }
                Console.Clear();
                if (op == 1)
                    login();
                else if (op == 2)
                    regis();
                else if (op == 3)
                {
                    db.exitdb();
                    break;
                }
            }
        }

        static void login()
        {
            Console.WriteLine("This is login menu.");
            while (true)
            {
                Console.WriteLine("Please enter your email and password");
                Console.Write("email > ");
                string email = Console.ReadLine();
                if (email.ToLower() == "exit")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.Write("password > ");
                    string password = Console.ReadLine();
                    user myuser = db.checkuser(email, password);
                    Thread.Sleep(1000);
                    if (myuser.get_email() != "")
                    {
                        Console.Clear();
                        Secmenu(myuser);
                        break;
                    }
                    Console.WriteLine("Incorrect email or password. Please try again.");

                }
            }

        }
        static void regis()
        {
            while (true)
            {
                Console.WriteLine("This is register menu.");
                Console.WriteLine("------------------------------------------------");
                string is_student = "";
                string Nprefix = "";
                int age;
                string name, lastname, email, st_id = "null", password;
                while (true)
                {
                    Console.Write("Are you student? (Y/N) : ");
                    is_student = Console.ReadLine().ToUpper();
                    if (is_student == "Y" || is_student == "N")
                        break;
                }

                while (true)
                {
                    Console.Write("Choose your name prefix : ");
                    Console.Write("\t [1]:นาย");
                    Console.Write("\t [2]:นาง");
                    Console.Write("\t [3]:นางสาว\n> ");
                    byte inp = 0;
                    try
                    {
                        inp = byte.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("------------------------------------------------");
                    }
                    if (inp == 1)
                    {
                        Nprefix = "นาย";
                        break;
                    }
                    else if (inp == 2)
                    {
                        Nprefix = "นาง";
                        break;
                    }
                    else if (inp == 3)
                    {
                        Nprefix = "นางสาว";
                        break;
                    }
                }
                Console.Write("name : ");
                name = Console.ReadLine();
                Console.Write("last name : ");
                lastname = Console.ReadLine();
                if (db.checkusername(name, lastname, Nprefix))
                {
                    Console.WriteLine("User is already registered. Please try again.");
                    Console.WriteLine("------------------------------------------------");
                    continue;
                }
                Console.Write("age : ");
                age = int.Parse(Console.ReadLine());
                Console.Write("email : ");
                email = Console.ReadLine();
                if (db.checkmail(email))
                {
                    Console.WriteLine("Invalid email. Please try again");
                    Console.WriteLine("------------------------------------------------");
                    continue;
                }
                if (is_student == "Y")
                {
                    Console.Write("student id : ");
                    st_id = Console.ReadLine();
                }
                Console.Write("password : ");
                password = Console.ReadLine();

                Console.Write("password : ");
                if (Console.ReadLine() != password)
                {
                    Console.WriteLine("Mismatched passwords. Please try again.");
                    Console.WriteLine("------------------------------------------------");
                    continue;
                }
                Console.Clear();
                user info = new user(Nprefix, name, lastname, age, email, password, st_id);
                db.insert(info);
                Secmenu(info);
                break;
            }
        }

        static void Secmenu(user userinfo)
        {
            Console.Clear();
            int op = 0;
            while (true)
            {
                op = 0;
                Console.WriteLine("Please select option:");
                Console.WriteLine("\t[1] : booking a seat");
                Console.WriteLine("\t[2] : seat booking result");
                Console.WriteLine("\t[3] : exit");
                Console.Write("> ");
                try
                {
                    op = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("------------------------------------------------");
                }
                while (true)
                {
                    if (op == 1 || op == 2 || op == 3)
                    {
                        break;
                    }
                    Console.WriteLine("Please select option only 1, 2 or 3:");
                    Console.WriteLine("\t[1] : booking a seat");
                    Console.WriteLine("\t[2] : seat booking result");
                    Console.WriteLine("\t[3] : exit");
                    Console.Write("> ");
                    try
                    {
                        op = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("------------------------------------------------");
                    }
                }
                Console.Clear();
                if (op == 1)
                    booking(userinfo);
                else if (op == 2)
                    result(userinfo);
                else if (op == 3)
                    break;
            }
        }
        static void booking(user userinfo)
        {
            List<seat> bookable = se.get_bookable();
            Console.WriteLine("This is booking menu.");
            Console.WriteLine("example: \"A1\" -\" A10\"");
            string inp = "", book = "";
            List<int> bookedList = new List<int>();
            while (true)
            {
                Console.Write(" > ");
                inp = Console.ReadLine();
                if (inp.ToLower() == "exit")
                {
                    Console.Clear();
                    break;
                }
                if (inp == "checkout")
                {
                    if (book != "")
                    {
                        pay(book, userinfo);
                    }
                    Console.Clear();
                    break;
                }
                if (userinfo.get_st_id() != "null" && inp == "\"A1\" -\" A10\"")
                {
                    Console.WriteLine("Cannot book. Please try again.");
                }
            }
        }

        static void pay(string booking, user userinfo)
        {
            int conuntbook = convertTOnum(booking);
            string type = "";
            double price = 0;
            if (userinfo.get_st_id() != null)
            {
                price = 1200.50 * conuntbook;
            }
            else
            {
                price = 5235.25 * conuntbook;
            }
            Console.WriteLine("You need to pay : " + price + " baht.");
            Console.WriteLine("Select payment : \n[1] : bank\n[2] : credit");
            if (Console.ReadLine() == "1")
            {
                Console.Write("enter your bank name: ");
                string f = Console.ReadLine();
                Console.Write("enter your bank number: ");
                string u = Console.ReadLine();
                type = "bank";
            }
            else
            {
                Console.WriteLine("Enter card holder name : ");
                Console.ReadLine();
                Console.WriteLine("Enter card number : ");
                Console.ReadLine();
                Console.WriteLine("Enter date : ");
                Console.ReadLine();
                Console.WriteLine("Enter cvv : ");
                Console.ReadLine();
                type = "credit";
            }
            bo.insert(userinfo.get_email(), booking,Convert.ToString(price), type);

        }

        static void result(user userinfo)
        {
            List<seat> booked = se.checkBooked(userinfo.get_email());
            if (booked.Count == 0)
            {
                Console.WriteLine("Please book your seat first.");
            }
            else
            {
                foreach (seat i in booked)
                {
                    string[] test = bo.getbook(userinfo.get_email());
                    Console.WriteLine("you are booking:\n" + test[2]);
                    Console.WriteLine("Total price = " + test[1]);
                    Console.WriteLine("payment type is " + test[3]);
                }
            }
        }

        static int convertTOnum(string book)
        {
            book.Replace("\"", "");
            book.Replace(" ", "");
            string[] test = book.Split('-');
            int[] re = new int[2];
            re[0] = conv(test[0]);
            re[1] = conv(test[0]);
            return re[1] - re[0];
        }
        static int conv(string fuk)
        {
            if (fuk.Contains('A'))
            {
                fuk.Replace("A", "");
                return int.Parse(fuk);
            }
            fuk.Replace("B", "");
            return int.Parse(fuk) + 10;
        }
    }

}
