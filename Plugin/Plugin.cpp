
template<typename T>
struct C
{
    T X;
};

extern "C"
{
    __stdcall void AddGenericInt(const C<int>* a, const C<int>* b, C<int>* c)
    {
        c->X = a->X + b->X;
    }
}