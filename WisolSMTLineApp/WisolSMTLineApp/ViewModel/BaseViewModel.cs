using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WisolSMTLineApp.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class ViewObj : INotifyPropertyChanged
    {
        string _key;
        public string Key
        {
            get { return _key; }
            set
            {
                if (_key != value)
                {
                    _key = value;
                    NotifyPropertyChanged(nameof(Key));
                }
            }
        }

        string _Text;
        public string Text
        {
            get { return _Text; }
            set
            {
                if (_Text != value)
                {
                    _Text = value;
                    NotifyPropertyChanged(nameof(Text));
                }
            }
        }
        string _Color;
        public string Color
        {
            get
            {
                return _Color;
            }
            set
            {
                if (_Color != value)
                {
                    _Color = value;
                    NotifyPropertyChanged(nameof(Color));
                }
            }
        }
        void NotifyPropertyChanged(string ProName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(ProName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

    class CommandExecute : ICommand
    {
        #region ICommand Members  

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            //Your Code  
        }
        #endregion
    }

    public class CommandHandler : ICommand
    {
        private Action<object> _action;
        private Action _NoParaAction;
        private Func<bool> _canExecute;

        /// <summary>
        /// Creates instance of the command handler
        /// </summary>
        /// <param name="action">Action to be executed by the command</param>
        /// <param name="canExecute">A bolean property to containing current permissions to execute the command</param>
        public CommandHandler(Action<object> action, Func<bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }
        public CommandHandler(Action action, Func<bool> canExecute)
        {
            _NoParaAction = action;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Wires CanExecuteChanged event 
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Forcess checking if execute is allowed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            if (parameter != null)
                _action(parameter);
            else
                _NoParaAction();
        }
    }
}
