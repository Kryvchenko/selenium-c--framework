using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ERPTestingCSharpSelenium.utilities
{
	public class JsonReader
	{
		public JsonReader()
		{
		}

		public string extractData(string tokenName)
		{
			String myJsonString = File.ReadAllText("utilities/testData.json");

			var jsonObject = JToken.Parse(myJsonString);
			return jsonObject.SelectToken(tokenName).Value<string>();
        }
	}
}

