using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageFilter;
using System.Diagnostics;

namespace MessageFilterUI
{
    static class FileHandler
    {
        public static ISet<String> Import(String fileName)
        {
            ISet<String> tags = new HashSet<String>();
            StreamReader streamReader = new StreamReader(fileName);
            while (!streamReader.EndOfStream)
            {
                String tag = streamReader.ReadLine();
                tags.Add(tag);
            }
            streamReader.Close();
            return tags;
        }

        public static void Export(String fileName, ISet<String> tags)
        {
            StreamWriter streamWriter = new StreamWriter(fileName);
            foreach (var tag in tags)
            {
                streamWriter.WriteLine(tag);
            }
            streamWriter.Close();
        }
    }
}
