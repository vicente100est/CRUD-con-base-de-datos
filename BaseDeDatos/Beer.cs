using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDeDatos
{
    public class Beer
    {
        public int BeerID { get; set; }
        public string Name { get; set; }
        public int BrandID { get; set; }
        public Beer(int id, string name, int idBrand)
        {
            this.BeerID = id;
            this.Name = name;
            this.BrandID = idBrand;
        }

        public Beer(string name, int idBrand)
        {
            this.Name = name;
            this.BrandID = idBrand;
        }
    }
}
