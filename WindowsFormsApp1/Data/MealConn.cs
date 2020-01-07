using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class MealConn
    {
        private const String databaseUrl = "https://food-planner-708e2.firebaseio.com";

        private const String databaseSecret = "sYFSgBBBSpLa5fLVoFdlhx8wEcDQ8oYlcbt6OtsP";

        private const String node = "Meals/";

        private FirebaseClient firebase;


        public class Meal
        {
            public string Name { get; set; }
            public string MealDesc { get; set; }
            public string Time { get; set; }
        }
        public MealConn()
        {
            this.firebase = new FirebaseClient(
                databaseUrl,
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(databaseSecret)
                });
        }

        public async Task Get()
        {
            var results = await firebase.Child(node).OrderByKey().OnceAsync<Meal>();
            Main.mealList = new List<Meal>();

            foreach (var result in results)
            {
                Main.mealList.Add(
                   new Meal
                   {
                       Name = result.Key,
                       MealDesc = result.Object.MealDesc,
                       Time = result.Object.Time
                   }); ;
            }
        }
        public async Task Insert(Meal meal)
        {
            await firebase.Child(node).PostAsync(meal);
        }
    }
}
