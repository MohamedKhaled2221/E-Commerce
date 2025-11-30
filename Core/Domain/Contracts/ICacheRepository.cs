using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ICacheRepository
    {
        // Get Cached Response
        Task<string?> GetAsync(string key);
        // Set Cached Response --> key , value , expiration time
        Task SetAsync (string key, object value, TimeSpan? duration );
     
    }
}
