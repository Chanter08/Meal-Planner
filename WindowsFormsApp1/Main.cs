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
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker2.Format = DateTimePickerFormat.Time;
            GetMeals();
        }

        private void GetMeals()
        {
            listMeals.Columns.Clear();
            listMeals.Rows.Clear();
            listMeals.Columns.Add("Meal","Meal");
            listMeals.Columns.Add("Description","Description");
            listMeals.Columns.Add("Time Eaten","Time Eaten");
            //dt.Columns.Add("Image",typeof(Image));
           

            mealList = new List<Meal>();

            // and a new connection

            MealConn conn = new MealConn();

            // get the data and add it to the heroList

            Task.Run(() => conn.Get()).Wait();

            foreach (Meal m in mealList)
            {
                listMeals.Rows.Add(new string[] { m.Name, m.MealDesc, m.Time });
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            MealConn conn = new MealConn();
            Meal newMeal = new Meal();

            newMeal.Name = txtMeal.Text;
            newMeal.MealDesc = txtDesc.Text;
            newMeal.Time = dateTimePicker1.Value.ToShortDateString() + " " + dateTimePicker2.Value.Hour.ToString() + ":"+ dateTimePicker2.Value.Minute.ToString();

            Task.Run(() => conn.Insert(newMeal)).Wait();

            foreach (Control c in tabPage1.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }

                dateTimePicker1.Value = DateTime.Today;

                dateTimePicker2.Value = DateTime.Now;
            }

            GetMeals();
        }
    }
}
