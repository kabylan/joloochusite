using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace joloochusite.Model
{
    /// <summary>
    /// Районы
    /// </summary>
    public class District
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int RegionId { get; set; }
        public Region Region { get; set; }
        public ICollection<Village> Villages { get; set; }
    }
}
