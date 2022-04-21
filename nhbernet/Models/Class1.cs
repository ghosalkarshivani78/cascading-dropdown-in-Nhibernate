using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nhbernet.Models
{
    public class user
    {
        public virtual int id { get; set; }

        public virtual string name { get; set; }

        public virtual string address { get; set; }

       // public virtual string city { get; set; }

        public virtual city cityid { get; set; }

        public virtual state stateid { get; set; }

    }
}