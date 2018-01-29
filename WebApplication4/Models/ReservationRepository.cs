using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace WebApplication4.Models
{
    public class ReservationRepository
    {
        //not used:
        public void LoadJson(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                List<Reservation> data = JsonConvert.DeserializeObject<List<Reservation>>(json);
            }
        }


        private static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Content/rezerwacje.json");
        
        private static ReservationRepository repo = new ReservationRepository(path); 
        public static ReservationRepository Current
        {
            get
            {
                return repo;
            }
        }


        public ReservationRepository(string jsonFile) {

            
            
          //  string json = File.ReadAllText(jsonFile);
            //Console.WriteLine(json);
            //List<Reservation> data = JsonConvert.DeserializeObject<List<Reservation>>(json);
            using (StreamReader r = new StreamReader(jsonFile))
            {
                string json = r.ReadToEnd();
                List<Reservation> data = JsonConvert.DeserializeObject<List<Reservation>>(json);
                this.data = data;
            };
           
        }

        List<Reservation> data = new List<Reservation>();
    

        public IEnumerable<Reservation> GetAll()
        {
            return data;
        }

        public Reservation Get(int id)
        {
            return data.Where(r => r.ReservationId == id).FirstOrDefault();
        }
        public Reservation Add(Reservation item)
        {
            item.ReservationId = data.Count + 1;
            data.Add(item);
            SaveJson(path);
            return item;
        }

        public void Remove(int id)
        {
            Reservation item = Get(id);
            if (item != null)
            {
                data.Remove(item);
                SaveJson(path);
            }
        }
        public bool Update(Reservation item) {
            Reservation storedItem = Get(item.ReservationId);
            if (storedItem != null) {
                storedItem.ClientName = item.ClientName;
                storedItem.Location = item.Location;
                SaveJson(path);
                return true;
            } else {
                return false;
            }
        }
        public void SaveJson(string jsonFile)
        {
            JsonSerializer serializer = new JsonSerializer();

            using (StreamWriter sw = new StreamWriter(jsonFile))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, data);
                // {"ExpiryDate":new Date(1230375600000),"Price":0}
            }
        }

    }
}