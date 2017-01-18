using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wox.Plugin.BrowserBookmark
{
    class IEBookmarks
    {
        public static List<Bookmark> GetBookmarks()
        {
            List<Bookmark> bookmarks = new List<Bookmark>();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Favorites);
            foreach (var enumerateFile in Directory.GetFiles(path, "*", SearchOption.AllDirectories))
            {
                Bookmark bookmark = GetBookmark(enumerateFile);
                if (bookmark != null)
                {
                    bookmarks.Add(bookmark);
                }
            }

            return bookmarks;
        }

        private static Bookmark GetBookmark(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            const string keyName = "URL=";
            foreach (var readLine in File.ReadLines(path))
            {
                if (readLine.StartsWith(keyName))
                {
                    string url = readLine.Substring(keyName.Length);
                    if (string.IsNullOrEmpty(url))
                    {
                        continue;
                    }

                    return new Bookmark()
                    {
                        Name = fileInfo.Name,
                        Url = readLine.Substring(keyName.Length),
                    };
                }
            }

            return null;
        }

    }
}
