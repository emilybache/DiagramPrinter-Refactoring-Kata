Extract Interface for Testing
==============================
* Check all the arguments and return values in the Wrapper class. If any of them are awkward (untestable) collaborators then you're not ready for this step yet.
* Copy this checklist into the code and make a commit with message: [intention] - Extract Interface for Testing
* Work out the role your Wrapper class plays in the method(s) you want to test. Think of a good name for the interface that doesn't include the word 'Adapter' or 'Wrapper'.
* In the Wrapper class, 'Extract Interface' and give it the name you chose. Select all public methods but not the Awkward property.
* In the method you want to test, change the type it is using to be your new interface.
* 'Rename' the variable or parameter to better match the interface name (remove the word 'Wrapper' or 'Adapter')
* Delete unnecessary comments and make a commit: [completed] - Extract Interface for Testing

Write your Unit Tests
=====================
* Use a mocking framework like NSubstitute to replace the IWrapper instance.
* Configure the substitute with return values for all the methods.
* Test all the important scenarios.
