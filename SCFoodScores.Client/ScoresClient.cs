using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using NLog;
using SCFoodScores.Client.DTOs;

namespace SCFoodScores.Client
{
	public class ScoresClient
	{
		private readonly ILogger _logger;

		public ScoresClient(ILogger logger)
		{
			_logger = logger;
		}

		public async Task<List<Inspection>> Search(string grade, string facilityType, string county)
		{
			using (var http = new HttpClient())
			{
				var response = await http.GetAsync("http://www.scdhec.gov/Apps/Environment/FoodGrades/rate.aspx?pg=" + grade + "&ft=" + facilityType + "&cy=" + county);
				response.EnsureSuccessStatusCode();

				var responseContent = await response.Content.ReadAsStringAsync();

				var doc = new HtmlDocument();
				doc.LoadHtml(responseContent);

				var rows = doc.DocumentNode.SelectNodes("//table/tbody/tr");

				var inspections = new List<Inspection>();
				foreach (var row in rows)
				{
					var cells = row.SelectNodes("./td");

					var name = cells[0].InnerText;
					var street = cells[1].InnerText;
					var city = cells[2].InnerText;
					var gradeResult = cells[3].InnerText;
					var date = cells[4].InnerText;
					var type = cells[5].InnerText;

					var inspectionUrl = cells[6].SelectSingleNode(".//a").GetAttributeValue("href", "");

					var inspection = new Inspection()
					{
						City = city,
						EstablishmentName = name,
						FullReportUrl = new Uri(inspectionUrl),
						Grade = gradeResult,
						InspectionDate = DateTime.Parse(date),
						Street = street,
						Type = type,
					};

					inspections.Add(inspection);
				}

				return inspections;
			}
		}

		public class Constants
		{
			public class SearchValues
			{
				public class FacilityTypes
				{
					public const string INSTITUTION = "208";
					public const string GROCERY_STORE = "211";
					public const string MOBILE_FOOD_UNIT = "204";
					public const string RETAIL_FOOD_ESTABLISHMENT = "206";
				}

				public class Counties
				{
					public const string ABBEVILLE = "01";
					public const string AIKEN = "02";
					public const string ALLENDALE = "03";
					public const string ANDERSON = "04";
					public const string BAMBERG = "05";
					public const string BARNWELL = "06";
					public const string BEAUFORT = "07";
					public const string BERKELEY = "08";
					public const string CALHOUN = "09";
					public const string CHARLESTON = "10";
					public const string CHEROKEE = "11";
					public const string CHESTER = "12";
					public const string CHESTERFIELD = "13";
					public const string CLARENDON = "14";
					public const string COLLETON = "15";
					public const string DARLINGTON = "16";
					public const string DILLON = "17";
					public const string DORCHESTER = "18";
					public const string EDGEFIELD = "19";
					public const string FAIRFIELD = "20";
					public const string FLORENCE = "21";
					public const string GEORGETOWN = "22";
					public const string GREENVILLE = "23";
					public const string GREENWOOD = "24";
					public const string HAMPTON = "25";
					public const string HORRY = "26";
					public const string JASPER = "27";
					public const string KERSHAW = "28";
					public const string LANCASTER = "29";
					public const string LAURENS = "30";
					public const string LEE = "31";
					public const string LEXINGTON = "32";
					public const string MARION = "33";
					public const string MARLBORO = "34";
					public const string MCCORMICK = "35";
					public const string NEWBERRY = "36";
					public const string OCONEE = "37";
					public const string ORANGEBURG = "38";
					public const string PICKENS = "39";
					public const string RICHLAND = "40";
					public const string SALUDA = "41";
					public const string SPARTANBURG = "42";
					public const string SUMTER = "43";
					public const string UNION = "44";
					public const string WILLIAMSBURG = "45";
					public const string YORK = "46";

					public Dictionary<string, string> List = new Dictionary<string, string>()
					{
						{ "ABBEVILLE", "01" },
						{ "AIKEN", "02" },
						{ "ALLENDALE", "03" },
						{ "ANDERSON", "04" },
						{ "BAMBERG", "05" },
						{ "BARNWELL", "06" },
						{ "BEAUFORT", "07" },
						{ "BERKELEY", "08" },
						{ "CALHOUN", "09" },
						{ "CHARLESTON", "10" },
						{ "CHEROKEE", "11" },
						{ "CHESTER", "12" },
						{ "CHESTERFIELD", "13" },
						{ "CLARENDON", "14" },
						{ "COLLETON", "15" },
						{ "DARLINGTON", "16" },
						{ "DILLON", "17" },
						{ "DORCHESTER", "18" },
						{ "EDGEFIELD", "19" },
						{ "FAIRFIELD", "20" },
						{ "FLORENCE", "21" },
						{ "GEORGETOWN", "22" },
						{ "GREENVILLE", "23" },
						{ "GREENWOOD", "24" },
						{ "HAMPTON", "25" },
						{ "HORRY", "26" },
						{ "JASPER", "27" },
						{ "KERSHAW", "28" },
						{ "LANCASTER", "29" },
						{ "LAURENS", "30" },
						{ "LEE", "31" },
						{ "LEXINGTON", "32" },
						{ "MARION", "33" },
						{ "MARLBORO", "34" },
						{ "MCCORMICK", "35" },
						{ "NEWBERRY", "36" },
						{ "OCONEE", "37" },
						{ "ORANGEBURG", "38" },
						{ "PICKENS", "39" },
						{ "RICHLAND", "40" },
						{ "SALUDA", "41" },
						{ "SPARTANBURG", "42" },
						{ "SUMTER", "43" },
						{ "UNION", "44" },
						{ "WILLIAMSBURG", "45" },
						{ "YORK", "46" },
					};
				}
			}
		}
	}
}