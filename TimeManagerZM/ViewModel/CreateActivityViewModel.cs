using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagerZM.ViewModel
{
    public class CreateActivityViewModel : ViewModelBase
    {
        private readonly MainViewModel _mainViewModel;

        public CreateActivityViewModel(MainViewModel mainViewModel) 
        { 
            _mainViewModel = mainViewModel;
        }
    }
}
