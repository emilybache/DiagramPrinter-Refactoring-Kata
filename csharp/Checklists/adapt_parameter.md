Adapt Parameter - Adapter class does not yet exist
==================================================

* Copy this checklist into the code and make a commit with message: [intention] - Adapt Parameter
* 'Extract method' on whole body. Method name unimportant.
* 'Transform parameters' & create adapter class
* 'Inline variable' on the new variable assigned to Wrapper.Awkward
* 'Rename' new method to overload original
* Delete unnecessary comments and make a commit [completed] - Adapt Parameter

Adapt Parameter - Adapter class exists already
==============================================
These instructions assume the existing adapter class is called Wrapper and has a property Awkward to access the awkward type that is being adapted. The parameter is named 'awkward'.

* Copy this checklist into the code and make a commit with message: [intention] - Adapt Parameter
* On first line of method, var wrapper = new Wrapper(awkward).
* On second line, var awkward = wrapper.Awkward (this will cause a name clash)
* Manually rename the awkward parameter on the first line & the method argument to avoid name clash
* 'Extract method' from the second line onwards. This method should have the  Wrapper class as an argument. Keep other args in same order as original method, and use same name.
* 'Inline' awkward variable so everything uses the new wrapper instead
* 'Rename' parameter in original method back to what it was before.
* Delete unnecessary comments and make a commit [completed] - Adapt Parameter
