using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models.ViewModels
{
    public class DeteilsVM
    {
        public DeteilsVM() 
        {
            Product = new Product();
        }
        public Product Product { get; set; }
        public bool inCart { get; set; }
    }
}
