# UnityInteropTest
Test project about using C++ Interop on Unity

## Motivation

According to [this document](https://docs.microsoft.com/ja-jp/dotnet/csharp/language-reference/proposals/csharp-7.3/blittable), generic types are not able to used by interop code.

> The primary motivation is to make it easier to author low level interop code in C#. **Unmanaged types are one of the core building blocks for interop code, yet the lack of support in generics makes it impossible to create re-usable routines across all unmanaged types.** Instead developers are forced to author the same boiler plate code for every unmanaged type in their library...

### Comparison between `UnsafeUtility` and `Marshal`

This project verifies the behaviors of the two classes below.

- `Unity.Collections.LowLevel.Unsafe.UnsafeUtility`
- `System.Runtime.InteropServices.Marshal`

Interestingly, Results of unit-test depend on the `scripting backend` parameter in the player settings.

|          | `UnsafeUtility`          | `Marshal`               |
-----------|--------------------------|-------------------------| 
| `Mono`   | :white_check_mark:  pass | :white_check_mark: pass |
| `IL2CPP` | :white_check_mark:  pass | :bangbang: **fail**     |

Test code is [here](Assets/Tests/UnsafeTest.cs).

## Build native code

### macOS
- CMake 3.18 or later
- XCode 11.3.1 or later

```sh
cd Plugin
cmake -G Xcode -B build
cmake --build build --config Release --target unsafe
```
