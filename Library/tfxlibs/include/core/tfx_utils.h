////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
* @Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*            All rights reserved.
************************************************************************************
* @file     tfx_util.h
* @create   July 23, 2025
* @brief    Define utility functions
* @note     For conditions of distribution and use, see copyright notice in readme.txt
***********************************************************************************/

#ifndef TFX_UTILS_H
#define TFX_UTILS_H

#include <string>

#include "tfx_def.h"
#include "tfx_math.h"

__BEGIN_NAMESPACE__

template<typename U, typename T = U>
constexpr T Deg2Rad(const U dbAngleDeg)
{
	return static_cast<T>(dbAngleDeg) * static_cast<T>(M_PI) / static_cast<T>(180.0);
};

template<typename U, typename T = U>
constexpr T Rad2Deg(const U dbAngleRad)
{
	return static_cast<T>(dbAngleRad) * static_cast<T>(180.0) / static_cast<T>(M_PI);
};

#if _WIN_FLAT
#include <objbase.h>
#	pragma comment(lib, "ole32.lib")
#else
#	include <uuid/uuid.h>
#endif

static const std::string CreateGUID()
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

static size_t GetFileSize(const wchar_t* path)
{
	FILE* raw = nullptr;
	if (_wfopen_s(&raw, path, L"rb") != 0 || !raw)
		return 0;

	std::unique_ptr<FILE, decltype(&fclose)> file(raw, &fclose);

	if (!file)
		return 0;

	_fseeki64(file.get(), 0, SEEK_END);
	uint64_t size = _ftelli64(file.get());

	return size;
}

static std::string ReadFile(const wchar_t* path)
{
	FILE* raw = nullptr;
	if (_wfopen_s(&raw, path, L"rb") != 0 || !raw)
		return {};

	std::unique_ptr<FILE, decltype(&fclose)> file(raw, &fclose);

	if (_fseeki64(file.get(), 0, SEEK_END) != 0)
		return {};

	const uint64_t size = _ftelli64(file.get());
	if (size <= 0)
		return {};

	_fseeki64(file.get(), 0, SEEK_SET);

	std::string data;
	data.resize(static_cast<size_t>(size));

	size_t readBytes = fread(&data[0], 1, data.size(), file.get());
	data.resize(readBytes);

	return data;
}

__END_NAMESPACE__

#endif // !TFX_UTILS_H
