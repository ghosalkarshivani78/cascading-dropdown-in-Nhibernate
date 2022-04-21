using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nhbernet.Models
{
    public class usermodel
    {
        public int id { get; set; }

        public string name { get; set; }

        public string address { get; set; }

        public string city { get; set; }

        public string state { get; set; }

        public SelectList cities { get; set; }
        public SelectList states { get; set; }
    }

    public class dll {
        public string id { get; set; }

        public string name { get; set; }
    }
}