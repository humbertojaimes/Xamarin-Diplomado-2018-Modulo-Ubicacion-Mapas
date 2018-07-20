using System;
namespace UbicacionMapas.Classes
{
    public class Store
    {
        public string Name
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public string Photo
        {
            get;
            set;
        }

        public Store(string name, string address, string photo)
        {
            this.Address = address;
            this.Name = name;
            this.Photo = photo;
        }
    } 

}
