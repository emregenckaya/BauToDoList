using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BauToDoList.Models
{
    public class Contact : BaseEntity
    {
        [Required(ErrorMessage ="Bu alan zorunludur.")]
        [StringLength(100)]
        [DisplayName("Ad")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [StringLength(100)]
        [DisplayName("Soyad")]
        public string LastName { get; set; }

        [StringLength(100)]
        [DisplayName("E-Mail")]
        public string Email { get; set; }

        [StringLength(25)]
        [DisplayName("Telefon")]
        public string Phone { get; set; }

        public virtual ICollection<ToDoItem> ToDoItems { get; set; }
    }
}