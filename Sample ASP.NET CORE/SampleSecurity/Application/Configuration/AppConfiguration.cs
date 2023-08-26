using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Configurations
{
    public class AppConfiguration
    {
        public string DatabaseConnection { get; set; }
        public string JWTAcessSecretKey { get; set; }
        public string JWTRefreshSecretKey { get; set; }
    }
}
