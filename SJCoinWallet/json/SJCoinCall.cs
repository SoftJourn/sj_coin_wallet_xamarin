using System;
using Newtonsoft.Json;

namespace SJCoinWallet.Json
{
	public class SJCoinCall
	{
		[JsonProperty(PropertyName = "jsonrpc")]
		public string JsonRPC { get; set; }
		[JsonProperty(PropertyName = "method")]
		public string Method { get; set; }
		[JsonProperty(PropertyName = "params")]
		public CallParams Params { get; set; }
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }
	}

	public class CallParams
	{
		[JsonProperty(PropertyName = "address")]
		public string Address { get; set; }
		[JsonProperty(PropertyName = "data")]
		public string Data { get; set; }
	}

	public class SendCallParams : CallParams
	{
		[JsonProperty(PropertyName = "priv_key")]
		public string PrivKey { get; set; }
		[JsonProperty(PropertyName = "gas_limit")]
		public long GasLimit { get; set; }
		[JsonProperty(PropertyName = "fee")]
		public long Fee { get; set; }
	}

	public class QueryCallParams : CallParams
	{
		[JsonProperty(PropertyName = "from")]
		public string From { get; set; }
	}
}
