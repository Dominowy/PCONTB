using System.Text.Json;
using System.Text.Json.Serialization;

namespace PCONTB.Panel.Application.Common.Extensions.Helpers.Enums
{
    public class LowercaseEnumConverter : JsonStringEnumConverter
    {
        public LowercaseEnumConverter() : base(new LowercaseNamingPolicy()) { }

        private class LowercaseNamingPolicy : JsonNamingPolicy
        {
            public override string ConvertName(string name) => name.ToLower();
        }
    }
}
