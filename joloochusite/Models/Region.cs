using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace joloochusite.Model
{
    /// <summary>
    /// Регион
    /// </summary>
    public class Region
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<District> Districts { get; set; }
    }
}
