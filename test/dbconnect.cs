using System.Data.SQLite;

namespace Test
{
    class dbconnection
    {
        private SQLiteConnection connector;
        public dbconnection()
        {
            if (!File.Exists("test.db"))
            {
                SQLiteConnection.CreateFile("test.db");
            }
            string cs = @"URI=file:./test.db";
            connector = new SQLiteConnection(cs);
            connector.Open();
            string sql = "CREATE TABLE IF NOT EXISTS account(mr varchar, name varchar, lastname varchar, age number(2), email varchar, st_id varchar, password varchar)";
            SQLiteCommand command = new SQLiteCommand(sql, connector);
            command.ExecuteNonQuery();
        }
        public void exitdb()
        {
            connector.Close();
        }

        public bool checkusername(string name, string lastname, string mr)
        {
            bool recheck = false;
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = connector.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT name FROM account where name = '{name}' and lastname = '{lastname}' and mr = '{mr}';";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                recheck = myreader == name;
            }
            return recheck;
        }

        public void insert(user newuser)
        {
            string sql = $"insert into account values('{newuser.get_mr()}', '{newuser.get_name()}', '{newuser.get_lastname()}', {newuser.get_age()}, '{newuser.get_email()}', '{newuser.get_st_id()}' , '{newuser.get_password()}');";
            SQLiteCommand command = new SQLiteCommand(sql, connector);
            command.ExecuteNonQuery();
        }
        public bool checkmail(string email)
        {
            if (!email.Contains('@'))
                return true;
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = connector.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT email FROM account where email = '{email}';";
            bool checker = false;
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                checker = myreader == email;
            }
            return checker;
        }

        public user checkuser(string email, string password)
        {
            user reuser = new user();
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = connector.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM account where email = '{email}' and password = '{password}';";
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            if (sqlite_datareader.Read())
            {
                reuser.set_mr(sqlite_datareader.GetString(0));
                reuser.set_name(sqlite_datareader.GetString(1));
                reuser.set_lastname(sqlite_datareader.GetString(2));
                reuser.set_age(sqlite_datareader.GetInt32(3));
                reuser.set_email(sqlite_datareader.GetString(4));
                reuser.set_st_id(sqlite_datareader.GetString(5));
                reuser.set_password(sqlite_datareader.GetString(6));
            }
            return reuser;
        }
    }

    class fu
    {
        private SQLiteConnection connector;
        public fu()
        {
            if (!File.Exists("test.db"))
            {
                SQLiteConnection.CreateFile("test.db");
            }
            string cs = @"URI=file:./test.db";
            connector = new SQLiteConnection(cs);
            connector.Open();
            string sql = "CREATE TABLE IF NOT EXISTS book(email varchar, seat varchar, price varchar,string type)";
            SQLiteCommand command = new SQLiteCommand(sql, connector);
            command.ExecuteNonQuery();
        }

        public void insert(string email, string book, string price, string type)
        {
            string sql = $"insert into book values('{email}','{book}',{price});";
            SQLiteCommand command = new SQLiteCommand(sql, connector);
            command.ExecuteNonQuery();
        }

        public string[] getbook(string email)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = connector.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM seat;";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            string[] fu_k = new string[4];
            while (sqlite_datareader.Read())
            {
                fu_k[0] = sqlite_datareader.GetString(0);
                fu_k[2] = sqlite_datareader.GetString(1);
                fu_k[1] = sqlite_datareader.GetString(2);
                fu_k[3] = sqlite_datareader.GetString(3);
            }
            return fu_k;
        }

    }
}