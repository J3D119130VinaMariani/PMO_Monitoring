using System;
using System.Collections.Generic;
using System.Text;

namespace MiniProject302.viewmodels
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
