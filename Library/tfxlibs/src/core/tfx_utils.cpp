#include "core/tfx_utils.h"

#if _WIN_FLAT
#include <objbase.h>
#	pragma comment(lib, "ole32.lib")
#else
#	include <uuid/uuid.h>
#endif

__BEGIN_NAMESPACE__

const std::string CreateGUID()
{

}

size_t GetFileSize(const wchar_t* path)
{
	return size_t();
}

std::string ReadFile(const wchar_t* path)
{
	
}

__END_NAMESPACE__
