using System;
using System.Windows.Input;

namespace AGENT.PackageViewer.Command
{
    public class DelegateCommand : ICommand
    {
        private Func<object, bool> canExecute;
        private Action<object> executeAction;

        private Func<bool> canExecuteSimple;
        private Action executeActionSimple;

        public DelegateCommand(Action<object> executeAction)
            : this(executeAction, null)
        {
        }

        public DelegateCommand(Action executeAction)
            : this(executeAction, null)
        {
        }

        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecute)
        {
            if (executeAction == null)
            {
                throw new ArgumentNullException("executeAction");
            }
            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }

        public DelegateCommand(Action executeAction, Func<bool> canExecute)
        {
            if (executeAction == null)
            {
                throw new ArgumentNullException("executeAction");
            }
            this.executeActionSimple = executeAction;
            this.canExecuteSimple = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            bool result = true;
            Func<object, bool> canExecuteHandler = this.canExecute;
            if (canExecuteHandler != null)
            {
                result = canExecuteHandler(parameter);
                return result;
            }

            Func<bool> canExecuteHandlerSimple = this.canExecuteSimple;
            if (canExecuteHandlerSimple != null)
            {
                result = canExecuteHandlerSimple();
            }

            return result;
        }

        public bool CanExecute()
        {
            bool result = true;
            Func<bool> canExecuteHandler = this.canExecuteSimple;
            if (canExecuteHandler != null)
            {
                result = canExecuteHandler();
            }

            return result;
        }


        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChanged;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        public void Execute(object parameter)
        {
            // Default to action that takes parameter.
            if (this.executeAction != null)
            {
                this.executeAction(parameter);
                return;
            }

            // Fallback to parameterless delegate.
            if (this.executeActionSimple != null)
            {
                this.executeActionSimple();
                return;
            }
        }

        public void Execute()
        {
            this.executeActionSimple();
        }
    }
}