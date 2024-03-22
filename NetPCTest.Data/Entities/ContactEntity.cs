using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPCTest.Data.Entities
{
    public class ContactEntity : BaseEntity
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public UserEntity User { get; set; }
        public Guid UserId { get; set; }


        public ContactEntity() { }
        public ContactEntity(Guid id, string firstname, string surname, string email,
            string phoneNumber, DateTime birthDate, string category, Guid userId)
        {
            Id = id;
            Firstname = firstname;
            Surname = surname;
            Email = email;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
            Category = category;
            UserId = userId;
        }

    }
}
