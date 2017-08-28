using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageFilter;
using System.Diagnostics;

namespace MessageFilterUI
{
    static class FileHandler
    {
        public static Boolean Import(System.IO.StreamReader streamReader, MainForm form)
        {
            String line;
            while (!streamReader.EndOfStream)
            {
                line = streamReader.ReadLine();
                Debug.WriteLine(line);
                if (!form.AddTag(line)) return false;
            }
            return true;
        }

        public static Boolean Export(System.IO.StreamWriter sw, Filter filter)
        {
            foreach (var tag in filter.Tags)
            {
                Debug.WriteLine(tag);
                sw.WriteLine(tag);
            }
            return true;
        }
    }
}
