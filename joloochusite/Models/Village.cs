using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace joloochusite.Model
{
    /// <summary>
    /// Поселки
    /// </summary>
    public class Village
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int? RegionId { get; set; }
        public Region Region { get; set; }
        public int? DistrictId { get; set; }
        public District District { get; set; }
    }
}
