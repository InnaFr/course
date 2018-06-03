using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Smth.Models
{
    [Table("Humans")]
    public class Human
    {
        [Key]
        public string name { get; set; }
        public int age { get; set; }
    }
}