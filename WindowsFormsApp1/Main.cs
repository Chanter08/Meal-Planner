using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsFormsApp1.MealConn;

namespace WindowsFormsApp1
{
    public sealed partial class Main : Form
    {
        public static List<Meal> mealList;
        public Main()
        {
            InitializeComponent();
            GetMeals();
        }

        private void GetMeals()
        {
            listView1.View = View.Details;
            listView1.Columns.Add("Meal");
            listView1.Columns.Add("Description");
            listView1.Columns.Add("Time Taken");
            listView1.GridLines = true;

            mealList = new List<Meal>();

            // and a new connection

            MealConn conn = new MealConn();

            // get the data and add it to the heroList

            Task.Run(() => conn.Get()).Wait();

            foreach (Meal m in mealList)
            {
                listView1.Items.Add(new ListViewItem(new string[] { m.Name, m.MealDesc, m.Time }));
            }
        }
    }
}
