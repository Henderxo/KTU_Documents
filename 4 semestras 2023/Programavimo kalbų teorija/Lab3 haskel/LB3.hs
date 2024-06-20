import Control.Monad
import System.IO

-- | A function to count the number of swaps needed to sort a list.
countSwaps :: Ord a => [a] -> Int
countSwaps xs = loop xs (length xs) 0
  where
    loop xs n count
      | n == 0    = count
      | otherwise = loop swapped (n-1) newCount
      where
        (swapped, newCount) = bubble xs count
    bubble (x1:x2:xs) count
      | x1 > x2   = let (ys, c) = bubble (x1:xs) (count+1)
                    in (x2:ys, c)
      | otherwise = let (ys, c) = bubble (x2:xs) count
                    in (x1:ys, c)
    bubble xs count = (xs, count)


-- | A function to parse input in the format specified in the problem statement.
parseInput :: String -> [(Int, [Int])]
parseInput input = go (tail $ lines input)
  where
    go [] = []
    go (n:l:xs) = (read n, map read $ words l) : go xs




-- | A function to solve the Train Swapping problem for a single test case.
solveTestCase :: (Int, [Int]) -> String
solveTestCase (n, xs) = "Optimal train swapping takes " ++ show (countSwaps xs) ++ " swaps."

-- | A function to solve the Train Swapping problem for all test cases.
solveAll :: [(Int, [Int])] -> String
solveAll = unlines . map solveTestCase

-- | The main function that reads input from a file and solves the problem.
main :: IO ()
main = do
  input <- readFile "Duom.txt"
  let testCases = parseInput input
      output = solveAll testCases
  writeFile "output.txt" output