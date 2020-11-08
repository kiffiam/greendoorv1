﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenDoorV1.Services.Interfaces
{
    public interface IAuthService
    {
        Task Register();
        Task Login();
        Task Logout();
    }
}
