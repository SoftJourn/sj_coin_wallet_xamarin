using System;
using Newtonsoft.Json;

namespace SJCoinWallet.Json
{
	public class Account
	{
		[JsonProperty(PropertyName = "address")]
		public string Address { get; set; }
		[JsonProperty(PropertyName = "pub_key")]
		public Object[] PubKey { get; set; }
		[JsonProperty(PropertyName = "sequence")]
		public long Sequence { get; set; }
		[JsonProperty(PropertyName = "balance")]
		public long Balance { get; set; }
		[JsonProperty(PropertyName = "code")]
		public string Code { get; set; }
		[JsonProperty(PropertyName = "storage_root")]
		public string StorageRoot { get; set; }
		[JsonProperty(PropertyName = "permissions")]
		public Permissions Permissions { get; set; }

	}

	public class Permissions
	{
		[JsonProperty(PropertyName = "base")]
		public PermissionDetails Base { get; set; }
		[JsonProperty(PropertyName = "roles")]
		public string[] Roles { get; set; }
	}

	public class PermissionDetails
	{
		[JsonProperty(PropertyName = "perms")]
		public long Perms { get; set; }
		[JsonProperty(PropertyName = "set")]
		public long Set { get; set; }
	}
}

