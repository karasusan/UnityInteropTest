#include <cstdint>

#if _MSC_VER // this is defined when compiling with Visual Studio
#define EXPORT_API __declspec(dllexport) // Visual Studio needs annotating exported functions with this
#else
#define EXPORT_API // XCode does not need annotating exported functions, so define is empty
#endif

template<typename T>
struct C
{
    T X;
};

template<typename T>
struct Nullable
{
    T value;
    bool hasValue;

	Nullable<T> operator +(const Nullable<T>& a) const
	{
        Nullable<T> c;
        if(!this->hasValue && !a.hasValue)
        {
            c.hasValue = false;
            c.value = 0;
            return c;
        }
		
        c.hasValue = true;
        c.value = (this->hasValue ? this->value : 0) + (a.hasValue ? a.value : 0);
        return c;
	}
};

extern "C"
{
EXPORT_API void AddGenericInt(const C<int32_t>* a, const C<int32_t>* b, C<int32_t>* c)
{
    c->X = a->X + b->X;
}

EXPORT_API void AddGenericNullableInt(const C<Nullable<int32_t>>* a, const C<Nullable<int32_t>>* b, C<Nullable<int32_t>>* c)
{
    c->X = a->X + b->X;
}

EXPORT_API void AddGenericNullableLong(const C<Nullable<int64_t>>* a, const C<Nullable<int64_t>>* b, C<Nullable<int64_t>>* c)
{
    c->X = a->X + b->X;
}

}