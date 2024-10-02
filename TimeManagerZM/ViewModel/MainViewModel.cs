using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagerZM.Services;

namespace TimeManagerZM.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly MyNavigationService _navigationService;

        public MainViewModel(MyNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

         
    }
}
