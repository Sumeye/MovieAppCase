using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAppCase.Core.Configuration
{
    public class Client
    {
        public string Id { get; set; }
        public string Secret { get; set; }

        /// <summary>
        /// www.myapi.com
        /// www.myapi2.com
        /// </summary>
        public List<string> Audiences { get; set; }
    }
}
