using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GarminExport.Auth
{

    public class Session
    {
        public CookieContainer Cookies { get; private set; }

        public Session()
        {
            Cookies = new CookieContainer();
        }
    }
}
