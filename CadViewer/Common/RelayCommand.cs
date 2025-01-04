﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CadViewer.Common
{
	/*
	 With parameters
	 */
	public class RelayCommand<T> : ICommand
	{
		private readonly Action<T> _execute;
		private readonly Func<bool> _canExecute;

		public event EventHandler CanExecuteChanged;

		public RelayCommand(Action<T> execute, Func<bool> canExecute = null)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

		public void Execute(object parameter) => _execute((T)parameter);

		public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}

	/*
	 Without parameters
	 */
	public class RelayCommand : ICommand
	{
		private readonly Action _execute;
		private readonly Func<bool> _canExecute;

		public event EventHandler CanExecuteChanged;

		public RelayCommand(Action execute, Func<bool> canExecute = null)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

		public void Execute(object parameter) => _execute();

		public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}
}
