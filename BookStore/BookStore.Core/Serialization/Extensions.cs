using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace BookStore.Core.Serialization
{
    internal static class JsonSerializerSettingsExtensions
    {
        [Conditional("DEBUG")]
        public static void EnsureFormatting(this JsonSerializerSettings settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            settings.Formatting = Formatting.Indented;
        }
    }
}
