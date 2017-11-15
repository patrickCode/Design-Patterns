using System.IO;
using System.Data;

namespace AdapterPattern.Basic.Renderer
{
    public class DataRenderer
    {
        private readonly IDbDataAdapter _dataAdapter;
        public DataRenderer(IDbDataAdapter dataReader)
        {
            _dataAdapter = dataReader;
        }

        public void Render(TextWriter writer)
        {
            writer.WriteLine("Render Started");
            var dataSet = new DataSet();

            _dataAdapter.Fill(dataSet);

            foreach(DataTable table in dataSet.Tables)
            {
                foreach (DataColumn column in table.Columns)
                {
                    writer.Write(column.ColumnName.PadRight(20) + " ");
                }
                writer.WriteLine();
                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        writer.Write(row[column].ToString().PadRight(20) + " ");
                    }
                    writer.WriteLine();
                }
            }
        }
    }
}
