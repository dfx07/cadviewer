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
		public struct BOOL
		{
			private Int32 value;
			public BOOL(Int32 value) => this.value = value;
			public static implicit operator BOOL(Int32 value) => new BOOL(value);
			public static implicit operator Int32(BOOL b) => b.value;
		}

		public struct INT
		{
			private Int32 value;
			public INT(Int32 value) => this.value = value;
			public static implicit operator INT(Int32 value) => new INT(value);
			public static implicit operator Int32(INT i) => i.value;
		}

		public static BOOL TRUE = 1;
		public static BOOL FALSE = 0;
	}
}
