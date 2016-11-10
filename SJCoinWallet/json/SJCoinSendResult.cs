using System;
using Newtonsoft.Json;

namespace SJCoinWallet.Json
{
	public class SJCoinSendResult
	{
		[JsonProperty(PropertyName = "result")]
		public Object [] Result { get; set; }
		[JsonProperty(PropertyName = "error")]
		public Object Error { get; set; }
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }
		[JsonProperty(PropertyName = "jsonrpc")]
		public string JsonRPC { get; set; }
	}

	public class SJCoinSendResultError
	{
		[JsonProperty(PropertyName = "code")]
		public string Code { get; set; }
		[JsonProperty(PropertyName = "message")]
		public string Message { get; set; }
	}

	public class SJCoinSendCallData
	{
		[JsonProperty(PropertyName = "call_data")]
		public SJCoinSendCallDataDetails Details { get; set; }
		[JsonProperty(PropertyName = "origin")]
		public string Origin { get; set; }
		[JsonProperty(PropertyName = "tx_id")]
		public string TxId { get; set; }
		[JsonProperty(PropertyName = "return")]
		public Object Return { get; set; }
		[JsonProperty(PropertyName = "exception")]
		public Object Exception { get; set; }

	}

	public class SJCoinSendCallDataDetails
	{
		[JsonProperty(PropertyName = "caller")]
		public string Caller { get; set; }
		[JsonProperty(PropertyName = "callee")]
		public string Callee { get; set; }
		[JsonProperty(PropertyName = "data")]
		public string Data { get; set; }
		[JsonProperty(PropertyName = "value")]
		public long Value { get; set; }
		[JsonProperty(PropertyName = "gas")]
		public long Gas { get; set; }
	}
}

