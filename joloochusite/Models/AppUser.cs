using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace joloochusite.Model
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class AppUser
    {
        [Key]
        public int Id { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronimyc { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
        public int MaxCount { get; set; }
        public int CurrentCount { get; set; }

    }
}
