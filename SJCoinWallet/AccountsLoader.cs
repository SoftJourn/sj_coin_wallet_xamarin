using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SJCoinWallet.Json;

namespace SJCoinWallet
{
	// https://github.com/eris-ltd/eris-db/blob/master/api.md
	public class AccountsLoader
	{
		private static String path = "/accounts";

		public string url { get; set; }
		public List<String> accounts { get; }

		public AccountsLoader(String url)
		{
			this.url = url;
			accounts = new List<string>();
		}

		/**
		 * http://www.asp.net/web-api/overview/advanced/calling-a-web-api-from-a-net-client
		 */
		//public async Task<List<String>> LoadAccountsAsyc()
		public List<String> LoadAccountsAsyc()
		{
            using (var client = new HttpClient())
			{
				try
				{
					Task<String> stringAsync = client.GetStringAsync(url + path);
					//stringAsync.RunSynchronously();
					String json = stringAsync.Result;
					//System.Diagnostics.Debug.WriteLine(json);

					var result = JsonConvert.DeserializeObject<ListAccounts>(
						json,
						new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
					);

					if (result == null || result.Accounts == null || result.Accounts.Length < 1)
					{
						throw new HttpRequestException("Empty account list");
					}

					accounts.Clear();
					foreach (Account account in result.Accounts)
					{
						// TODO: calculate flags properly 2118 - Participant, 16383 - Root & Full
						// We basically need accounts that allowed to run smart contracts & transact
						if (account.Permissions.Base.Perms == 2118 || account.Permissions.Base.Perms == 16383)
						{
							accounts.Add(account.Address);
						}
						//16383 = 8192 + 4096 + 2048 + 1024 + 512 + 256 + 128 + 64 + 32 + 16 + 8 + 4 + 2 + 1
						// 2118 = 2048 + 64 + 4 + 2

						// 2302 = 2048 + 128 + 64 + 32 + 16 + 8 + 4 + 2
						// https://github.com/eris-ltd/eris-cm/blob/master/Godeps/_workspace/src/github.com/tendermint/tendermint/permission/types/permissions.go
						// https://github.com/tendermint/tendermint/wiki/Eris-Permissions
					}
					System.Diagnostics.Debug.WriteLine(accounts.Count);
				}
				catch (HttpRequestException e)
				{
					// Handle exception.
					System.Diagnostics.Debug.WriteLine(e.Message);
				}
			}
			return accounts;
		}

		public List<String> LoadAccounts()
		{
			//LoadAccountsAsyc().Wait();
			LoadAccountsAsyc();
			return accounts;
		}

	}
}
