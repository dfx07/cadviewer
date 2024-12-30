using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CadViewer.API
{
	public class GCHandleProvider : IDisposable
	{
		public GCHandleProvider(object target)
		{
			Handle = GCHandle.Alloc(target);
		}

		public IntPtr Pointer => GCHandle.ToIntPtr(Handle);

		public GCHandle Handle { get; }

		private void ReleaseUnmanagedResources()
		{
			if (Handle.IsAllocated) Handle.Free();
		}

		public void Dispose()
		{
			ReleaseUnmanagedResources();
			GC.SuppressFinalize(this);
		}

		~GCHandleProvider()
		{
			ReleaseUnmanagedResources();
		}
	}
}
