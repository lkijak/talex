using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LukaszKijak.Models
{
    public class ContentType
    {
        public string GetContentType(string fileName)
        {
            var type = GetMimeTypes();
            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            return type[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            Dictionary<string, string> type = new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".cs", "text/cs" },
                {".json", "text/json" },
                {".cshtml", "text/cshtml" },
                {".js", "text/js" },
                {".css", "text/css" },
                {".md", "text/md" },
                {".sln", "application/sln" },
                {".csproj", "application/csproj" },
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"}
            };

            return type;
        }
    }
}
