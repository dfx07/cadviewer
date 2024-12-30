/*Define type data macro*/

#define IntPtr	void*

#ifndef Int
#define Int		int
#endif // !Int

#ifndef Bool
#define Bool	int
#endif // !Bool

#define Float	float
#define Double	double

#define True	1
#define False	0


#define NULL_RETURN(ptr, r) if(ptr == NULL) return r;


