namespace Joe.Common.DataAccess
{
    public class JoeSqlParameterCollection
    {
        internal List<JoeSqlParameter> Items { get; set; } = new List<JoeSqlParameter>();

        public void Clear()
        {
            Items.Clear();
        }

        public void Add(string name, object value)
        {
            Items.Add(new JoeSqlParameter
            {
                Name = name,
                Value = value
            });
        }
    }
}
