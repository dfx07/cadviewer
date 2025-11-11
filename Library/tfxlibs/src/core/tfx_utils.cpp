#include "core/tfx_utils.h"

#if _WIN_FLAT
#include <objbase.h>
#	pragma comment(lib, "ole32.lib")
#else
#	include <uuid/uuid.h>
#endif

__BEGIN_NAMESPACE__

std::string CreateGUID()
{
#if _WIN_FLAT
	GUID guid;
	if (CoCreateGuid(&guid) != S_OK)
		return "";

	char buf[64];
	snprintf(buf, sizeof(buf),
		"%08X-%04X-%04X-%04X-%012llX",
		guid.Data1, guid.Data2, guid.Data3,
		(guid.Data4[0] << 8) | guid.Data4[1],
		((unsigned long long)guid.Data4[2] << 40) |
		((unsigned long long)guid.Data4[3] << 32) |
		((unsigned long long)guid.Data4[4] << 24) |
		((unsigned long long)guid.Data4[5] << 16) |
		((unsigned long long)guid.Data4[6] << 8) |
		((unsigned long long)guid.Data4[7])
	);

	return buf;
#else
	uuid_t uuid;
	uuid_generate(uuid);

	char str[37];
	uuid_unparse_lower(uuid, str);
	return str;
#endif
}

__END_NAMESPACE__
