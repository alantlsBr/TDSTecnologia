﻿using System;
using System.Collections.Generic;
using System.Text;
using TDSTecnologia.Site.Infrastructure.Data;

namespace TDSTecnologia.Site.Infrastructure.Services
{
    class BasicService
    {
        protected readonly AppContexto _context;

        public BasicService(AppContexto context)
        {
            _context = context;
        }
    }
}
