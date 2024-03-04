using System;
using CommunityToolkit.Mvvm.Messaging.Messages;
namespace TesteDPlus.Db
{
	public class ClienteMensageria : ValueChangedMessage<ClienteMensagem>
	{
		public ClienteMensageria(ClienteMensagem value) : base(value)
		{

		}
	}
}

