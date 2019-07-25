using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BauToDoList.Models
{
    public class BaseEntity
    {


        public int Id { get; set; }

        [DisplayName("Oluşturan kişi")]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [DisplayName("Oluşturulma tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Düzenleyen kişi")]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        [DisplayName("Düzenleme tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime UpdatedDate { get; set; }
    }
}