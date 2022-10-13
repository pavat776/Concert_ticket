using System.Data.SQLite;

namespace Test
{
    class seat
    {
        private string id;
        private int status;
        private string email;
        public seat(string id, int status, string email)
        {
            this.id = id;
            this.status = status;
            this.email = email;
        }

        public seat()
        {
            this.id = "";
            this.status = 0;
            this.email = "";
        }

        public void set_id(string id)
        {
            this.id = id;
        }
        public void set_status(int status)
        {
            this.status = status;
        }
        public void set_email(string email)
        {
            this.email = email;
        }

        public string get_id() { return this.id; }
        public string get_status()
        {
            if (status == 1)
                return "booking";
            else if (status == 2)
                return "booked";
            return "blank";
        }
        public string get_email() { return this.email; }
    }
    class seatS
    {
        private SQLiteConnection connector;
        public seatS()
        {
            string cs = @"URI=file:./test.db";
            connector = new SQLiteConnection(cs);
            connector.Open();
            string sql = "CREATE TABLE IF NOT EXISTS seat(id varchar(3),number(1) status, varchar email)";
            SQLiteCommand command = new SQLiteCommand(sql, connector);
            command.ExecuteNonQuery();
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = connector.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT count(*) FROM seat;";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            sqlite_datareader.Read();
            int row = sqlite_datareader.GetInt32(0);
            if (row != 20)
            {
                sql = "delete from seat;";
                command = new SQLiteCommand(sql, connector);
                command.ExecuteNonQuery();
                for (int i = 1; i <= 10; i++)
                {
                    sql = $"insert into seat values('A{i}','0','');";
                    command = new SQLiteCommand(sql, connector);
                    command.ExecuteNonQuery();
                }
                for (int i = 1; i <= 10; i++)
                {
                    sql = $"insert into seat values('B{i}','0','');";
                    command = new SQLiteCommand(sql, connector);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<seat> checkBooked(string email)
        {
            List<seat> listofseat = new List<seat>();
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = connector.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM seat where email = '{email}';";
            bool checker = false;
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                seat seatin = new seat();
                seatin.set_id(sqlite_datareader.GetString(0));
                seatin.set_status(sqlite_datareader.GetInt32(1));
                seatin.set_email(sqlite_datareader.GetString(2));
                listofseat.Add(seatin);
            }
            return listofseat;
        }

        public List<seat> get_bookable()
        {
            List<seat> listofseat = new List<seat>();
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = connector.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM seat where status = 0;";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                seat seatin = new seat();
                seatin.set_id(sqlite_datareader.GetString(0));
                seatin.set_status(sqlite_datareader.GetInt32(1));
                seatin.set_email(sqlite_datareader.GetString(2));
                listofseat.Add(seatin);
            }
            return listofseat;
        }

        public List<seat> getSeat()
        {
            List<seat> listofseat = new List<seat>();
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = connector.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM seat;";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                seat seatin = new seat();
                seatin.set_id(sqlite_datareader.GetString(0));
                seatin.set_status(sqlite_datareader.GetInt32(1));
                seatin.set_email(sqlite_datareader.GetString(2));
                listofseat.Add(seatin);
            }
            return listofseat;
        }
    }
}