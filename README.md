Git Repo at https://github.com/dgulyas/MdtsTakeHome

For spec see https://github.com/dgulyas/MdtsTakeHome/blob/main/MdtsTakeHomeSpec.pdf

## To run Step 1:
1. Build Solution.
2. Navigate to \MdtsTakeHome\Processor\bin\Debug\net6.0
3. Execute Processor.exe

## To run Step 2:
1. Build Solution
2. Navigate to \MdtsTakeHome\Interface\bin\Debug\net6.0-windows
3. Execute Interface.exe

## To run tests:
1. Open solution in visual studio
2. Click on Test > Run All Tests

## Assumptions:
* A generator interval is an integer.
* Computing the output of all generators each second will take less than 1 second.
* Within the same second, the order of output doesn't matter.

## Issues:
* There are some TODOs in the code.
* There are some floating point math issues making Step 1's output not match the spec.
* There's a general lack of data correctness checking. Putting "a" into a dataset for example causes an exception.
* I don't know how the customer is going to use this, so the layout of the wpf controls might suck. I'd want to talk to them and the product owner about the layout. The DGVs could be taller for example. Get some sample data.json files from them.

## Improvements:
* Right after starting, we could look to see what operations are supported and then precompute each datasets result for each operation. Then no math needs to happen during simulation.
* When saving, the json in saved as one line/ugly. This might work: https://stackoverflow.com/a/67928315/3128682
* None of the stuff on the DGV is locked down. Some people don't want to be able to resize/reorder the columns.
* There could be some indication that the simulation is finished, or that one is running.
* Add more unit tests. I'm doing this for free, so I don't want to write more.
* I haven't done enough testing to be comfortable sending this to production.
* The code probably doesn't follow your favourite coding conventions. If I was on a team I'd use the teams conventions.

---

![Screenshot of Interface](/Interface.png)
