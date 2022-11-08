# MSTest Windows App SDK 1.1 Unit Test regression repro

This project demonstrates the regression in unit test behavior for Windows App SDK 1.1,
reported here:
https://github.com/microsoft/testfx/issues/1127

Root cause shows that the regression is due to the removal of this project property in
Windows App SDK 1.1:
<WindowsAppContainer>true</WindowsAppContainer>

This was necessary to address another issue, related to C++ library references:
http://task.ms/36741036

As a workaround, WindowsAppContainer=true can be explicitly added back to the project, 
but that reintroduces the break above, not allowing C++ library references.  

