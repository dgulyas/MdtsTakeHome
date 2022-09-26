# To run Step 1:
1. Build Solution.
2. Navigate to \MdtsTakeHome\Processor\bin\Debug\net6.0
3. Execute Processor.exe

# Assumptions:
* A generator interval is an integer.
* Computing the output of all generators will take less than 1 second.

# Issues:
* There are some TODOs in the code.
* There are some floating point math issues making Step 1's output not match the spec.

# Improvements:
* Right after starting, we could look to see what operations are supported and then precompute each datasets result for each operation. Then no math needs to happen during simulation.