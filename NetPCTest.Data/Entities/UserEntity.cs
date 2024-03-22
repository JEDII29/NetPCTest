using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPCTest.Data.Entities
{
    public class UserEntity : BaseEntity
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public virtual ICollection<ContactEntity> Contacts { get; set; }

    }
}
