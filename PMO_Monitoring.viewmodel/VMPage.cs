using System;
using System.Collections.Generic;
using System.Text;

namespace PMO_viewmodel
{
    public class VMPage
    {
        public string sortOrder { get; set; }
        public string searchString { get; set; }
        public string currentFilter { get; set; }
        public int? pageNumber { get; set; }
        public int? showData { get; set; }

        public string? sortBy { get; set; }

        
    }
}
