using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPCTest.Data.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Login { get; set; }

        public string Password { get; set; }

    }
}
