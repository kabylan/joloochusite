using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace joloochusite.Model
{
    /// <summary>
    /// Заказы
    /// </summary>
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        [Display(Name = "Каяктан")]
        public int? StartPointId { get; set; }
        [Display(Name = "Каяктан")]
        public Point StartPoint { get; set; }
        [Display(Name = "Каяка")]
        public int? EndPointId { get; set; }
        
        
        [Display(Name = "Каяка")]
        public Point EndPoint { get; set; }
        
        [Display(Name = "Убакыт")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd'.'MM'.'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartTime { get; set; }


        [Display(Name = "Жол кире")]
        public int? Summ { get; set; }
    }
}
