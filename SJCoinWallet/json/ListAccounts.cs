using System;
using Newtonsoft.Json;

namespace SJCoinWallet.Json
{
	public class ListAccounts
	{
		[JsonProperty(PropertyName = "accounts")]
		public Account[] Accounts { get; set; }
	}
}

