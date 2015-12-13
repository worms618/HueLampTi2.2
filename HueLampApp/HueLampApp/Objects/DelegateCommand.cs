using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HueLampApp.Objects
{
    public class DelegateCommand : ICommand
    {
        private Action _commandHandler;

        public DelegateCommand(Action commandHandler)
        {
            _commandHandler = commandHandler;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            //System.Diagnostics.Debug.WriteLine($"Paramater type: {parameter.GetType()}");
            return true;
        }

        public void Execute(object parameter)
        {
            //System.Diagnostics.Debug.WriteLine($"Paramater type: {parameter.GetType()}");
            _commandHandler();
        }
    }
}
