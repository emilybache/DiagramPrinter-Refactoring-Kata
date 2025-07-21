Diagram Printer Refactoring Kata - With Tests
=============================================

There are two main exercises in this repository, translated into several programming languages.

Refactoring challenge
----------------------

Improve the design of the code in 'DiagramPrinter' and 'DiagramPhysicalPrinter' without breaking the unit tests. In particular, improve the design of these two methods, which are the only ones with any significant test coverage:

DiagramPrinter.PrintDiagram()
DiagramPhysicalPrinter.DoPrint()

The rest of the system, (which you need to imagine because it's not included here), calls the public methods on the 'DiagramPrinter' class. You should not change the signature of these public methods. You should try not to change any code that is marked as not being possible to call from a unit test. 


Unit testing challenge
----------------------

Go to the branch 'main'.

You'd like to write some unit tests for the DiagramPrinter class and the DiagramPhysicalPrinter class. This is made more difficult because the FlowchartDiagram  that they both use is absolutely huge and impossible to construct in a unit test. There are also several other related classes that are similarly awkward.  

There are some tests to start you off, but the coverage is low. The "PrintSummary" method is easier to begin with, but you want to get the other methods under test too.

This kata is to help you practice dependency breaking techniques and safe refactorings to get some awkward code under test.


Acknowledgements
----------------

This exercise was designed by Emily Bache