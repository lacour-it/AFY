using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using AFYClient.Models;


namespace AFYClient.API
{
    internal class APIFactory
    {
        internal string APIURL { get; set; }
        internal Authenticate Authentification { get; set; }
        internal HttpClient client { get; set; }
        internal string HolidaysAPI;
        internal string EmployeesAPI;
        internal string WorkingAccountsAPI;
        internal string WorkingTimesAPI;

        public APIFactory()
        {
            Authentification = new Authenticate();
            APIURL = Authentification.APIURL;
            client = Authentification.client;
            HolidaysAPI = APIURL + "/holidays";
            EmployeesAPI = APIURL + "/employees";
            WorkingAccountsAPI = APIURL + "/workingaccounts";
            WorkingTimesAPI = APIURL + "/workingtimes";
        }

        public async Task<List<Holiday>> GetHolidaysAsync()
        {
            List<Holiday> holidays;
            string result = await client.GetStringAsync(HolidaysAPI);
            holidays = JsonConvert.DeserializeObject<List<Holiday>>(result);
            return holidays;
        }

        public async Task<Holiday> GetHolidayByIDAsync(int HolidayID)
        {
            Holiday holiday;
            string result = await client.GetStringAsync(HolidaysAPI + "/" + HolidayID);
            holiday = JsonConvert.DeserializeObject<Holiday>(result);
            return holiday;
        }

        public async Task<Holiday> SaveHolidayAsync(Holiday holiday)
        {
            HttpResponseMessage message;
            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.TryAddWithoutValidation(TokenString1, TokenString2);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string json = JsonConvert.SerializeObject(holiday);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            if (holiday.HolidayId > 0)
                message = await client.PutAsync(HolidaysAPI + "/" + holiday.HolidayId, content);
            else
                message = await client.PostAsync(HolidaysAPI, content);
            if (message.IsSuccessStatusCode)
            {
                string res = await message.Content.ReadAsStringAsync();
                holiday = JsonConvert.DeserializeObject<Holiday>(res);
                return holiday;
            }
            else
            {
                return null;
            }
        }

        public async Task<Uri> CreateHolidayAsync(Holiday holiday)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsJsonAsync(
                HolidaysAPI, holiday);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

    }
}
