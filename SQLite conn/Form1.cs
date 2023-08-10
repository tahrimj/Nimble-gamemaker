using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sqlite_Connection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Database_Sqlite database_Sqlite = new Database_Sqlite();
            database_Sqlite.CreateConnection("ytp");
            database_Sqlite.CreateTable();
            database_Sqlite.DeleteData(2);
            database_Sqlite.ReadData();

           
            InitializeComponent();
        }
    }
}
