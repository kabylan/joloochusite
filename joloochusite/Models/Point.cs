using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace joloochusite.Model
{
    /// <summary>
    /// Точки
    /// </summary>
    public class Point
    {
        [Key]
        public int Id { get; set; }
        public int? VillageId { get; set; }
        public Village Village { get; set; }
        public int? DistrictId { get; set; }
        public District District { get; set; }
    }
}
