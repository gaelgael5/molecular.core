using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaApi.Helpers
{
    public static class PathBuilderHelper
    {

        public static string GetRelativePath(string path1 , string path2, bool cutExtension = true)
        {

            Uri _path1 = new Uri(path1.Replace("/", "\\"));
            Uri _path2 = new Uri(path2.Replace("/", "\\"));
            Uri diff = _path1.MakeRelativeUri(_path2);
            string relPath = diff.OriginalString;

            if (!relPath.StartsWith("."))
                relPath = $"./{relPath}";

            if (cutExtension)
            {
                int extensionLength = System.IO.Path.GetExtension(path2.Replace("\\", "/")).Length;
                relPath = relPath.Substring(0, relPath.Length - extensionLength);
            }

            return relPath;
        }


    }
}
