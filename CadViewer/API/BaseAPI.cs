using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CadViewer.API
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void CallbackFunctionNotifyUI(string message, int nParam, int nWaram);

	public class BaseAPI
	{
		public struct _BOOL
		{
			private Int32 value;
			public _BOOL(Int32 value) => this.value = value;
			public static implicit operator _BOOL(Int32 value) => new _BOOL(value);
			public static implicit operator Int32(_BOOL b) => b.value;
		}

		public struct _INT
		{
			private Int32 value;
			public _INT(Int32 value) => this.value = value;
			public static implicit operator _INT(Int32 value) => new _INT(value);
			public static implicit operator Int32(_INT i) => i.value;
		}

		public struct _FLOAT
		{
			private float value;
			public _FLOAT(float value) => this.value = value;
			public static implicit operator _FLOAT(float value) => new _FLOAT(value);
			public static implicit operator float(_FLOAT i) => i.value;
		}

		public static _BOOL TRUE = 1;
		public static _BOOL FALSE = 0;
	}
}
