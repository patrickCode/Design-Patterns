using System;
using System.Collections.Generic;
using System.Text;
using AdapterPattern.Basic.Model;
using System.Data;
using AdapterPattern.Basic.Renderer;
using System.IO;

namespace AdapterPattern.Basic
{
    public class SuperHeroRendererAdapter : ISuperheroRendererAdapter
    {
        private IDbDataAdapter _adapter;
        public SuperHeroRendererAdapter() { }

        public string ToString(List<Superhero> heroes)
        {
            _adapter = new SuperHeroDbAdapter(heroes);
            var renderer = new DataRenderer(_adapter);

            var textWriter = new StringWriter();
            renderer.Render(textWriter);

            return textWriter.ToString();
        }

        internal class SuperHeroDbAdapter : IDbDataAdapter
        {
            private List<Superhero> _heroes;
            public IDbCommand DeleteCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public IDbCommand InsertCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public IDbCommand SelectCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public IDbCommand UpdateCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public MissingMappingAction MissingMappingAction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public MissingSchemaAction MissingSchemaAction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public ITableMappingCollection TableMappings => throw new NotImplementedException();

            public SuperHeroDbAdapter(List<Superhero> heroes)
            {
                _heroes = heroes;
            }

            public int Fill(DataSet dataSet)
            {
                var table = new DataTable();
                table.Columns.Add(new DataColumn("Id", typeof(int)));
                table.Columns.Add(new DataColumn("Hero", typeof(string)));
                table.Columns.Add(new DataColumn("Alias", typeof(string)));

                foreach(var hero in _heroes)
                {
                    var row = table.NewRow();
                    row[0] = hero.Id;
                    row[1] = hero.Name;
                    row[2] = hero.Alias;
                    table.Rows.Add(row);
                }
                dataSet.Tables.Add(table);
                dataSet.AcceptChanges();

                return _heroes.Count;
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
}
