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
		public static Int32 TRUE = 1;
		public static Int32 FALSE = 0;
	}
}
