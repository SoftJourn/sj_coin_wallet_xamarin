// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace SJCoinWallet.Helpers
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
  public static class Settings
  {
    private static ISettings AppSettings
    {
      get
      {
        return CrossSettings.Current;
      }
    }

    #region Setting Constants
              
		private const string ServiceEndpointKey = "service_endpoint_key";  
		private static readonly string ServiceEndpointDefault = "http://eris.softjourn.if.ua:1337";
              
		private const string MyAddressKey = "my_address_key";
		private static readonly string MyAddressDefault = "";
              
		private const string MyPubKeyKey = "my_pub_key_key";     
		private static readonly string MyPubKeyDefault = "";
              
		private const string MyPrivKeyKey = "my_priv_key_key";
		private static readonly string MyPrivKeyDefault = "";

    #endregion


    public static string ServiceEndpoint
    {
      get
      {
        return AppSettings.GetValueOrDefault<string>(ServiceEndpointKey, ServiceEndpointDefault);
      }
      set
      {
        AppSettings.AddOrUpdateValue<string>(ServiceEndpointKey, value);
      }
    }

	public static string MyAddress
	{
		get { return AppSettings.GetValueOrDefault(MyAddressKey, MyAddressDefault); }
		set { AppSettings.AddOrUpdateValue(MyAddressKey, value); }
	}

	public static string MyPubKey
	{
		get { return AppSettings.GetValueOrDefault(MyPubKeyKey, MyPubKeyDefault); }
		set { AppSettings.AddOrUpdateValue(MyPubKeyKey, value); }
	}

	public static string MyPrivKey
	{
		get { return AppSettings.GetValueOrDefault(MyPrivKeyKey, MyPrivKeyDefault); }
		set { AppSettings.AddOrUpdateValue(MyPrivKeyKey, value); }
	}
  }
}
