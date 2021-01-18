using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace joloochusite.Model
{
    /// <summary>
    /// Тип машины
    /// </summary>
    public class TypeCar
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
