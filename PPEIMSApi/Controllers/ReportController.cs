
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Web.Http;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using PPEIMSApi.Models;


namespace PPEIMSApi.Controllers
{

    //this is from home
    public class ReportController : ApiController
    {

        private PPEIMSEntities db = new PPEIMSEntities();
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/greet")]
        public string Greet()
        {

            try
            {
                this.WriteLog("greet", true);
                return "Hi";
            }
            catch (Exception e)
            {

                return e.Message.ToString();
                throw;
            }


        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/printreport")]

        public byte[] PrintReports(string rvm)
        {


            var o = JsonConvert.DeserializeObject(rvm);
            ReportViewModel rptVM = JsonConvert.DeserializeObject<ReportViewModel>(rvm);
            string report = rptVM.Report;

            try
            {
                DataSet ds = new DataSet();
                LocalReport LocalReport = new LocalReport
                {
                    ReportPath = baseDir + "\\Reports\\" + report + ".rdlc"
                };
                DateTime def = new DateTime(1, 1, 1);

                string _strFilter = "";
                

                //NOT CONSOLIDATED
                if (rptVM.Report == "rptEmployeeSummary")
                {

                    var v =

                    db.RequestDetailUsers
                                   .Where(b => b.Status == "Active")
                                   .Where(b => b.DocumentStatus == 1)
                                   //.Where(b => b.UserId == User.Identity.GetUserId())
                                   .Where(b => b.RequestDetails.Status == "Active")
                                   .Where(b => b.RequestDetails.Requests.DocumentStatus == "Approved")
                                   .Where(a => a.RequestDetails.Requests.ApprovedDate >= rptVM.fromDate && a.RequestDetails.Requests.ApprovedDate <= rptVM.toDate)

                                   .Where(b=>b.Users.DepartmentId == rptVM.dept)
                                   .Select(a => new
                                   {
                                      
                                       
                                       
                                       
                                       EmployeeType = a.Users.Category,
                                       Months = a.Users.Category == "OFFICE" ? a.RequestDetails.Items.PPEs.Office : a.RequestDetails.Items.PPEs.Field,


                                       EmployeeName = a.Users.Name,
                                       Department = a.Users.Departments.Name,
                                       Category = a.Users.Category,
                                       ItemNo = a.RequestDetails.Items.No,
                                       a.RequestDetails.Items.Description,
                                       a.RequestDetails.Items.Description2,
                                       DateIssued = a.RequestDetails.Requests.WarehouseApprovedDate,
                                       PPEType = a.RequestDetails.Items.PPEs.Name,
                                       a.RequestDetails.Items.CompanyId

                                   });






                    var lsts = v.OrderBy(a => a.Description).ToList();
                    DataTable dts = new DataTable();
                    dts = ToDataTable(lsts);
                    ReportDataSource datasources = new ReportDataSource("EmployeeSummary", dts);
                    LocalReport.DataSources.Clear();
                    LocalReport.DataSources.Add(datasources);
                    return LocalReport.Render(rptVM.rptType);

                }
                else
                {
                    return null;
                }
                






            }
            catch (Exception e)
            {
                this.WriteLog(e.InnerException.Message.ToString(), true);
                throw;
            }


        }


        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public void WriteLog(string text, bool append)
        {
            StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\logs.txt", append);
            sw.Write(text);
            sw.Close();
        }
    }


}

