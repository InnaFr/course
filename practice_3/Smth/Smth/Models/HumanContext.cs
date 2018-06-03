using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Smth.Models
{
    public class HumanContext:DbContext
    {
        public DbSet<Human> Humans { get; set; }
    }
}