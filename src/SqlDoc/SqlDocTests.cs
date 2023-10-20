using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SqlDoc
{
    public class SqlDocTests
    {
        [Test]
        public void Rendering_A_Table_Works_Correctly()
        {         
            // Arrange       
            var table = new Table();
            table.Name = "Foo";

            var columnA = new Column();
            columnA.Name="Bar";
            columnA.Type="int";
            table.Columns = new List<Column>{columnA};

            // Act
            var tableString = table.ToString();

            // Assert
            Assert.That(tableString, Is.EqualTo($"Table: Foo{Environment.NewLine}\tBar of type int"));
        }
        
        [Test]
        public void StoredProcedure_ToString_ShouldPrintValidData()
        {
            var table = new Table
            {
                Name = "Foo"
            };
            var columnA = new Column();
            columnA.Name="Bosh";
            columnA.Type="int";
            table.Columns = new List<Column>{columnA};

            View view = new View()
            {
                Name = "Bannana",
                Columns = new List<Column>(),
                Dependencies = new List<Table>()
            };
            Table salaryTable = new Table()
            {
                Name = "Salary"
            };
            var salaryColumnA = new Column();
            salaryColumnA.Name="Bosh";
            salaryColumnA.Type="int";
            salaryTable.Columns = new List<Column>{salaryColumnA};

            ICollection<DatabaseObject> dependencies = new List<DatabaseObject>()
            {
                table,
                view
            };
            
            StoredProcedure sp = new StoredProcedure("Foo", dependencies);
            
            var expected = "Stored Procedure: Foo" + Environment.NewLine +
                           "\tTable: Baz" + Environment.NewLine +
                           "\t\tBosh of type int" + Environment.NewLine +
                           "\tView: Banana" + Environment.NewLine +
                           "\t\tTable: Salary" + Environment.NewLine +
                           "\t\t\tBosh of type int";
            
            
            Assert.That(sp.ToString(), Is.EqualTo(expected));
        }
    }
    
}
