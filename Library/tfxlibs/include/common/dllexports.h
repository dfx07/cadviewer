#ifdef _WIN32
#    ifdef LIBRARY_EXPORTS
#        define LIBRARY_API __declspec(dllexport)
#        define LIBRARY_API_INLINE inline LIBRARY_API

#ifndef _NAME_MANGLING
#        define C_STYLE extern "C"
#else
#        define C_STYLE
#endif // _NAME_MANGLING

#    else
#        define LIBRARY_API __declspec(dllimport)
#    endif
#elif
#    define LIBRARY_API
#endif

