using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagerZM.Enums;

namespace TimeManagerZM.Services
{
    public class MyNavigationService
    {
        private readonly Action<ViewType> _navigateAction;

        public MyNavigationService(Action<ViewType> navigateAction)
        {
            _navigateAction = navigateAction;
        }

        public void Navigate(ViewType viewType) 
        {
            _navigateAction(viewType);
        }

    }
}
