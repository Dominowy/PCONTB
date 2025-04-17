using PCONTB.Panel.Application.Common.Extensions.Helpers;

namespace PCONTB.Panel.Application.Common.Extension.Helpers
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
                    Value = e.ToString()
                })
                .ToList();
        }
    }
}
