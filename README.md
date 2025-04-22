Diagram Printer Refactoring Kata
================================

You'd like to write some unit tests for the DiagramPrinter class. This is made more difficult because the IDiagram interface is absolutely huge and the implementation class (not included here) is impossible to construct in a unit test.

There are two tests to start you off, but the coverage is low. The "PrintSummary" method is easier to begin with, but you also want to get the "PrintDiagram" method under test too.

This kata is to help you practice dependency breaking techniques and safe refactorings to get an awkward class under test.

Additional Branches
-------------------

There are two additional branches you could look at in this repo:

* with_tests - has unit tests and demonstrates several dependency-breaking techniques
* sample_solution - decomposes the conditionals to improve readability


Acknowledgements
----------------

This exercise was designed by Emily Bache
