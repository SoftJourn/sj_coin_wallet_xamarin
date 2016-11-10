using System;
using Newtonsoft.Json;

namespace SJCoinWallet.Json
{
	public class SJCoinBalanceResult
	{
		[JsonProperty(PropertyName = "result")]
		public ResultDetails Result { get; set; }
		[JsonProperty(PropertyName = "error")]
		public string Error { get; set; }
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }
		[JsonProperty(PropertyName = "jsonrpc")]
		public string JsonRPC { get; set; }
	}

	public class ResultDetails
	{
		[JsonProperty(PropertyName = "return")]
		public string Return { get; set; }
		[JsonProperty(PropertyName = "gas_used")]
		public string GasUsed { get; set; }
	}
}
