using System;
using System.Data;
using System.IO;
using System.Linq;
using ExcelDataReader;
using System.Diagnostics;
using OfficeOpenXml;



class Program
{
    static void Main(string[] args)
    {

        List<City> cities = new List<City>();
        List<Road> roads = new List<Road>();
        cities = InOutUtils.ReadFile("IP_data_2023.xlsx");
        
        Stopwatch timer = new Stopwatch();
        

        Dictionary<City, bool> visitedC = new Dictionary<City, bool>();
        foreach (City c in cities)
        {
            visitedC[c] = false;
        }

        List<Path> Paths = new List<Path>();
        timer.Start();
        while (timer.Elapsed.TotalSeconds < 10)  //c1 | 1
        {

            Dictionary<City, bool> visited = new Dictionary<City, bool>();  //c2 | 1
            foreach (City c in cities)  //c3 | n
            {
                visited[c] = false; //c4 | n;
            }
            
            int Ratings = 0;  //c5 | 1
            int Timerr = 0;  //c6 | 1
            Random rnd = new Random();  //c7 | 1
            int number = rnd.Next(0, cities.Count);  //c8 | 1
            if (visitedC[cities[number]] == true)  //c9 | 1
            {
                continue; //
            }
            else
            {
                visitedC[cities[number]] = true; //
            }
            //int number = 0;
            City StartingCity = cities[number]; //
            Path startpath = new Path(); //
            FindAllPaths(StartingCity, Paths, Timerr, Ratings, StartingCity, visited, startpath); //
            
        }
        timer.Stop();
        Console.WriteLine(Paths.Count());
        Paths.Sort((p1, p2) => p1.Rating.CompareTo(p2.Rating));
        
        List<Path> distinctPaths = Paths.Distinct(new PathComparer()).ToList();
        Paths = Paths.OrderBy(x => x.Rating).ToList();
        //InOutUtils.PrintPaths(Paths);

        List<Path> Top10 = Utils.BestPaths(distinctPaths);
        InOutUtils.PrintPaths(Top10);

        Console.WriteLine("-----------------------------------------------------------------------------------");


        Console.WriteLine("--------------------------Genetic Paths-------------------------------------");

        int populationSize = 50;
        int numGenerations = 100;

        Population population = new Population(distinctPaths, populationSize, true);

        population.printer();

        Console.WriteLine("--------------------------Genetic Best Path-------------------------------------");

        // Iterate for the desired number of generations
        for (int generation = 0; generation < numGenerations; generation++)
        {
            // Evolve the population
            population = GeneticOptimization.EvolvePopulation(population, roads);
        }

        // Get the best path from the final population
        Path bestPath = population.GetFittest();

        // Output the best path, its rating, and time
        Console.WriteLine("Best Path: " + string.Join(" -> ", bestPath.Roads.Select(city => city.Name)));
        Console.WriteLine("Rating: " + bestPath.Rating);
        Console.WriteLine("Time: " + bestPath.Time);

        void FindAllPathsForced(City StartingCity, List<Path> list, int Timerr, double Ratings, City current, Path path, Stopwatch timer)
        {
            if(timer.Elapsed.TotalSeconds > 10)                                  //c1 | 1 
            {
                return;                                                          //c2 | 1
            }

            Path newPath = new Path(path);                                       //c3 | 1
            int Time = Timerr;                                                   //c4 | 1
            double Rating = Ratings;                                             //c5 | 1
            if(Timerr/3600 > 48)
            {
                return;                                                          //c2 | 1
            }

            newPath.Add(Rating, Time, current);                                  //c6 | 1
            
         

            /*foreach(City c in path.Roads)
            {
                Console.Write(c.Name + "->");
            }*/
            //Console.Write("\n");
            if (current.Name == StartingCity.Name && Time != 0)                 //c7 | 1
            {

                list.Add(newPath);                                              //c8 | 1
                return;                                                         //c2 | 1

            }

            for (int i = 0; i < current.Roads.Count(); i++)                      //c9 | n +1
            {
                //Console.WriteLine(newPath.Repeat(current, current.Roads[i].To));
                if(newPath.Repeat(current, current.Roads[i].To))                 //c10 | n
                {        
                    if(!newPath.Contains(current.Roads[i].To))                   //c11 | n
                    {
                        FindAllPathsForced(StartingCity, list, Time + current.Roads[i].Time, Rating, current.Roads[i].To, newPath, timer);   //T(n - 1) | n
                    }
                    else
                    {
                        FindAllPathsForced(StartingCity, list, Time + current.Roads[i].Time, Rating + current.Roads[i].To.Rating, current.Roads[i].To, newPath, timer);    ////T(n - 1) | n
                    }
                    
                }
                    
            }
            
        }

        void FindAllPaths(City StartingCity, List<Path> list, int Timerr, double Ratings, City current, Dictionary<City, bool> visited, Path path)
        {
            Dictionary<City, bool> dic = new Dictionary<City, bool>(visited); // c1 | 1
            Path newPath = new Path(path); // c2 | 1
            int Time = Timerr; // c3 | 1
            double Rating = Ratings; // c4 | 1

            for (int r = 0; r < current.Roads.Count(); r++) // c5 | n +1
            {
                if (dic[current] == false) // c6 | n
                {
                    dic[current] = true; //c7 | n
                }
                else
                {
                    if (current.Name != StartingCity.Name) //c8 | n
                    {
                        return;   //c9 | n
                    }
                    if (Time / 3600 > 48)   //c10 | n
                    {
                        return;   //c11 | n
                    }

                }             
                
                int Timer = 1000000;   // c12 | n
                City next = current;  // c13 | n
                //Console.WriteLine(Time);
                if (StartingCity.Name == current.Name)  // c14 | n
                {
                    if (r == 0)   // c15 | n
                    {
                        newPath.Add(Rating, Time, current);  // c16 | n
                    }
                }
                else  
                {
                    newPath.Add(Rating, Time, current);   // c17 | n
                }

                if (Time / 3600 > 20)  // c18 | n
                {
                    if (current.Name == StartingCity.Name && Time != 0)  // c19 | n
                    {
                        list.Add(newPath);   // c20 | n
                        return;   // c21 | n
                    }
                    //Console.WriteLine(current.Roads.Count());
                    for (int i = 0; i < current.Roads.Count(); i++) // c22 | n*(n+1)
                    {
                        if (StartingCity.Name == current.Roads[i].To.Name) //c23 | n*n
                        {
                            FindAllPaths(StartingCity, list, Time + current.Roads[i].Time, Rating + current.Roads[i].To.Rating, current.Roads[i].To, dic, newPath); //T(n-1) * n * n
                        }

                        if (Timer > current.Roads[i].Time && (dic[current.Roads[i].To] == false || current.Roads[i].To.Name == StartingCity.Name))  //c24 | n*n
                        {
                            Timer = current.Roads[i].Time;   //c25 | n*n
                            next = current.Roads[i].To;     //c26 | n*n
                        }
                    }
                    //Console.WriteLine(current.Name + " -> " + next.Name);

                    FindAllPaths(StartingCity, list, Time + Timer, Rating + next.Rating, next, dic, newPath);  //T(n-1)*n
                    dic[next] = true;   //c28 | n
                }
                else
                {
                    if (current.Name == StartingCity.Name && Time != 0)  //c29 | n
                    {
                        list.Add(newPath);  //c30 | n
                        return;  //c31 | n
                    }

                    //Console.WriteLine(current.Roads.Count()); 
                    for (int i = 0; i < current.Roads.Count(); i++)  //c32 | n*(n + 1)
                    {
                        if (Timer > current.Roads[i].Time && (dic[current.Roads[i].To] == false || current.Roads[i].To.Name == StartingCity.Name)) //c33 | n
                        {
                            Timer = current.Roads[i].Time;  //c34 | n*n
                            next = current.Roads[i].To; //c35 | n*n
                        }
                    }
                    //Console.WriteLine(current.Name + " -> " + next.Name);

                    FindAllPaths(StartingCity, list, Time + Timer, Rating + next.Rating, next, dic, newPath); //c36 | n
                    dic[next] = true; //c37 | n
                }

            }



        }

    }

    public class PathComparer : IEqualityComparer<Path>
    {
        public bool Equals(Path x, Path y)
        {
            // Compare the Roads property of Path objects for equality
            return x.Roads.SequenceEqual(y.Roads);
        }

        public int GetHashCode(Path obj)
        {
            // Generate a hash code based on the Roads property
            int hash = 17;
            foreach (var road in obj.Roads)
            {
                hash = hash * 23 + road.GetHashCode();
            }
            return hash;
        }
    }

    public static class Utils
    {
        public static List<Path> BestPaths(List<Path> paths)
        {
            int count = 0;
            List<Path> result = new List<Path>();
            for(int i = paths.Count - 1; i > 0; i--)
            {
                if(count == 10)
                {
                    break;
                }
                Path path = paths[i];
                if(path.Time < 48 * 3600)
                {
                    result.Add(path);
                    count++;
                }
            }
            return result;
        }



        public static Path BestPath(List<Path> paths)
        {
            paths.Sort((p1, p2) => p1.Rating.CompareTo(p2.Rating));
            return paths[paths.Count - 1];
        }

        public static int Calculatetime(List<City> cities, List<Road> roads)
        {
            int i = 0;
            for(int y = 0; y < cities.Count - 1; y++)
            {
                for (int z = 0; z < roads.Count; z++)
                {
                    if(cities[i].Name == roads[z].From.Name && cities[i + 1].Name == roads[z].To.Name)
                    {
                        i += roads[z].Time;
                        break;
                    }
                }
            }
            return i;
           
        }
    }
    
    public class GeneticOptimization
    {
        private const int Elitism = 5;
        private const double Mutation = 1.5;
        private const int TournamentSize = 3;
        private static Path SelectViaTournament(Population Population, List<Road> roads)
        {
            Population Tournament = new Population(TournamentSize);

            Random Randomizer = new Random();
            for (int i = 0; i < TournamentSize; i++)
                Tournament.PathAdd(Population.Paths[Randomizer.Next(0, Population.Paths.Count)]);

            return Tournament.GetFittest();
        }


        private static Path CrossoverAndMutate(Path first, Path second, List<Road> roads)
        {
            Random randomizer = new Random();

            // Create a new path object for the offspring
            Path offspring = new Path();

            // Randomly select a crossover point
            int crossoverPoint = randomizer.Next(1, first.Roads.Count - 1);

            // Add cities from the first parent up to the crossover point
            for (int i = 0; i < crossoverPoint; i++)
            {
                City city = first.Roads[i];
                offspring.Roads.Add(city);
                offspring.Rating += city.Rating;
                
            }
            //offspring.Time = Utils.Calculatetime(offspring.Roads, roads);
            // Add cities from the second parent after the crossover point
            for (int i = crossoverPoint; i < second.Roads.Count - 1; i++)
            {
                City city = second.Roads[i];

                // Check if the city is already present in the offspring
                if (!offspring.Roads.Contains(city))
                {
                    offspring.Roads.Add(city);
                    offspring.Rating += city.Rating;
                    //offspring.Time += city.Roads.Find(r => r.To == second.Roads[i + 1]).Time;
                }
            }
            //offspring.Time = Utils.Calculatetime(offspring.Roads, roads);
            // Add the starting city at the end to represent the round trip
            offspring.Roads.Add(offspring.Roads[0]);
            //offspring.Time += offspring.Roads.Last().Roads.Find(r => r.To == offspring.Roads[0]).Time;

            // Perform mutation
            if (randomizer.NextDouble() < Mutation)
            {
                int mutationPoint1 = randomizer.Next(1, offspring.Roads.Count - 1);
                int mutationPoint2 = randomizer.Next(1, offspring.Roads.Count - 1);

                // Swap the cities at the mutation points
                City temp = offspring.Roads[mutationPoint1];
                offspring.Roads[mutationPoint1] = offspring.Roads[mutationPoint2];
                offspring.Roads[mutationPoint2] = temp;

                // Update the time for the mutated path
                offspring.Time = Utils.Calculatetime(offspring.Roads, roads);
            }
            //offspring.Time = Utils.Calculatetime(offspring.Roads, roads);
            return offspring;
        }

        public static Population EvolvePopulation(Population population, List<Road> roads)
        {
            Population newPopulation = new Population(population.PopulationSize);

            // Add the fittest individuals from the previous population (elitism)
            Path bestPath = Utils.BestPath(population.Paths);
            newPopulation.PathAdd(bestPath);

            for (int i = 1; i < population.PopulationSize; i++)
            {
                // Select parents via tournament selection
                Path parent1 = SelectViaTournament(population, roads);
                Path parent2 = SelectViaTournament(population, roads);

                // Perform crossover and mutation to create offspring
                Path offspring = CrossoverAndMutate(parent1, parent2, roads);

                // Add the offspring to the new population
                newPopulation.PathAdd(offspring);
            }

            newPopulation.Paths = newPopulation.Paths.OrderBy(path => path.Rating).ToList();

            return newPopulation;

        }

    }
    
    public class Population
    {
        public int PopulationSize;
        public List<Path> Paths;
        public Population(List<Path> Paths, int PopulationSize, bool Initialize)
        {
            this.Paths = new List<Path>();
            this.PopulationSize = PopulationSize;
            for(int i = 0; i < PopulationSize; i++)
            {


                Random rnd = new Random();
                int number = rnd.Next(0, Paths.Count);
                if(Paths[number].Time / 3600 < 48)
                {
                    this.Paths.Add(Paths[number]);

                }
                else
                {
                    i--;
                    continue;
                }
                

            }                
                     
        }

        public Population(int PopulationSize)
        {
            this.Paths = new List<Path>();
            this.PopulationSize = PopulationSize;
        }

        public void PathAdd(Path path)
        {
            this.Paths.Add((Path)path);
        }

        public Path GetFittest()
        {
            //Console.WriteLine(Utils.BestPath(Paths).Time+ " "+ Utils.BestPath(Paths).Rating);
            return Utils.BestPath(Paths);
        }

        public void printer()
        {
            InOutUtils.PrintPaths(this.Paths);
        }

       
    }

    public class Path
    {
        public double Rating { get; set; }

        public int Time { get; set; }

        public List<City> Roads { get; set; }

        public Path()
        {
            this.Roads = new List<City>();
            this.Rating = 0;
            this.Time = 0;
        }

        public Path(Path p)
        {

            this.Roads = new List<City>(p.Roads);
            this.Rating = p.Rating;
            this.Time = p.Time;
        }

        public bool Contains(City c)
        {
            for (int i = 0; i < Roads.Count(); i++)
            {
                if(c.Name == Roads[i].Name)
                {
                    return true;

                }
            }
            return false;
        }

        public void Add(double rating, int time, City city)
        {
            this.Rating = rating;
            this.Time = time;
            this.Roads.Add(city);
        }

        public bool Repeat(City current, City next)
        {
            for (int i = 0; i < Roads.Count(); i++)
            {
                if (current.Name == Roads[i].Name)
                {
                    if (i + 1 < Roads.Count() && next.Name == Roads[i + 1].Name)
                    {
                        return false;
                    }
                }
            }
            return true;
        }





    }

    public class City
    {
        public string Name { get; set; }
        public double Rating { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public List<Road> Roads { get; set; }

        public City(string name, double rating, double x, double y)
        {
            Name = name;
            Rating = rating;
            X = x;
            Y = y;
            Roads = new List<Road>();
        }

        public City()
        {
            Name = "";
            Rating = 0;
            X = 0;
            Y = 0;
            Roads = new List<Road>();
        }

        public void AddRoad(Road r)
        {
            this.Roads.Add((Road)r);
        }
    }

    public class Road
    {
        public City From { get; set; }
        public City To { get; set; }
        public int Time { get; set; }
        public double Cost { get; set; }

        public Road(City from, City to, int time, double cost)
        {
            From = from;
            To = to;
            Time = time;
            Cost = cost;
        }


    }

    public static class InOutUtils
    {

        
            public static void PrintPaths(List<Path> paths)
            {

                foreach (Path p in paths)
                {

                    foreach (City y in p.Roads)
                    {


                        Console.Write(y.Name + " -> ");


                    }
                    Console.Write(p.Time + " " + p.Rating);
                    Console.Write("\n\n");


                }
            }

            public static List<City> ReadFile(string FilePath)
            {
                List<City> list = new List<City>();
                List<Road> Roads = new List<Road>();
                using (ExcelPackage package = new ExcelPackage(new FileInfo(FilePath)))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    ExcelRange cityrange = worksheet.Cells["H6:K103"]; // Example range: cells A1 to B5

                    ExcelRange roadrange = worksheet.Cells["M6:P2275"];





                    // Read the values from the cells into a two-dimensional array
                    object[,] cities = cityrange.Value as object[,];

                    string CityName = "";
                    double Rating = 0;
                    double x = 0;
                    double y = 0;

                    for (int row = 0; row < cities.GetLength(0); row++)
                    {

                        for (int col = 0; col < cities.GetLength(1); col++)
                        {
                            // Access the value at the current row and column
                            if (col == 0)
                            {
                                CityName = cities[row, col].ToString();
                            }
                            if (col == 1)
                            {
                                Rating = (double)cities[row, col];
                            }
                            if (col == 2)
                            {
                                x = (double)cities[row, col];
                            }
                            if (col == 3)
                            {
                                y = (double)cities[row, col];
                            }


                            // Perform further processing with the value as needed
                            // You can cast the value to the appropriate data type based on your Excel data
                        }
                        list.Add(new City(CityName, Rating, x, y));
                    }

                    object[,] roads = roadrange.Value as object[,];

                    City From = new City();
                    City To = new City();
                    int Time = 0;
                    double price = 0;

                    for (int row = 0; row < roads.GetLength(0); row++)
                    {
                        for (int col = 0; col < roads.GetLength(1); col++)
                        {

                            if (col == 0)
                            {
                                string f = roads[row, col].ToString();
                                foreach (City c in list)
                                {
                                    if (f == c.Name)
                                    {
                                        From = c;
                                        break;
                                    }
                                }
                            }
                            if (col == 1)
                            {
                                string g = roads[row, col].ToString();
                                foreach (City c in list)
                                {
                                    if (g == c.Name)
                                    {
                                        To = c;
                                        break;
                                    }
                                }
                            }
                            if (col == 2)
                            {
                                Time = Convert.ToInt32(roads[row, col]);
                            }
                            if (col == 3)
                            {
                                price = (double)roads[row, col];
                            }
                            // Access the value at the current row and column


                            // Perform further processing with the value as needed
                            // You can cast the value to the appropriate data type based on your Excel data
                        }
                        foreach (City c in list)
                        {
                            if (From.Name == c.Name)
                            {
                                c.AddRoad(new Road(From, To, Time, price));
                                break;
                            }
                        }
                    }

                }
                return list;
            }


        

    }
}

