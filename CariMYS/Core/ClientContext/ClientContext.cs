using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Translation;

namespace Core.Application.ClientContext
{
    public class ClientContext
    {
        public string UserId { get; set; }
        public string IpAddress { get; }
        public Language Language { get; }
        public TimeSpan UtcOffset { get; }

        public ClientContext(string userId, Language language, TimeSpan utcOffset, string? ipAddress)
        {
            IpAddress = ipAddress ?? String.Empty;
            UtcOffset = utcOffset;
            UserId = userId;
            Language = language;
        }
    }
}
