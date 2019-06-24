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
            int[] calore = new int[length];

            for(int i=0;i<length;i++)
                calore[i] = (protein[i] * 5) + (carbs[i] * 5) + (fat[i] * 9);
            
            int dietPlansLength = dietPlans.Length;
            int[] result = new int[dietPlansLength];

            for(int i=0;i<dietPlansLength;i++)
            {
                List<int> listOfIndexes = new List<int>();
                for(int index=0;index<length;index++)
                    listOfIndexes.Add(index);

                for(int j=0;j<dietPlans[i].Length;j++)
                {
                    if(dietPlans[i][j] == 't')
                        listOfIndexes = MinValueIndexes(calore, listOfIndexes);
                    else if(dietPlans[i][j] == 'p')
                        listOfIndexes = MinValueIndexes(protein, listOfIndexes);
                    else if(dietPlans[i][j] == 'c')
                        listOfIndexes = MinValueIndexes(carbs, listOfIndexes);
                    else if(dietPlans[i][j] == 'f')
                        listOfIndexes = MinValueIndexes(fat, listOfIndexes);
                    else if(dietPlans[i][j] == 'T')
                        listOfIndexes = MaxValueIndexes(calore, listOfIndexes);
                    else if(dietPlans[i][j] == 'P')
                        listOfIndexes = MaxValueIndexes(protein, listOfIndexes);
                    else if(dietPlans[i][j] == 'C')
                        listOfIndexes = MaxValueIndexes(carbs, listOfIndexes);
                    else if(dietPlans[i][j] == 'F')
                        listOfIndexes = MaxValueIndexes(fat, listOfIndexes);
                }

                result[i] = listOfIndexes.Min();
            }

            return result;
        }

        private static List<int> MinValueIndexes(int[] caloreOrProteinOrCarbsOrFat, List<int> listOfIndexes)
        {
            int minValue = int.MaxValue;

            foreach(int index in listOfIndexes)
            {
                if(minValue > caloreOrProteinOrCarbsOrFat[index])
                    minValue = caloreOrProteinOrCarbsOrFat[index];
            }

            List<int> returnListofIndexes = new List<int>();
            foreach(int index in listOfIndexes)
            {
                if(minValue == caloreOrProteinOrCarbsOrFat[index])
                    returnListofIndexes.Add(index);
            }
            return returnListofIndexes;
        }

        private static List<int> MaxValueIndexes(int[] caloreOrProteinOrCarbsOrFat, List<int> listOfIndexes)
        {
            int maxValue = int.MinValue;

            foreach(int index in listOfIndexes)
            {
                if(maxValue < caloreOrProteinOrCarbsOrFat[index])
                    maxValue = caloreOrProteinOrCarbsOrFat[index];
            }
            
            List<int> returnListofIndexes = new List<int>();
            foreach(int index in listOfIndexes)
            {
                if(maxValue == caloreOrProteinOrCarbsOrFat[index])
                    returnListofIndexes.Add(index);
            }
            return returnListofIndexes;
        }
    }
}
