using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishesYulin4ISP9_7.EF
{
    partial class User
    {
        public string UserNames => $"{UserSurname} {UserName} {UserPatronymic}";
    }
}
