using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace joloochusite.Model
{
    /// <summary>
    /// Марка определенного типа машины
    /// </summary>
    public class Mark
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeCarId { get; set; }
        public TypeCar TypeCar { get; set; }
    }
}
