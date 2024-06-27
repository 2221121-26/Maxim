using NUnit.Framework;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Microsoft.AspNetCore.Mvc;
using c__z9_webAPI;

namespace TestProject1
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            string stroka = "abcdef";
            string expectedString = "cbafed"; // Ожидаемый результат

            // Act
            if (stroka.Length % 2 == 0)
            {
                stroka = perevorot(stroka[..(stroka.Length / 2)]) + perevorot(stroka.Substring(stroka.Length / 2));
            }
            else
            {
                stroka = perevorot(stroka) + stroka;
            }

            static string perevorot(string originalString)
            {
                string reversedString = new string(originalString.Reverse().ToArray());
                return reversedString;

            }

            // Assert
            Assert.AreEqual(expectedString, stroka);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            string stroka = "abcdef";
            bool isPass = true;
            bool expectedString = true; // Ожидаемый результат

            // Act
            foreach (char ch in stroka)
            {
                if (ch < 'a' || ch > 'z')
                {
                    isPass = false;
                }
            }

            // Assert
            Assert.AreEqual(expectedString, isPass);
        }

        [Test]
        public void Test3()
        {
            // Arrange 
            Dictionary<char, int> freq = new Dictionary<char, int>();
            string stroka = "cbafed";
            JArray expectedJArray = new JArray("c - 1", "b - 1", "a - 1", "f - 1", "e - 1", "d - 1");

            // Act 
            foreach (char ch in stroka)
            {
                if (freq.ContainsKey(ch))
                    freq[ch]++;
                else
                    freq[ch] = 1;
            }
            JArray json_array = new JArray();
            foreach (KeyValuePair<char, int> kvp in freq)
            {
                json_array.Add($"{kvp.Key} - {kvp.Value}");
            }
            // Assert
            Assert.AreEqual(expectedJArray, json_array);
        }

        [Test]
        public void Test4()
        {
            // Arrange
            string stroka = "cbafed";
            JObject expectedJObject = new JObject { { "vowels_substring", "afe" } }; // ожидаемый результат
            JObject keyValuePairs = new JObject();

            char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'y' };
            int firstIndex = stroka.IndexOfAny(vowels);
            int lastIndex = stroka.LastIndexOfAny(vowels);

            // Act 
            if (firstIndex == -1 || lastIndex == -1)
            {
                keyValuePairs.Add("vowels_substring", "Подстрока, которая начинается и заканчивается на гласную, не найдена");
            }
            else
            {
                string substring = stroka.Substring(firstIndex, lastIndex - firstIndex + 1);
                keyValuePairs.Add("vowels_substring", substring);
            }

            // Assert
            Assert.AreEqual(expectedJObject, keyValuePairs);
        }

        [Test]
        public void Test5_QuickSort() // сортировки
        {
            // Arrange
            string stroka = "cbafed";
            int choice = 1;
            JObject keyValuePairs = new JObject();
            JObject expectedJObject = new JObject { { "sorted_string", "abcdef" } }; // ожидаемый результат


            // Act
            string sortedString = QuickSort.SortString(stroka);
            keyValuePairs.Add("sorted_string", sortedString);


            // Assert
            Assert.AreEqual(expectedJObject, keyValuePairs);
        }

        [Test]
        public void Test5_TreeSort()
        {
            // Arrange
            string stroka = "cbafed";
            int choice = 1;
            JObject keyValuePairs = new JObject();
            JObject expectedJObject = new JObject { { "sorted_string", "abcdef" } }; // ожидаемый результат


            // Act
            BinaryTree tree = new BinaryTree();
            string bb = stroka;
            foreach (char bukva in bb)
            {
                tree.Insert(bukva);
            }
            keyValuePairs.Add("sorted_string", tree.PrintInOrder().ToString());


            // Assert
            Assert.AreEqual(expectedJObject, keyValuePairs);
        }
    }
}