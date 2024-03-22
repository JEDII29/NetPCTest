using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPCTest.Domain.Model
{
    public class BasicContactModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public BasicContactModel(string firstname, string lastname) 
        {
            Firstname = firstname;
            Lastname = lastname;
        }
        public BasicContactModel(){}
    }
}
