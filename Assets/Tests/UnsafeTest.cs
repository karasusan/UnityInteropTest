using System;
using System.Runtime.InteropServices;
using NUnit.Framework;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Collections;

public class UnsafeTest
{
#if UNITY_EDITOR_OSX
    private const string dllName = "unsafe";
#elif UNITY_EDITOR_WIN
    private const string dllName = "unsafe.dll";
#elif UNITY_STANDALONE
    private const string dllName = "unsafe";  
#endif     
    
    struct TypeA<T> where T : unmanaged           
    {
        public T X;
        
        public static unsafe explicit operator void*(TypeA<T> a)
        {
            var ptr = UnsafeUtility.Malloc(UnsafeUtility.SizeOf<TypeA<T>>(), 4, Allocator.Temp);
            UnsafeUtility.CopyStructureToPtr(ref a, ptr);
            return ptr;
        }
        public static unsafe explicit operator TypeA<T>(void* ptr)
        {
            UnsafeUtility.CopyPtrToStructure(ptr, out TypeA<T> c);
            return c;
        }
    }
    
    struct TypeB<T> where T : unmanaged           
    {
        public T X;
        
        public static explicit operator IntPtr(TypeB<T> a)
        {
            var ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf<TypeB<int>>());
            Marshal.StructureToPtr(a, ptr, false);
            return ptr;
        }
        public static explicit operator TypeB<T>(IntPtr ptr)
        {
            return Marshal.PtrToStructure<TypeB<T>>(ptr);
        }
    }

    [Test]
    public unsafe void TypeATest()
    {
        var a = new TypeA<int> {X = 1};
        var b = new TypeA<int> {X = 2};
        var ptr = UnsafeUtility.Malloc(UnsafeUtility.SizeOf<TypeA<int>>(), 4, Allocator.Temp);
        AddGenericInt((void*)a, (void*)b, ptr);
        var c = (TypeA<int>) ptr;
        Assert.AreEqual(a.X + b.X, c.X);
    }

    [Test]
    public unsafe void TypeBTest()
    {
        var a = new TypeB<int> {X = 1};
        var b = new TypeB<int> {X = 2};
        var ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf<TypeB<int>>());
        AddGenericInt((void*)(IntPtr)a, (void*)(IntPtr)b, (void*)ptr); 
        var c = (TypeB<int>) ptr;
        Assert.AreEqual(a.X + b.X, c.X);
    }   
    
    [DllImport(dllName)]
    static extern unsafe void AddGenericInt(void* ptr1, void* ptr2, void* ptr3);
}