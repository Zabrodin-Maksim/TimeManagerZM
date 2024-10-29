using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeManagerZM.Services;
using TimeManagerZM.View;

namespace TimeManagerZM.ViewModel
{
    public class AuthorizationViewMdoel : ViewModelBase
    {
        private readonly MainViewModel _mainViewModel;

        // Commands
        public ICommand RegisterCommand { get; }
        public ICommand LoginCommand { get; }

        private string _userName;
        private string _password;

        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value, nameof(UserName)); }
        }

        public string UserPassword
        {
            get => _password;
            set => SetProperty(ref _password, value, nameof(UserPassword));
        }


        public AuthorizationViewMdoel(MainViewModel mainViewModel) 
        {
            _mainViewModel = mainViewModel;
            RegisterCommand = new MyICommand(Register);
            LoginCommand = new MyICommand(Login);
        }

        // TODO: Дописать логику проверки пароля и реализацию кнопок
        private void Register()
        {
            if (UserPassword.Length >= 8 && !String.IsNullOrEmpty(UserPassword))
            {
            }
            else { }
        }

        private void Login()
        {

        }
    }
}
