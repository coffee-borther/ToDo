using System;
using System.Security.Principal;
using System.Threading.Tasks;
using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Extensions;

namespace MyToDo.Api.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IMapper _iMapper;

        public LoginService(IUnitOfWork iUnitOfWork, IMapper iMapper)
        {
            _iUnitOfWork = iUnitOfWork;
            _iMapper = iMapper;
        }

        public async Task<ApiResponse> Login(string account, string password)
        {
            try
            {
                password = password.GetMD5();
                var model = await _iUnitOfWork.GetRepository<User>().GetFirstOrDefaultAsync(predicate:
                    x => x.Account.Equals(account) && x.Password.Equals(password));
                if (model == null)
                {
                    return new ApiResponse(false, "账号密码错误，请重试！");
                }

                return new ApiResponse(true, model);
            }
            catch (Exception e)
            {
                return new ApiResponse(false, $"登录失败 {e}");
            }
        }

        public async Task<ApiResponse> Register(UserDto userDto)
        {
            try
            {
                var model = _iMapper.Map<User>(userDto);
                var repository = _iUnitOfWork.GetRepository<User>();
                var result = await repository.GetFirstOrDefaultAsync(predicate: x => x.Account.Equals(model.Account));
                if (result != null)
                {
                    return new ApiResponse(false, $"当前账号{result.Account}已存在");
                }

                model.CreateDate = DateTime.Now;
                model.Password = model.Password.GetMD5();
                await repository.InsertAsync(model);
                if (await  _iUnitOfWork.SaveChangesAsync() > 0)
                {
                    return new ApiResponse(true, model);
                }

                return new ApiResponse(false, "注册失败！");
            }
            catch (Exception e)
            {
                return new ApiResponse(false, $"注册失败 {e}");
            }
        }
    }
}