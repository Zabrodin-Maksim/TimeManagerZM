using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagerZM.Services;

namespace TimeManagerZM.ViewModel
{
    public class AuthorizationViewMdoel : ViewModelBase
    {
        private readonly MainViewModel _mainViewModel;
        public AuthorizationViewMdoel(MainViewModel mainViewModel) 
        {
            _mainViewModel = mainViewModel;
        }
    }
}
