using System;
using System.Collections.Generic;
using System.Text;

namespace PMO_viewmodel
{
    public class VMResponse
    {
        public VMResponse()
        {
            Success = true;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public object Entity { get; set; }
    }
}
