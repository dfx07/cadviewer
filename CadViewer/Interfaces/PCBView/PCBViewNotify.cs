using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadViewer.Interfaces
{
	public enum EnumPCBViewEvent
	{
		MOUSE_MOVE	= 0x000001,
		MOUSE_ENTER	= 0x000002,
		MOUSE_DOWN	= 0x000004,
		MOUSE_UP	= 0x000008,
		MOUSE_WHEEL	= 0x000010,
		MOUSE_DRAG	= 0x000020,
		KEY_DOWN	= 0x000040,
		KEY_UP		= 0x000080,
	}

	public enum EnumPCBViewMsg
	{
		SET_TITLE_MSG,
		SHOW_MENU_CONTENT,
		DISABLE_EVENTS,
		ENABLE_EVENTS,
		GET_CTRL_KEY_STATE,
	}

	public interface IPCBViewHandlerListener
	{
		int SendUIMessage(EnumPCBViewMsg msg, int nData, float fData, string strData, object pObjectData);

		int GetIntUIData(EnumPCBViewMsg msg, int lParam, int wParam);
		string GetStringUIData(EnumPCBViewMsg msg, int lParam, int wParam);
	}
}
