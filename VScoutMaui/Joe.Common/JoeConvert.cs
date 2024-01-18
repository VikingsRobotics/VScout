namespace Joe.Common
{
    public static class JoeConvert
    {
        public static decimal ToDecimal(object value)
        {
            if (value == null)
            {
                return 0;
            }

            if (value == DBNull.Value)
            {
                return 0;
            }

            return Convert.ToDecimal(value);
        }

        public static int ToInt32(object value)
        {
            if (value == null)
            {
                return 0;
            }

            if (value == DBNull.Value)
            {
                return 0;
            }

            return Convert.ToInt32(value);
        }

        public static bool ToBoolean(object value)
        {
            if (value == null)
            {
                return false;
            }

            if (value == DBNull.Value)
            {
                return false;
            }

            return Convert.ToBoolean(value);
        }

        public static string ToString(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (value == DBNull.Value)
            {
                return string.Empty;
            }

            return value.ToString() ?? string.Empty;
        }
    }
}