using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONET_TELEPHONES
{
    public class Phone_rec
    {
        private int id;
        private string model;
        private string ind_id;
        private int year;
        private double price;

        public Phone_rec() { }

        public int Id { get => id; set => id = value; }
        public string Model { get => model; set => model = value; }
        public string Ind_id { get => ind_id; set => ind_id = value; }
        public int Year { get => year; set => year = value; }
        public double Price { get => price; set => price = value; }
    }

    public class Industry_rec
    {
        private string id;
        private string name;
        private string country;
        private string type;
        private string website;

        public Industry_rec() { }

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Country { get => country; set => country = value; }
        public string Type { get => type; set => type = value; }
        public string Website { get => website; set => website = value; }
    }
}
