using System;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
namespace TesteDPlus.DTOs
{
	public partial class ClienteDTO : ObservableObject
	{
        [ObservableProperty]
        public int idCliente;

        [ObservableProperty]
        public string nome;

        [ObservableProperty]
        public  string sobreNome;

        [ObservableProperty]
        public int idade;

        [ObservableProperty]
        public  string endereco;
    }
}

