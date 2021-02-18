using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MineweeperWebApplication.Models
{
    public class User
    {
        [Required]
        [StringLength(20, MinimumLength = 4)]
        [DisplayName("User's First Name")]
        public String FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 4)]
        [DisplayName("User's Last Name")]
        public String LastName { get; set; }

        [Required]
        [StringLength(20, MinimumLength =3)]
        [DisplayName("User's Sex")]
        public String Sex { get; set; }

        [Required]
        [Range(6, 100, ErrorMessage = "You must me at least 6 years old to play this game!")]
        [DisplayName("User's Age")]
        public int Age { get; set; }

        [Required]
        [StringLength(20, MinimumLength =4)]
        [DisplayName("User's State")]
        public String State { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(40, MinimumLength = 4)]
        [DisplayName("User's Email Address")]
        public String Email { get; set; }

        [Required]
        [StringLength(40, MinimumLength =4)]
        [DisplayName("Username")]
        public String Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 4)]
        [DisplayName("Password")]
        public String Password { get; set; }

        public User(string firstName, string lastName, string sex, int age, string state, string email, string username, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Sex = sex;
            Age = age;
            State = state;
            Email = email;
            Username = username;
            Password = password;
        }

        public User()
        {
        }
    }
}
