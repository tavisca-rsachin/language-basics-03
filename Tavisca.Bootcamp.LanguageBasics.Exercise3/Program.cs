using System;
using System.Collections.Generic;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            int length = protein.Length;
            int[] cal = new int[length];

            for(int i=0;i<length;i++)
                cal[i] = (protein[i] * 5) + (carbs[i] * 5) + (fat[i] * 9);
            
            int dp_length = dietPlans.Length;
            int[] res = new int[dp_length];

            for(int i=0;i<dp_length;i++)
            {
                List<int> list = new List<int>();
                for(int j=0;j<length;j++)
                    list.Add(j);

                for(int j=0;j<dietPlans[i].Length;j++)
                {
                    if(dietPlans[i][j] == 't')
                        list = MinValueIndex(cal, list);
                    else if(dietPlans[i][j] == 'p')
                        list = MinValueIndex(protein, list);
                    else if(dietPlans[i][j] == 'c')
                        list = MinValueIndex(carbs, list);
                    else if(dietPlans[i][j] == 'f')
                        list = MinValueIndex(fat, list);
                    else if(dietPlans[i][j] == 'T')
                        list = MaxValueIndex(cal, list);
                    else if(dietPlans[i][j] == 'P')
                        list = MaxValueIndex(protein, list);
                    else if(dietPlans[i][j] == 'C')
                        list = MaxValueIndex(carbs, list);
                    else if(dietPlans[i][j] == 'F')
                        list = MaxValueIndex(fat, list);
                }

                res[i] = list.Min();
            }

            
            return res;

        }

        private static List<int> MinValueIndex(int[] arr, List<int> list)
        {
            int min_value = int.MaxValue;

            foreach(int x in list)
            {
                if(min_value > arr[x])
                    min_value = arr[x];
            }
            
            List<int> ret = new List<int>();
            foreach(int x in list)
            {
                if(min_value == arr[x])
                    ret.Add(x);
            }
            return ret;
        }

        private static List<int> MaxValueIndex(int[] arr, List<int> list)
        {
            int max_value = int.MinValue;

            foreach(int x in list)
            {
                if(max_value < arr[x])
                    max_value = arr[x];
            }
            
            List<int> ret = new List<int>();
            foreach(int x in list)
            {
                if(max_value == arr[x])
                    ret.Add(x);
            }
            return ret;
        }
    }
}
