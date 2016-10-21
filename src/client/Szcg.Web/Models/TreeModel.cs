using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Szcg.Web.Models
{
    public class TreeModel
    {
        public string id { get; set; }
        public string pId { get; set; }
        public string name { get; set; }
        public bool open { get; set; }
        public string phone { get; set; }
    }
}