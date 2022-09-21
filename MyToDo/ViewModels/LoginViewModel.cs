using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyToDo.Common;
using MyToDo.Extesions;
using MyToDo.Services;
using MyToDo.Shared.Dtos;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace MyToDo.ViewModels
{
    public class LoginViewModel : BindableBase, IDialogAware
    {
        private readonly LoginService _loginService;
        private readonly IEventAggregator _aggregator;

        public LoginViewModel(LoginService loginService, IEventAggregator aggregator)
        {
            UserDto = new RegisterUserDto();
            _loginService = loginService;
            _aggregator = aggregator;
        }

        #region Prop

        public string Title { get; } = "ToDo";
        public event Action<IDialogResult>? RequestClose;

        [AllowNull] private string _account;

        public string Account
        {
            get => _account;
            set
            {
                _account = value;
                RaisePropertyChanged();
            }
        }

        [AllowNull] private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }

        private int _seletedIndex;

        public int SelectedIndex
        {
            get => _seletedIndex;
            set
            {
                _seletedIndex = value;
                RaisePropertyChanged();
            }
        }

        private RegisterUserDto _userDto;

        public RegisterUserDto UserDto
        {
            get => _userDto;
            set
            {
                _userDto = value;
                RaisePropertyChanged();
            }
        }

        private string _userName;

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Command

        public DelegateCommand<string> ExecuteCommand => new DelegateCommand<string>(Execute);

        #endregion

        #region Method

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "Login":
                    Login();
                    break;
                case "Logout":
                    Logout();
                    break;
                case "Return":
                    SelectedIndex = 0;
                    break;
                case "Register":
                    Register();
                    break;
                case "RegisterPage":
                    SelectedIndex = 1;
                    break;
            }
        }

        private async void Register()
        {
            if (string.IsNullOrWhiteSpace(UserDto.Account) ||
                string.IsNullOrWhiteSpace(UserDto.UserName) ||
                string.IsNullOrWhiteSpace(UserDto.Password) ||
                string.IsNullOrWhiteSpace(UserDto.NewPassWord))
            {
                _aggregator.SendMessage("不可为空，请检查是否填写正确！", "Login");
                return;   
            }
            var registerResult = await _loginService.RegisterAsync(new UserDto()
            {
                Account = UserDto.Account,
                UserName = UserDto.UserName,
                Password = UserDto.Password
            });
            if (registerResult != null && registerResult.Status)
            {
                //注册成功,返回登录页页面
                SelectedIndex = 0;
                _aggregator.SendMessage(registerResult.Message, "Login");
                return;
            }
            _aggregator.SendMessage("注册失败！", "Login");
        }

        private async void Login()
        {
            if (string.IsNullOrWhiteSpace(Account) || string.IsNullOrWhiteSpace(Password))
            {
                _aggregator.SendMessage("请输入完整的注册信息！", "Login");
                return;
            }
            var loginResult = await _loginService.LoginAsync(new UserDto()
            {
                Account = Account,
                Password = Password
            });
            if (UserDto.Password != UserDto.NewPassWord)
            {
                _aggregator.SendMessage("密码不一致,请重新输入！", "Login");
                return;
            }
            if (loginResult != null && loginResult.Status)
            {
                AppSession.UserName = loginResult.Result.UserName;
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
                return;
            }
            _aggregator.SendMessage("登录失败", "Login");
        }

        private void Logout()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.No));
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            Logout();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }

        #endregion
    }
}
