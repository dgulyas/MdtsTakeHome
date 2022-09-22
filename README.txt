#To run Step 1:
1. Build Solution.
2. Navigate to \MdtsTakeHome\Processor\bin\Debug\net6.0
3. Execute Processor.exe

#Assumptions:
* A generator interval is an integer.

#Issues:
* The current approach uses Sleep(1000) which waits exactly 1 second before continuting execution. Since the rest of the statements in the loop take time to execute, each loop takes slightly longer than 1 second. Over a long enough time this extra time will cause a second to be missed. I have ideas for fixing this, but they would take to long to experiment with.
* There are some TODOs in the code.