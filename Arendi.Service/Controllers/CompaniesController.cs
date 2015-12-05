using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Arendi.Service.Models;

namespace Arendi.Service.Controllers
{
    public class CompaniesController : ApiController
    {
        private ArendiDBEntities db = new ArendiDBEntities();

        [Route("get/getcompanies")]
        [HttpGet]
        public List<string> GetCompanies()
        {
            var companies = db.Companies;
            var company_names = new List<string>();

            if (db.Companies != null)
            {
                foreach (var company in companies)
                {
                    company_names.Add(company.CompanyName);
                }
                return company_names;
            }
            else
            {
                return null;
            }
        }// GetCompanies

        [Route("get/deletecompany")]
        [HttpGet]
        public bool DeleteCompany(string name)
        {
            Company company;

            try
            {
                company = db.Companies.Where(c => c.CompanyName == name).First();
                db.Entry(company).State = EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }// DeleteCompany

        [Route("get/addcompany")]
        [HttpGet]
        public bool AddCompany(string name)
        {
            Company company_to_add = new Company
            {
                CompanyName = name
            }; 

            try
            {
                // If company_to_add exists, do not add again
                if (db.Companies.Where(c => c.CompanyName == name).Count() != 0)
                    return false;

                db.Companies.Add(company_to_add);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }// AddCompany
    }
}