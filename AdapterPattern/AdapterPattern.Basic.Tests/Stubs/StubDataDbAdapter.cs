using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AdapterPattern.Basic.Tests
{
    public class StubDataDbAdapter : IDbDataAdapter
    {
        public IDbCommand DeleteCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IDbCommand InsertCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IDbCommand SelectCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IDbCommand UpdateCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public MissingMappingAction MissingMappingAction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public MissingSchemaAction MissingSchemaAction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ITableMappingCollection TableMappings => throw new NotImplementedException();

        public int Fill(DataSet dataSet)
        {
            var table = new DataTable();
            table.Columns.Add(new DataColumn("Id", typeof(int)));
            table.Columns.Add(new DataColumn("Hero", typeof(string)));
            table.Columns.Add(new DataColumn("Alias", typeof(string)));

            var row_1 = table.NewRow();
            row_1[0] = 1;
            row_1[1] = "Spiderman";
            row_1[2] = "Peter Parker";
            table.Rows.Add(row_1);
            var row_2 = table.NewRow();
            row_2[0] = 2;
            row_2[1] = "Batman";
            row_2[2] = "Bruce Wayne";
            table.Rows.Add(row_2);
            dataSet.Tables.Add(table);
            dataSet.AcceptChanges();

            return 2;
        }

        public DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType)
        {
            throw new NotImplementedException();
        }

        public IDataParameter[] GetFillParameters()
        {
            throw new NotImplementedException();
        }

        public int Update(DataSet dataSet)
        {
            throw new NotImplementedException();
        }
    }
}
