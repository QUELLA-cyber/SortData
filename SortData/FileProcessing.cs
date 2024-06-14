using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SortData
{
    public static class FileProcessing
    {
        public static List<Object> ReadObject (string filepath)
        {
            List<Object> databaseobject = new List<Object> ();
            Object currentObject = null;
            foreach (var line in File.ReadLines (filepath))
            {
                Console.WriteLine ($"Проверка строки: '{line}'");
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (currentObject != null)
                    {
                        databaseobject.Add (currentObject);
                        currentObject = null;
                    }
                }
                else if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    currentObject = new Object
                    {
                        Title = line.Trim('[',']')
                    };
                }
                else if (line.StartsWith("Connect="))
                {
                    if (currentObject != null)
                    {
                        currentObject.Connect = line.Substring(8);
                    }
                }
                else
                {
                    throw new FormatException("Недопустимый формат данных");
                }
            }
            if (currentObject != null)
            {
                databaseobject.Add (currentObject);
            }
            return databaseobject;
        }
        public static void SaveInvalidData(List<Object> invalidObject)
        {
            using (StreamWriter writer = new StreamWriter("bad_data.txt"))
            {
                foreach (var obj in invalidObject)
                {
                    writer.WriteLine($"[{obj.Title}]");
                    writer.WriteLine($"Connect={obj.Connect}");
                    writer.WriteLine();
                }
            }
        }
        public static void SaveValidData(List<Object> validObject, int parts)
        {
            int objectPart = (int)Math.Ceiling(validObject.Count / (double)parts);
            for (int i = 0; i <parts; i++)
            {
                string fileName = $"base_{i + 1}.txt";
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    var objectsToWrite = validObject.Skip(i * objectPart).Take(objectPart);
                    foreach (var obj in objectsToWrite)
                    {
                        writer.WriteLine($"[{obj.Title}]");
                        writer.WriteLine($"Connect={obj.Connect}");
                        writer.WriteLine();
                    }
                }
            }
        }
    }
}
