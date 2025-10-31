using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        #region Part 3 NotFound Error Handling 
        public ProductNotFoundException(int id) : base($"Product with Id : {id} Not Found")
        {

        } 

        #endregion
    }
}
