using System;

namespace HomeWork3.Models
{
    public class User
    {
        public Guid id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string phone { get; set; }

        public string email { get; set; }
    }
}
