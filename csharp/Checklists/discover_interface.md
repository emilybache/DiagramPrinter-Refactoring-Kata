Discover Adapter Interface
==========================
* Copy this checklist into the code and make a commit with message: [intention] - Discover Adapter Interface
* Identify every call to 'wrapper.Awkward'. Go through each one and decide which checklist to use. Either 'Discover Adapter Method' or 'Propagate Adapter'. Add the relevant checklist as a comment.
* Follow the new checklists for each call to 'wrapper.Awkward'.
* Delete unnecessary comments and make a commit [completed] - Discover Adapter Interface

Discover Adapter Method
=======================
Use this checklist for code that uses wrapper.Awkward that you don't need to include in your unit tests.
* 'Extract method' for the code using 'wrapper.Awkward'. Preferably take the whole line of code into the new method. Make sure the wrapper class is one of the arguments.
* If the new method is not static, 'Introduce Parameter' for all the member variables it uses. Then make it static.
* 'Make method non static' and select the wrapper class as the instance to move it to.
* Delete unnecessary comments and make a commit [underway] - Discover Adapter Interface

Propagate Adapter
=================
Use this checklist when there is a method like: foo.bar(wrapper.Awkward) and you want to include the method 'bar' in your unit tests.
* On the 'foo' class, follow the checklist "Adapt Parameter - Adapter class exists already" for the 'bar' method.
* You should now be able to instead call the method 'foo.bar(wrapper)'.
* Delete unnecessary comments and make a commit [underway] - Discover Adapter Interface
* Go to the 'Discover Adapter Interface' checklist to continue to discover the interface needed, this time focusing on usages in the new method.