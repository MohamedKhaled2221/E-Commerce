using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.IdentityModule
{
    #region Part 2 Identity Class Identity Context
    public class User : IdentityUser
    {
        public string DisplayName { get; set; } = string.Empty;
        public Address Address { get; set; }
    } 
    #endregion
}
