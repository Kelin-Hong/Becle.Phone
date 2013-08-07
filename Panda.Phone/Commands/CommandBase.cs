using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Panda.Phone.Commands
{
    public class CommandBase:ICommand
    {
        Action<object> action;
        Func<object,bool> func;
        public CommandBase(Action<object> _action,Func<object,bool> _func)
        {
            this.action = _action;
            this.func = _func;
        }
        public bool CanExecute(object parameter)
        {
            return func(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            action(parameter);
        }
    }
}
