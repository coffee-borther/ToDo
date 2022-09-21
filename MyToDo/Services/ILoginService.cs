﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyToDo.Shared;
using MyToDo.Shared.Dtos;

namespace MyToDo.Services
{
    public interface ILoginService
    {
        Task<ApiResponse<UserDto>> LoginAsync(UserDto dto);
        Task<ApiResponse> RegisterAsync(UserDto dto);

    }
}
