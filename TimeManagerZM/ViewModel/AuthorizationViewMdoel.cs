using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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

        // Private Fields
        private string _userName;
        private string _password;

        private string _attentionTextUser;
        private string _attentionTextPassword;

        // Text for attention
        private string smallPasswordText = "The password must contain \nat least 8 characters.";
        private string wrongLogin = "Username is not found or";
        private string wrongPassword = "Wrong Password!";

        // Properties
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value, nameof(UserName)); }
        }
        public string UserPassword
        {
            get => _password;
            set => _password = value;
        }

        public string AttentionTextUser
        {
            get => _attentionTextUser;
            set { SetProperty(ref _attentionTextUser, value, nameof(AttentionTextUser)); }
        }
        public string AttentionTextPassword
        {
            get => _attentionTextPassword;
            set { SetProperty(ref _attentionTextPassword, value, nameof(AttentionTextPassword)); }
        }

        public AuthorizationViewMdoel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            //RegisterCommand = new MyICommand<object>(async parameter => await Register(parameter));
            RegisterCommand = new MyICommand<object>(async parameter => await Register(parameter));
            LoginCommand = new MyICommand<object>(async parameter => await Login(parameter));
        }

        
        private async Task Register(object parameter)
        {
            AttentionTextPassword = "";
            AttentionTextUser = "";
            if (parameter is PasswordBox passwordBox) 
            {
                UserPassword = passwordBox.Password;

                // Check Password by rules 
                if (!IsPasswordOk(UserPassword))
                {
                    AttentionTextPassword = smallPasswordText;  
                } 
                else // is ok
                {
                    AttentionTextPassword = "";

                    // Send to Database
                    await _mainViewModel.AddNewUser(UserName, UserPassword);

                    Debug.WriteLine("[INFO] A new User was successfully added!");
                }
                UserPassword = String.Empty; // Clear Property
            }
        }

        // TODO: LOGIN
        private async Task Login(object parameter)
        {
            if (parameter is PasswordBox passwordBox)
            {
                UserPassword = passwordBox.Password;

                // Check Password by rules 
                if (!IsPasswordOk(UserPassword))
                {
                    AttentionTextPassword = smallPasswordText;
                    AttentionTextUser = wrongLogin;
                }
                else // is ok
                {
                    // Check exist user 
                    if ( !await _mainViewModel.GetUser(UserName, UserPassword))
                    {
                        AttentionTextUser = wrongLogin;
                        AttentionTextPassword = wrongPassword;
                    }
                    // clear Attentions
                    else
                    {
                        AttentionTextPassword = "";
                        AttentionTextUser = "";
                    }
                }
                UserPassword = String.Empty; // Clear Property
            }
        }

        private bool IsPasswordOk(string password)
        {
            if (String.IsNullOrEmpty(UserPassword) || UserPassword.Length < 8) return false;
            else return true;
        }
    }
}
