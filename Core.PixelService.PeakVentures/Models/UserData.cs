using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PixelService.PeakVentures.Models
{
    public class UserData
    {
        #region constructors
        public UserData(string ipAddress, string? referrer, string? userAgent)
        {
            Referrer = referrer ?? "null";
            UserAgent = userAgent ?? "null";
            IpAddress = ipAddress;
        }
        #endregion

        public string? Referrer { get; set; }
        public string? UserAgent { get; set; }
        public string IpAddress { get; set; }

        public override string ToString()
        {
            return $"{DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture)}|{Referrer}|{UserAgent}|{IpAddress}";
        }

        public static UserData? FromRequest(HttpRequest request)
        {
            var referrer = request.Headers.TryGetValue("Referrer", out var referrerValues) ? referrerValues.FirstOrDefault() : null;
            var userAgent = request.Headers.TryGetValue("User-Agent", out var userAgentValues) ? userAgentValues.FirstOrDefault() : null;
            var ipAddress = request.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();

            if (String.IsNullOrEmpty(ipAddress))
                return null;

            return new UserData(ipAddress, referrer, userAgent);
        }
    }
}
