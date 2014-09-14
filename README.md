AsyncUnitTests
==============
The purpose of this demo project is to prototype a clean methodology for running automated tests against asynchronous processes which don't have a well-defined continuation mechanism. An example of this is a common pattern of tests written against BizTalk integrations:
- Copy an input message as a file to an input folder
- Wait a period of time for various BizTalk artefacts to process the incoming message
- Pick up an output file containing the outgoing message and assert against this.

This can get cumbersome because there isn't an easy way for a test method to receive a notification that the process being tested has completed. The idea of this demo project is to abstract filesystem events into an instance that can be awaited, making it easier to fire off a large number of test scenarios at once and complete each test method as the results arrive. This does require that the test framework being used supports parallelised tests, which MSTest is meant to, but I have had some issues with getting this to behave correctly, so doing this properly is stil outstanding.
