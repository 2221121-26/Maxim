using System.Text;

namespace c__z7_webAPI
{
    public static class StringProcessing
    {
        public static String WorkWithString(string stroka, int choice)
        {
            var invalidChars = stroka.Where(c => !char.IsLower(c));
            bool isValid = true;
            var builder = new StringBuilder(256);
            foreach (char ch in stroka)
            {
                if (ch < 'a' || ch > 'z')
                {
                    builder.Append(ch);
                    isValid = false;
                }
            }

            if (isValid)
            {
                Dictionary<char, int> freq = new Dictionary<char, int>();

                if (stroka.Length % 2 == 0)
                {
                    stroka = perevorot(stroka[..(stroka.Length / 2)]) + perevorot(stroka.Substring(stroka.Length / 2));
                }
                else
                {
                    stroka = perevorot(stroka) + stroka;
                }
                builder.AppendLine(stroka);

                foreach (char ch in stroka)
                {
                    if (freq.ContainsKey(ch))
                        freq[ch]++;
                    else
                        freq[ch] = 1;
                }
                foreach (KeyValuePair<char, int> kvp in freq)
                {
                    builder.AppendLine($"{kvp.Key} - {kvp.Value}");
                }

                char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'y' };
                int firstIndex = stroka.IndexOfAny(vowels);
                int lastIndex = stroka.LastIndexOfAny(vowels);
                // Проверка существования подстроки
                if (firstIndex == -1 || lastIndex == -1)
                {
                    builder.AppendLine("Подстрока, которая начинается и заканчивается на гласную, не найдена.");
                }
                else
                {
                    string substring = stroka.Substring(firstIndex, lastIndex - firstIndex + 1);
                    builder.AppendLine(substring);
                }


                // Выбор алгоритма сортировки
                switch (choice)
                {
                    case 1:
                        string sortedString = QuickSort.SortString(stroka);
                        builder.AppendLine(sortedString);
                        break;

                    case 2:
                        BinaryTree tree = new BinaryTree();
                        string bb = stroka;
                        foreach (char bukva in bb)
                        {
                            tree.Insert(bukva);
                        }
                        tree.PrintInOrder();
                        builder.AppendLine();
                        break;

                    default:
                        builder.AppendLine("Неверный выбор алгоритма.");
                        break;
                }

                // API 
                builder.AppendLine(API_Async(stroka).Result.ToString());
            }

            static string perevorot(string message)
            {
                string hc = "";
                foreach (var ch in message)
                {
                    hc = ch + hc;
                }
                return hc;
            }
            return builder.ToString();
        }

        static async Task<string> API_Async(string stroka)
        {
            string url = "http://www.randomnumberapi.com/api/v1.0/random?min=0&max=" + stroka.Length.ToString();

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(url); // Send a GET request to the API

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response body as a string
                        var responseBody = await response.Content.ReadAsStringAsync();
                        responseBody = responseBody.TrimStart('[').TrimEnd(']', '\n');
                        int randomNumber = int.Parse(responseBody);

                        return stroka.Remove(randomNumber, 1).ToString();
                    }
                    else
                    {
                        // if error
                        Random random = new Random();
                        int randomNumber = random.Next(0, stroka.Length);
                        return stroka.Remove(randomNumber, 1).ToString();
                    }
                }
                catch (Exception ex) { return (ex.Message).ToString(); }
            }
        }
    }

    // Quick Sort
    public class QuickSort
    {
        public static string SortString(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length <= 1)
            {
                return input;
            }

            char[] charArray = input.ToCharArray();
            Sort(charArray, 0, charArray.Length - 1);

            return new string(charArray);
        }

        private static void Sort(char[] array, int low, int high)
        {
            if (low < high)
            {
                int partitionIndex = Partition(array, low, high);

                Sort(array, low, partitionIndex - 1);
                Sort(array, partitionIndex + 1, high);
            }
        }

        private static int Partition(char[] array, int low, int high)
        {
            char pivot = array[high];
            int i = low;

            for (int j = low; j < high; j++)
            {
                if (array[j] < pivot)
                {
                    char temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                }
            }

            char temp1 = array[i];
            array[i] = array[high];
            array[high] = temp1;

            return i;
        }
    }


    // Tree sort
    public class Node
    {
        public char Value;
        public Node Left;
        public Node Right;

        public Node(char value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    public class BinaryTree
    {
        private Node root;

        private Node InsertRec(Node root, char value)
        {
            if (root == null)
            {
                root = new Node(value);
                return root;
            }

            if (value < root.Value)
            {
                root.Left = InsertRec(root.Left, value);
            }
            else if (value >= root.Value)
            {
                root.Right = InsertRec(root.Right, value);
            }

            return root;
        }

        public void Insert(char value)
        {
            root = InsertRec(root, value);
        }

        public void PrintInOrder()
        {
            PrintInOrderRec(root);
        }

        private void PrintInOrderRec(Node root)
        {
            if (root != null)
            {
                PrintInOrderRec(root.Left);
                Console.Write($"{root.Value}");
                PrintInOrderRec(root.Right);
            }
        }
    }
}
