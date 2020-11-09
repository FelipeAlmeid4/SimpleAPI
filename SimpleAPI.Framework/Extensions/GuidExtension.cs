using System;

namespace SimpleAPI.Framework.Extensions
{
    public static class GuidExtension
    {
        public static bool IsNullOrEmpty(this Guid guid)
        {
            return guid == null || guid == Guid.Empty;
        }
    }
}
