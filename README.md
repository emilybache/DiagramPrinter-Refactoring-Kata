Diagram Printer Refactoring Kata
================================

This exercise was inspired by some production code I saw once. There are several characteristics that make it difficult unit test, so that's one of the things you can practice with this exercise. The other thing you can work on is purely improving the design with safe refactorings that you feel confident to do without tests.

The rest of the system, (which you need to imagine because it's not included here), calls the public methods on the 'DiagramPrinter' class. You should not change the signature of these public methods. You should try not to change any code that is marked as not being possible to call from a unit test.

Unit testing challenge
----------------------

You'd like to write some unit tests for the DiagramPrinter class and the DiagramPhysicalPrinter class. This is made more difficult because the FlowchartDiagram  that they both use is absolutely huge and impossible to construct in a unit test. There are also several other related classes that are similarly awkward.  

There are two tests to start you off, but the coverage is low. The "PrintSummary" method is easier to begin with, but you want to get the other methods under test too.

This kata is to help you practice dependency breaking techniques and safe refactorings to get some awkward code under test.

Refactoring challenge
---------------------

If you'd prefer to skip the unit testing challenge and jump straight into improving the readability and structure of the code, there is an additional branch 'with_tests' that has some good unit tests. They are not completely comprehensive though so you will need to take care with your refactoring.

Acknowledgements
----------------

This exercise was designed by Emily Bache
