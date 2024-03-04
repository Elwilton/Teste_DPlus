using System;
namespace TesteDPlus.Db
{
	public static class ConexaoDB
	{
        public static string DevolverRota(string nomeBaseDados)
		{
			string rotaBaseDados = string.Empty;
			if(DeviceInfo.Platform == DevicePlatform.Android)
			{
				rotaBaseDados = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				rotaBaseDados = Path.Combine(rotaBaseDados, nomeBaseDados);
			}
			else if (DeviceInfo.Platform == DevicePlatform.iOS)
			{
				rotaBaseDados = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				rotaBaseDados = Path.Combine(rotaBaseDados, "..", "Library", nomeBaseDados);
			}
			
				return rotaBaseDados;
			
		}
	}
}

