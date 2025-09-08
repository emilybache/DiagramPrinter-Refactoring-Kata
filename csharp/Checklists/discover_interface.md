Discover Adapter Interface
==========================
* Copy this checklist into the code and make a commit with message: [intention] - Discover Adapter Interface
* Identify every call to 'wrapper.Awkward' and mark it with a comment containing the checklist "Discover Adapter Method" (below)
* Follow the new checklists for each call to 'wrapper.Awkward'.

Discover Adapter Method
=======================
* 'Extract method' for the code using 'wrapper.Awkward'. Preferably take the whole line of code into the new method. Make sure the wrapper class is one of the arguments.
* If the new method is not static, 'Introduce Parameter' for all the member variables it uses. Then make it static.
* 'Make method non static' and select the wrapper class as the instance to move it to.
* Delete unnecessary comments and make a commit [completed] - Discover Adapter Interface

