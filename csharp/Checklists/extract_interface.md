Extract Interface for Testing
==============================

* In your Wrapper class, make the Awkward property private. Check it compiles. If it does not then you're not ready for this step yet.
* Check all the arguments and return values in the Wrapper class. If any of them are awkward (untestable) collaborators then you're not ready for this step yet.
* Copy this checklist into the code and make a commit with message: [intention] - Extract Interface for Testing
* Work out the role your Wrapper class plays in the method(s) you want to test. Think of a good name for the interface that doesn't include the word 'Adapter' or 'Wrapper'.
* In the Wrapper class, 'Extract Interface' and give it the name you chose. Select all public fields.
* In the method you want to test, change the parameter type to your new interface.
* 'Rename' the parameter to better match the interface name (remove the word 'Adapter' or 'Wrapper')
* Make sure the method you want to test is public or otherwise accessible from the test.
* Delete unnecessary comments and make a commit [completed] - Extract Interface for Testing


Write your Unit Tests
=====================
* Use a mocking framework like NSubstitute to replace the IWrapper instance.
* Configure the substitute with return values for all the methods.
* Test all the important scenarios.
