namespace Test
{
    class user
    {
        private string mr;
        private string name;
        private string lastname;
        private int age;
        private string email;
        private string password;
        private string st_id;

        public user()
        {
            this.mr = "";
            this.name = "";
            this.lastname = "";
            this.age = 0;
            this.email = "";
            this.password = "";
            this.st_id = "";
        }

        public user(string mr, string name, string lastname, int age, string email, string password, string st_id = "null")
        {
            this.mr = mr;
            this.name = name;
            this.lastname = lastname;
            this.age = age;
            this.email = email;
            this.password = password;
            this.st_id = st_id;
        }

        public string get_mr() { return this.mr; }
        public string get_name() { return this.name; }
        public string get_lastname() { return this.lastname; }
        public int get_age() { return this.age; }
        public string get_email() { return this.email; }
        public string get_password() { return this.password; }
        public string get_st_id() { return this.st_id; }

        public void set_mr(string mr) { this.mr = mr; }
        public void set_name(string name) { this.name = name; }
        public void set_lastname(string lastname) { this.lastname = lastname; }
        public void set_age(int age) { this.age = age; }
        public void set_email(string email) { this.email = email; }
        public void set_password(string password) { this.password = password; }
        public void set_st_id(string st_id) { this.st_id = st_id; }
    }
}