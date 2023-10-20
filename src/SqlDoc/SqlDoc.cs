using System;
using System.Linq;
using System.Collections.Generic;

namespace SqlDoc
{
    public class Database {
        public IEnumerable<DatabaseObject> Objects { get; set; } = Enumerable.Empty<DatabaseObject>();
    }

    public interface DatabaseObject {
        string Name { get; set; }
    }

    public class View : DatabaseObject {
        public IEnumerable<Column> Columns { get; set; } = Enumerable.Empty<Column>();
        public IEnumerable<Table> Dependencies { get; set; } = Enumerable.Empty<Table>();
        public string Name { get; set; } = "";
    }

    public class Table : DatabaseObject {
        public IEnumerable<Column> Columns { get; set; } = Enumerable.Empty<Column>();
    
        public string Name { get; set; } = "";

        public override string ToString()
        {
            string output = "";
        
            output = "Table: " + Name + "\n";
            foreach(var column in Columns) {
                output = output + "\t" + column.Name + " of type " + column.Name;
            }

            return output;
        }
    }

    public class Column : DatabaseObject {
        public string Name { get; set; } = "";
    
        public string Type { get; set; } = "";
    }
    
    public class StoredProcedure : DatabaseObject 
    {
        public StoredProcedure(string name, ICollection<DatabaseObject> dependencies)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            Name = name;

            Dependencies = dependencies ?? throw new ArgumentNullException(nameof(dependencies));
        }
        
        public ICollection<DatabaseObject> Dependencies { get; }
        
        public string Name { get; set; }

        public override string ToString()
        {
            string output = "";
        
            output = "Stored Procedure: " + Name + "\n";
            foreach (var item in Dependencies)
            {
                if (item is Table)
                {
                    var table = (Table)item;
                    output += table.ToString();
                }
                if (item is View)
                {
                    var view = (View)item;
                    output += view.ToString();
                }
            }
            return output;
        }
    }
}
