using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Helper
{
    public class Pagination
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int maxPage { get; set; }
        public int count { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public string sortColumn { get; set; }
        public string currOrder { get; set; }
        public string search { get; set; }

        public Pagination()
        {
            pageSize = 3;
            sortColumn = "Name";
            currOrder = "asc";
            search = "";
        }
    }
    
}