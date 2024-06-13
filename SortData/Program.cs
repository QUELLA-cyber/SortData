using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath;

            Console.WriteLine("Введите путь к файлу:");
            filePath = Console.ReadLine();
            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("Путь к файлу не может быть пустым.");
                Console.ReadLine();
                return;
            }

            List<string> validData = new List<string>();
            List<string> invalidData = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    string dataBlock = "";
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            if (!string.IsNullOrEmpty(dataBlock))
                            {
                                validData.Add(dataBlock);
                                dataBlock = "";
                            }
                        }
                        else
                        {
                            dataBlock += line + "\n";
                        }
                    }

                    // Обработка последнего блока, если он не пустой
                    if (!string.IsNullOrEmpty(dataBlock))
                    {
                        validData.Add(dataBlock);
                    }
                }

                Console.WriteLine("Корректные данные:");
                foreach (var data in validData)
                {
                    Console.WriteLine(data);
                }

                SaveData("bad_data.txt", invalidData);

                if (validData.Count == 0)
                {
                    Console.WriteLine("Нет корректных данных для сохранения.");
                }
                else
                {
                    int partCount = 5;
                    int partSize = (int)Math.Ceiling(validData.Count / (double)partCount);
                    for (int i = 0; i < partCount; i++)
                    {
                        int startIndex = i * partSize;
                        int endIndex = Math.Min(startIndex + partSize, validData.Count);
                        if (startIndex < validData.Count)
                        {
                            var partData = validData.GetRange(startIndex, endIndex - startIndex);
                            SaveData($"base_{i + 1}.txt", partData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обработке файла: {ex.Message}");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Обработка завершена.");
            Console.ReadLine();
        }

        static void SaveData(string fileName, List<string> data)
        {
            using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                foreach (string dataBlock in data)
                {
                    sw.WriteLine(dataBlock);
                    sw.WriteLine();
                }
            }
        }
    }
}

