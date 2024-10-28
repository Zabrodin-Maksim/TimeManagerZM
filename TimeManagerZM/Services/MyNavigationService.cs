using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagerZM.Enums;
using TimeManagerZM.ViewModel;

namespace TimeManagerZM.Services
{
    public class MyNavigationService
    {
        private readonly MainViewModel _mainViewModel;

        public MyNavigationService(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public void Navigate(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Authorization:
                    _mainViewModel.CurrentViewModel = new AuthorizationViewMdoel(_mainViewModel); // To Authorization
                    break;
            }
        }

    }
}
