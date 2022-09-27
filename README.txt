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
* When saving there's no error checking when converting the csv datasets to int lists.

# Improvements:
* Right after starting, we could look to see what operations are supported and then precompute each datasets result for each operation. Then no math needs to happen during simulation.
* When saving, the json in saved un-formatted. This might work: https://stackoverflow.com/a/67928315/3128682