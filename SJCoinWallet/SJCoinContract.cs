using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SJCoinWallet.Json;

namespace SJCoinWallet
{
	public class SJCoinContract
	{
		private static String codeAddress = "8C7F03A7A927D412CB19499500529E6FBE9A1BA0";
		//"5DCFF4E2FAE97CDB8DB921386B97A2C16CB2E159"; 
		//"0849FFFCAAA4C0115DB35B15036E5B0C7587563E";
		private static String path = "/rpc";
		private static String balanceFormat = "37F42841000000000000000000000000{0}";
		private static String sendFormat = "D0679D34000000000000000000000000{0}00000000000000000000000000000000000000000000000000000000{1:X8}";

		public string url { get; set; }
		public string address { get; set; }
		public string publicKey { get; set; }
		private string privateKey;

		// 30F0558A87A06D51230FA7B99C9F334FB7F636B9
		public SJCoinContract(String url, String address, String publicKey, String privateKey)
		{
			this.url = url;
			this.address = address;
			this.publicKey = publicKey;
			this.privateKey = privateKey;
		}

		public void Mint()
		{
		}

		/*
		{"jsonrpc":"2.0","method":"erisdb.transactAndHold",
		"params":{
			"priv_key":"C8F19FCE954FD1F2F501F789DD4E3ADE82D00F6E8E6224F5BDF2DEBE04BC31462D21269EB62F56B0B05BE583DDBAE9740234143D913A26BE9924BE8C4853D90F",
			"address":"0849FFFCAAA4C0115DB35B15036E5B0C7587563E",
			"data":"D0679D34000000000000000000000000DC9A1D54D7E29668C3D12C64D4196FD52761AAC50000000000000000000000000000000000000000000000000000000000000064",
			"gas_limit":1000000,
			"fee":0},
		"id":"1"}

{	"result":
	[4,
	 {"call_data":{
	 	"caller":"30F0558A87A06D51230FA7B99C9F334FB7F636B9",
	 	"callee":"0849FFFCAAA4C0115DB35B15036E5B0C7587563E",
	 	"data":"D0679D34000000000000000000000000DC9A1D54D7E29668C3D12C64D4196FD52761AAC50000000000000000000000000000000000000000000000000000000000000064",
	 	"value":1,
	 	"gas":999701},
	 	"origin":"30F0558A87A06D51230FA7B99C9F334FB7F636B9",
	 	"tx_id":"4574EA71083D8A71C4EBD23D850D7F7C8777D171",
	 	"return":"",
	 	"exception":""}
	 ],
	 "error":null,
	 "id":"1",
	 "jsonrpc":"2.0"}

		*/
		public string Send(String toAddress, long amount)
		{
			string hexValue = amount.ToString("X");
			if (hexValue.Length > 8)
			{
				return "Error: amount is too large for this POC to survive";
			}
			String json = MakeCall("erisdb.transactAndHold", new SendCallParams()
			{
				PrivKey = privateKey,
				Address = codeAddress,
				Data = String.Format(sendFormat, toAddress, amount),
				GasLimit = 1000000,
				Fee = 0
			});
			//String json = "{\t\"result\":\n\t[4,\n\t {\"call_data\":{\n\t \t\"caller\":\"30F0558A87A06D51230FA7B99C9F334FB7F636B9\",\n\t \t\"callee\":\"0849FFFCAAA4C0115DB35B15036E5B0C7587563E\",\n\t \t\"data\":\"D0679D34000000000000000000000000DC9A1D54D7E29668C3D12C64D4196FD52761AAC50000000000000000000000000000000000000000000000000000000000000064\",\n\t \t\"value\":1,\n\t \t\"gas\":999701},\n\t \t\"origin\":\"30F0558A87A06D51230FA7B99C9F334FB7F636B9\",\n\t \t\"tx_id\":\"4574EA71083D8A71C4EBD23D850D7F7C8777D171\",\n\t \t\"return\":\"\",\n\t \t\"exception\":\"\"}\n\t ],\n\t \"error\":null,\n\t \"id\":\"1\",\n\t \"jsonrpc\":\"2.0\"}";

			// {"result":null,"error":{"code":-32602,"message":"encoding/hex: odd length hex string"},"id":"1","jsonrpc":"2.0"}

			System.Diagnostics.Debug.WriteLine(json);
			var data = JsonConvert.DeserializeObject<SJCoinSendResult>(json,
																	 new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
			if (data != null && data.Result != null && data.Result.Length > 1)
			{
				json = data.Result[1].ToString();
				//System.Diagnostics.Debug.WriteLine(json);
				var result = JsonConvert.DeserializeObject<SJCoinSendCallData>(json,
																	 new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
				if (result != null && result.TxId != null && result.TxId.Length > 10)
				{
					// OK
					System.Diagnostics.Debug.WriteLine(result.TxId);
					return result.TxId;
				}
			}
			else if (data != null && data.Error != null && !string.IsNullOrEmpty(data.Error.ToString()))
			{
				json = data.Error.ToString();
				var result = JsonConvert.DeserializeObject <SJCoinSendResultError>(json,
																	 new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
				if (result != null && !string.IsNullOrEmpty(result.Message))
				{
					System.Diagnostics.Debug.WriteLine(result.Message);
					return "Error: " + result.Message;
				}
			}
			return "";
		}

		// {"result":{"return":"00000000000000000000000000000000000000000000000000000000000f38de","gas_used":0},"error":null,"id":"1","jsonrpc":"2.0"}
		public long QueryBalance()
		{
			String json = MakeCall("erisdb.call", new QueryCallParams()
			{
				From = address,
				Address = codeAddress,
				Data = String.Format(balanceFormat, address)
			});
			var data = JsonConvert.DeserializeObject<SJCoinBalanceResult>(json,
																	 new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

			if (data != null && data.Result != null && data.Result.Return != null)
			{
				return long.Parse(data.Result.Return, System.Globalization.NumberStyles.HexNumber);
			}
			return 0L;
		}

		/*
		 * tcpdump -s 0 -A port 1337
		 * WTF? Seriously? 
		 * https://bugzilla.xamarin.com/show_bug.cgi?id=18278
		 */
		private String MakeCall(String method, CallParams par)
		{
			String result = "";
			using (var client = new HttpClient())
			{
				try
				{
					SJCoinCall callData = new SJCoinCall()
					{
						JsonRPC = "2.0",
						Method = method,
						Params = par,
						Id = "1"
					};
					String content = JsonConvert.SerializeObject(callData);
					//System.Diagnostics.Debug.WriteLine(content);

					HttpContent httpContent = new StringContent(content,
																System.Text.Encoding.UTF8,
																"application/json");
					Task<HttpResponseMessage> responseAsync = client.PostAsync(url + path, httpContent);
					HttpResponseMessage response = responseAsync.Result;
					Task<String> bodyAsync = response.Content.ReadAsStringAsync();
					result = bodyAsync.Result;
					//System.Diagnostics.Debug.WriteLine(result);

				}
				catch (HttpRequestException e)
				{
					// Handle exception.
					System.Diagnostics.Debug.WriteLine(e.Message);
				}
			}
			return result;
		}
	}
}