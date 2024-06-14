using System;
using System.Collections.Generic;


namespace SortData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Вы используете SortData <testdata.txt>");
                Environment.Exit(-1);
            }

            string inputFilepath = args[0];

            try
            {
                List<Object> databaseObject = FileProcessing.ReadObject(inputFilepath);
                List<Object> validObjects = new List<Object>();
                List<Object> invalidObjects = new List<Object>();

                foreach (var dbObject in databaseObject)
                {
                    if (Validator.IsValid(dbObject))
                    {
                        validObjects.Add(dbObject);
                    }
                    else
                    {
                        invalidObjects.Add(dbObject);
                    }
                }

                FileProcessing.SaveInvalidData(invalidObjects);
                FileProcessing.SaveValidData(validObjects, 5);

                Console.WriteLine("Обработка завершена успешно.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                Environment.Exit(-1);
            }
            Console.WriteLine("Нажмите кнопку, чтобы выйти...");
            Console.ReadLine();
        }
    }
}

