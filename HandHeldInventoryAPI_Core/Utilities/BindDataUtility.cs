using System.Data;

namespace HandHeldInventoryAPI_Core.Utilities
{
    public class BindDataUtility
    {
        public T BindDatatableToObject<T>(DataTable dt)
        {
            DataRow dr = dt.Rows[0];

            // Get all columns' name
            List<string> columns = new List<string>();
            foreach (DataColumn dc in dt.Columns)
            {
                columns.Add(dc.ColumnName.ToString().ToUpper());
            }

            // Create object
            var ob = Activator.CreateInstance<T>();

            // Get all fields
            var fields = typeof(T).GetFields();
            foreach (var fieldInfo in fields)
            {
                if (columns.Contains(fieldInfo.Name))
                {
                    // Fill the data into the field
                    fieldInfo.SetValue(ob, dr[fieldInfo.Name]);
                }
            }

            // Get all properties
            var properties = typeof(T).GetProperties();
            foreach (var propertyInfo in properties)
            {
                if (columns.Contains(propertyInfo.Name.ToUpper()))
                {
                    // Fill the data into the property
                    propertyInfo.SetValue(ob, dr[propertyInfo.Name]);
                }
            }
            return ob;
        }
        public List<T> BindDatatableToList<T>(DataTable dt)
        {
            List<string> columns = new List<string>();
            foreach (DataColumn dc in dt.Columns)
            {
                columns.Add(dc.ColumnName.ToString().ToUpper());
            }

            var fields = typeof(T).GetFields();
            var properties = typeof(T).GetProperties();

            List<T> lst = new List<T>();

            foreach (DataRow dr in dt.Rows)
            {
                var ob = Activator.CreateInstance<T>();

                foreach (var fieldInfo in fields)
                {
                    if (columns.Contains(fieldInfo.Name.ToUpper()))
                    {
                        fieldInfo.SetValue(ob, dr[fieldInfo.Name]);
                    }
                }

                foreach (var propertyInfo in properties)
                {
                    if (columns.Contains(propertyInfo.Name.ToUpper()))
                    {
                        propertyInfo.SetValue(ob, dr[propertyInfo.Name]);
                    }
                }

                lst.Add(ob);
            }

            return lst;
        }
        public int ExtractInt(object data)
        {
            if (data.GetType() == typeof(int))
            {
                return (int)data;
            }
            else
            {
                int i = 0;
                int.TryParse(data + "", out i);
                return i;
            }
        }
        /*
         * The DataType of "Level" in the DataColumn is String, but it is Int in Class of Foo. Therefore, we need to take extra steps to verify and convert the data.
         * Below is one of the possible way to verify and convert the data:
         * In above example, we'll change this line:
            
            C#
            fieldInfo.SetValue(ob, dr[fieldInfo.Name]);
            to this:

            C#
            if (fieldInfo.FieldType == typeof(int))
            {
                int i = ExtractInt(dr[fieldInfo.Name]);
                fieldInfo.SetValue(ob, i);
            }
            else
            {
                fieldInfo.SetValue(ob, dr[fieldInfo.Name]);
            }
         */
    }
}
