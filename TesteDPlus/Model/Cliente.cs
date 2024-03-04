using System;
using System.ComponentModel.DataAnnotations;

namespace TesteDPlus.Model
{
	public class Cliente
	{
		[Key]
		public int IDCliente { get; set; }

		public required string Nome { get; set; }

        public required string SobreNome { get; set; }

		public int Idade { get; set; }

		public required string Endereco { get; set; }
	}
}

