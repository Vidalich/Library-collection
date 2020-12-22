using System;
using System.Collections.Generic;
using System.Text;

namespace task5_1
{
    class Book
    {
        private int id;
        private string data;
        private string genre;
        private string status;

        public int Id { get => id; set => id = value; }
        public string Data { get => data; set => data = value; }
        public string Genre { get => genre; set => genre = value; }
        public string Status { get => status; set => status = value; }

        public Book(int id, string data, string genre, string status)
        {
            this.id = id;
            this.data = data;
            this.genre = genre;
            this.status = status;
        }

        public Book()
        {
            this.id = 0;
            this.data = "";
            this.genre = "";
            this.status = "";
        }

        public override string ToString()
        {
            return $"{id};{data};{genre};{status}";
        }
    }
}
