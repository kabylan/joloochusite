using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace joloochusite.Model
{
    /// <summary>
    /// Машина
    /// </summary>
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public int? MarkId { get; set; }
        public Mark Mark { get; set; }
        public int? UserId { get; set; }
        public AppUser User { get; set; }
        public string Number { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public string CarMark { get; set; }
    }
}
