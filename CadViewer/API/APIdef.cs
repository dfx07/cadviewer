using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadViewer.APIDef
{
	using BOOL = System.Int32;
	using INT = System.Int32;

	public partial class SharedAliases
	{
		// Aliases as type properties
		public static string AliasString => "Hello from shared alias!";
		public static System.Collections.Generic.List<int> AliasIntList => new System.Collections.Generic.List<int> { 1, 2, 3 };
	}
}
