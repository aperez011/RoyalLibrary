using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RL.Utility.Interfaces
{
    public interface IJwtOptions
    {
        string Issuer { get; }
        string Audience { get; }
        byte[] SecretKey { get; }
    }
}
