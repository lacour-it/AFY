using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using AFYClient.Models;

namespace AFYClient.API
{
    internal class Authenticate
    {
        internal string APIURL { get; set; }
        internal HttpClient client { get; set; }
        internal string ValuesAPI;
        internal string UsersAPI;


        public Authenticate()
        {
            APIURL = GetURL();
            client = new HttpClient();
            client.BaseAddress = new Uri(APIURL);
            ValuesAPI = APIURL + "/values";
            UsersAPI = APIURL + "/users";

        }
        internal string GetURL()
        {
            return ConfigurationManager.AppSettings["APIURL"];
        }

        /*public async Task<UserDto> Register(UserDto user)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.TryAddWithoutValidation(TokenString1, TokenString2);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string json = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            string registerAPI = UsersAPI + "/register";
            HttpResponseMessage message = await client.PostAsJsonAsync(registerAPI, content);
            if (message.IsSuccessStatusCode)
            {
                string res = await message.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<UserDto>(res);
                return user;
            }
            else
            {
                return null;
            }
        }*/

        public async Task<List<string>> GetValuesAsync()
        {
            List<string> values;
            string result = await client.GetStringAsync(ValuesAPI);
            values = JsonConvert.DeserializeObject<List<string>>(result);
            return values;
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            List<UserDto> users;
            string result = await client.GetStringAsync(UsersAPI);
            users = JsonConvert.DeserializeObject<List<UserDto>>(result);
            return users;
        }

        public async Task<UserDto> GetUserByIDAsync(int UserID)
        {
            UserDto user;
            string result = await client.GetStringAsync(UsersAPI + "/" + UserID);
            user = JsonConvert.DeserializeObject<UserDto>(result);
            return user;
        }

        public async Task<UserDto> SaveUserAsync(UserDto user)
        {
            HttpResponseMessage message;
            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.TryAddWithoutValidation(TokenString1, TokenString2);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string json = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            if (user.Id > 0)
                message = await client.PutAsync(UsersAPI + "/" + user.Id, content);
            else
                message = await client.PostAsync(UsersAPI, content);
            if (message.IsSuccessStatusCode)
            {
                string res = await message.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<UserDto>(res);
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<Uri> CreateUserAsync(UserDto user)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsJsonAsync(
                UsersAPI, user);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

    }
}
