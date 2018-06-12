using System.Threading.Tasks;
using SCFoodScores.Client;

namespace SCFoodScores.Host
{
	class Program
	{
		static void Main(string[] args)
		{
			Run().ConfigureAwait(false).GetAwaiter().GetResult();
		}

		private static async Task Run()
		{
			var logger = NLog.LogManager.GetCurrentClassLogger();
			var client = new ScoresClient(logger);

			var inspections = await client.Search("A", ScoresClient.Constants.SearchValues.FacilityTypes.GROCERY_STORE, ScoresClient.Constants.SearchValues.Counties.GREENVILLE);
		}
	}
}
