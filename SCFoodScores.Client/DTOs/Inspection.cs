using System;

namespace SCFoodScores.Client.DTOs
{
	public class Inspection
	{
		public string EstablishmentName { get; set; }
		public string Street { get; set; }
		public string City { get; set; }
		public string Grade { get; set; }
		public DateTime InspectionDate { get; set; }
		public string Type { get; set; }
		public Uri FullReportUrl { get; set; }
	}
}