namespace PCONTB.Panel.Application.Common.Extensions.Helpers.Enums
{
    public static class EnumHelper
    {
        public static List<EnumItem> EnumToList<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(e => new EnumItem
                {
                    Id = Convert.ToInt32(e),
                    Value = ToCamelCase(e.ToString())
                })
                .ToList();
        }

        private static string ToCamelCase(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length < 2)
                return input?.ToLower();

            return char.ToLowerInvariant(input[0]) + input.Substring(1);
        }

    }
}
