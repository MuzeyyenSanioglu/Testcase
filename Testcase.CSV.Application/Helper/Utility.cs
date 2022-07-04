using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testcase.CSV.Application.Helper
{
    public class Utility
    {
        public JObject ConvertToNameFromCSVFile(string fileName)
        {
            var fileObject = fileName.Split("_");
            dynamic result = new JObject();
            result.UserId = fileObject[0];
            result.AppoinmentId = fileObject[1];
            result.TestId = fileObject[2];

            return result;
        }
        public  DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }

            }


            return dt;
        }
    }
}
