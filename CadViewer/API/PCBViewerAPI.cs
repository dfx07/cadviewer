using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CadViewer.Properties;
using CadViewer.APIDef;

namespace CadViewer.API
{
	public static class DLL
	{
		public const string PCBNameDll = "PCBView.dll";
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ContextConfig
	{
		public BaseAPI.BOOL m_bUseContextExt;
		public BaseAPI.INT  m_nAntialiasingLevel; // 0~8
	};

	class PCBViewerAPI : BaseAPI
	{
		[DllImport(DLL.PCBNameDll, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SetCallbackFunctionNotifyUI(IntPtr pHandle, CallbackFunctionNotifyUI pCall);


		[DllImport(DLL.PCBNameDll, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr CreatePCBView(IntPtr pHandle);

		[DllImport(DLL.PCBNameDll, CallingConvention = CallingConvention.Cdecl)]
		public static extern int CreateContext(IntPtr pHandle, ContextConfig _ctxConfig);


		[DllImport(DLL.PCBNameDll, CallingConvention = CallingConvention.Cdecl)]
		public static extern void DestroyPCBView(IntPtr pHandle);


		[DllImport(DLL.PCBNameDll, CallingConvention = CallingConvention.Cdecl)]
		public static extern void DrawLine(IntPtr pPCB, INT a);


		[DllImport(DLL.PCBNameDll, CallingConvention = CallingConvention.Cdecl)]
		public static extern void Draw(IntPtr pPCB);


		[DllImport(DLL.PCBNameDll, CallingConvention = CallingConvention.Cdecl)]
		public static extern void Clear(IntPtr pPCB);


		[DllImport(DLL.PCBNameDll, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SetView(IntPtr pPCB, INT nWidth, INT nHeight);

	}
}
