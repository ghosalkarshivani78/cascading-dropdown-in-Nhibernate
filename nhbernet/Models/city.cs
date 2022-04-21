using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nhbernet.Models
{
    public class city
    {
        public virtual int id { get; set; }
        public virtual string cityname { get; set; }

        public virtual state stateid { get; set; }
    }
}