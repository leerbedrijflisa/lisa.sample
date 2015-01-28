using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Lisa.Sample.Models;
using Newtonsoft.Json;

namespace Lisa.Sample.Access
{
    public class PersonProxy
    {
        public async Task<IEnumerable<Person>> GetAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61063/");
                var result = await client.GetAsync("/api/persons");

                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Person>>(json);
            }
        }

        public async Task<Person> GetAsync(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61063/");
                var url = String.Format("/api/persons/{0}", id);
                var result = await client.GetAsync(url);

                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                var json = await result.Content.ReadAsStringAsync();
                var person = JsonConvert.DeserializeObject<Person>(json);
                return person;
            }
        }

        public async Task<Person> PostAsync(Person person)
        {
            var json = JsonConvert.SerializeObject(person);

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:61063/");
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync("/api/persons/", content);

                var response = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Person>(response);
            }
        }
    }
}