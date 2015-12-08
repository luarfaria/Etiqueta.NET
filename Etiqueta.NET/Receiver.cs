using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etiqueta.NET.Core
{
    public class Receiver : Person
    {
         public String Name { get { return base.Name; } }
        public String Address { get { return base.Address; } }
        public String Complement { get { return base.Complement; } }
        public String District { get { return base.District; } }
        public String ZipCode { get { return base.ZipCode; } }
        public String City { get { return base.City; } }
        public String State { get { return base.State; } }
        public Receiver(String Name, String Address, String Complement, String District, String ZipCode,
            String City, String State)
        {
            base.Name = Name;
            base.Address = Address;
            base.Complement = Complement;
            base.District = District;
            base.ZipCode = ZipCode;
            base.City = City;
            base.State = State;
        }
    }
}
