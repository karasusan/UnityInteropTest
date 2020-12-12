cmake_minimum_required(VERSION 3.18)

project(unsafe
  LANGUAGES C CXX
)

set(CMAKE_CXX_STANDARD 14)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

set(CMAKE_CXX_FLAGS_DEBUG "-g3 -O0 -D_DEBUG")

if(XCODE)
  add_library(unsafe MODULE)
  set_target_properties(unsafe 
    PROPERTIES
      BUNDLE TRUE
      BUNDLE_EXTENSION bundle
  )
else()
  add_library(unsafe SHARED)
endif()

target_sources(unsafe
  PRIVATE
    Plugin.cpp
)

set(PROJECT_BINARY_DIR "${CMAKE_SOURCE_DIR}/../Assets/Plugins/x64")
set_target_properties(unsafe
  PROPERTIES
    OUTPUT_NAME unsafe
    RUNTIME_OUTPUT_DIRECTORY_RELEASE ${PROJECT_BINARY_DIR}
    RUNTIME_OUTPUT_DIRECTORY_DEBUG ${PROJECT_BINARY_DIR}
    LIBRARY_OUTPUT_DIRECTORY_RELEASE ${PROJECT_BINARY_DIR}
    LIBRARY_OUTPUT_DIRECTORY_DEBUG ${PROJECT_BINARY_DIR}
)

if(MSVC)
  set(CMAKE_CXX_FLAGS_DEBUG "${CMAKE_CXX_FLAGS_DEBUG} -D_ITERATOR_DEBUG_LEVEL=0 /Zi")
  set_target_properties(WebRTCPlugin
    PROPERTIES 
      MSVC_RUNTIME_LIBRARY "MultiThreaded$<$<CONFIG:Debug>:Debug>"
  )
endif()
