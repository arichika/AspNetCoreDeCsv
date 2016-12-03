using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace AspNetCoreDeCsv.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Produces("text/csv")]
        public async Task<IActionResult> GetCsv()
        {
            // Response.ContentType = "text/csv"; //これでも
            Response.Headers.Add(
                new KeyValuePair<string, StringValues>(
                    "Content-Disposition",
                    new StringValues(@"attachment; filename=""data_"
                        + RuntimeInformation.OSDescription
                        + @"_.csv""")));

            return Ok(GetData());
        }

        private static IEnumerable<CsvLayout> GetData()
        {
            return new List<CsvLayout>
            {
                new CsvLayout
                {
                    RowId = 0,
                    日本語列 = "日本語、あいうabcｼｰｴｽブイ　。",
                    date_time_0 = DateTime.MinValue,
                    date_time_1 = DateTime.MinValue.ToString("yyyy/MM/dd HH:mm:ss"),
                },
                new CsvLayout
                {
                    RowId = 1,
                    日本語列 = "さろげ𩸽𩸽𩹉さろげ",
                    date_time_0 = DateTime.MaxValue,
                    date_time_1 = DateTime.MaxValue.ToString("yyyy/MM/dd HH:mm:ss"),
                }
            };
        }

        public class CsvLayout
        {
            public int RowId { get; set; }
            public string 日本語列 { get; set; }
            public DateTime date_time_0 { get; set; }
            public string date_time_1 { get; set; }
        }
    }
}
