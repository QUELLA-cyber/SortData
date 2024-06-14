using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SortData
{
    public static class Validator
    {
        public static bool IsValid(Object Oobject)
        {
            if (string.IsNullOrEmpty(Oobject.Title) || string.IsNullOrEmpty(Oobject.Connect))
            {
                return false;
            }

            if (string.IsNullOrEmpty(Oobject.ID) || !IsValidID(Oobject.ID))
            {
                return false;
            }

            if (Oobject.Connect.StartsWith("File="))
            {
                string filePath = Oobject.Connect.Substring(5).Trim('"');
                return IsValidFilePath(filePath);
            }

            else if (Oobject.Connect.StartsWith("Srvr="))
            {
                string[] parts = Oobject.Connect.Substring(5).Split(';');
                string hostPart = parts.FirstOrDefault(p => p.StartsWith("Srvr="));
                string refPart = parts.FirstOrDefault(p => p.StartsWith("Ref="));
                return !string.IsNullOrEmpty(hostPart) && !string.IsNullOrEmpty(refPart);
            }

            return false;
        }

        private static bool IsValidFilePath(string path)
        {
            string pattern = @"^(?:[a-zA-Z]:|\\\\[a-zA-Z0-9_.$-]+\\[a-zA-Z0-9_.$-]+)\\(?:[a-zA-Z0-9(){}\[\]!@#%&+=._-]+\\)*[a-zA-Z0-9(){}\[\]!@#%&+=._-]*$";
            return Regex.IsMatch(path, pattern);
        }

        private static bool IsValidID(string id)
        {
            return Guid.TryParse(id, out _);
        }
    }
}
