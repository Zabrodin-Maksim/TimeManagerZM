using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TimeManagerZM.View
{
    public class MyICommand : ICommand
    {
        private readonly Action _executeMethod;
        private readonly Func<bool> _canExecuteMethod;

        public MyICommand(Action executeMethod)
        {
            _executeMethod = executeMethod ?? throw new ArgumentNullException(nameof(executeMethod));
        }

        public MyICommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            _executeMethod = executeMethod ?? throw new ArgumentNullException(nameof(executeMethod));
            _canExecuteMethod = canExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return _canExecuteMethod == null || _canExecuteMethod();
        }

        public void Execute(object parameter)
        {
            _executeMethod?.Invoke();
        }

        public event EventHandler CanExecuteChanged;

        #endregion
    }
}

