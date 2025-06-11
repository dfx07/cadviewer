/*Define type data macro*/

#define IntPtr	void*

#ifndef _INT
#define _INT	int
#endif // !Int

#ifndef _BOOL
#define _BOOL	int
#endif // !Bool

#ifndef _FLOAT
#define _FLOAT	float
#endif // !FLOAT

#ifndef _DOUBLE
#define _DOUBLE	double
#endif // !DOUBLE

#ifndef TRUE
#define TRUE	1
#endif // !TRUE

#ifndef FALSE
#define FALSE	0
#endif // !FALSE


#define NULL_RETURN(ptr, ...) if (ptr == nullptr) return __VA_ARGS__
