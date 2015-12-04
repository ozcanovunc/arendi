using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Arendi.Controllers
{
    public static class CompanyController
    {
        private static readonly HttpClient CompanyControllerClient = new HttpClient
        {
            BaseAddress = new Uri(App.BaseAddress)
        };

        public static async Task<List<string>> GetCompanies()
        {
            string requestString = "getcompanies";
            string requestResult = await CompanyControllerClient.GetStringAsync(requestString);
            List<string> companies = JsonConvert.DeserializeObject<List<string>>(requestResult);
            return companies;
        }

        public static async Task<bool> DeleteCompany(string name)
        {
            string requestString = "deletecompany?name=" + name;
            string requestResult = await CompanyControllerClient.GetStringAsync(requestString);
            bool isDeleted = JsonConvert.DeserializeObject<bool>(requestResult);
            return isDeleted;
        }

        public static async Task<bool> AddCompany(string name)
        {
            string requestString = "addcompany?name=" + name;
            string requestResult = await CompanyControllerClient.GetStringAsync(requestString);
            bool isAdded = JsonConvert.DeserializeObject<bool>(requestResult);
            return isAdded;
        }
    }
}
