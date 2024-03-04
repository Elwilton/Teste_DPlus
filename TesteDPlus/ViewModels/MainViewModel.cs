using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using TesteDPlus.DataAccess;
using TesteDPlus.Db;
using TesteDPlus.DTOs;
using TesteDPlus.Model;
using System.Collections.ObjectModel;
using TesteDPlus.Views;

namespace TesteDPlus.ViewModels
{
	public partial class MainViewModel: ObservableObject
	{
		private readonly ClienteDbContext _dbContext;

		[ObservableProperty]
		private ObservableCollection<ClienteDTO> listaCliente = new ObservableCollection<ClienteDTO>();

		public MainViewModel(ClienteDbContext context)
		{
            _dbContext = context;

			MainThread.BeginInvokeOnMainThread(new Action(async () => await Obter()));

			WeakReferenceMessenger.Default.Register<ClienteMensageria>(this, (r, m) =>
			{
				ClienteMensagemRecebido(m.Value);

            });
        }

		public async Task Obter()
		{
			var lista = await _dbContext.CLientes.ToListAsync();
			if (lista.Any())
			{
				foreach(var item in lista)
				{
					ListaCliente.Add(new ClienteDTO
					{
						IdCliente = item.IDCliente,
						Nome = item.Nome,
						Idade = item.Idade,
						Endereco = item.Endereco,
					});
				}
			}
		}

		private void ClienteMensagemRecebido(ClienteMensagem clienteMensagem)
		{
			var clienteDto = clienteMensagem.ClienteDto;
			if (clienteMensagem.EsCrear)
			{
				ListaCliente.Add(clienteDto);
			}
			else
			{
				var encontrado = ListaCliente.First(e => e.IdCliente == clienteDto.IdCliente);

				encontrado.Nome = clienteDto.Nome;
                encontrado.SobreNome = clienteDto.SobreNome;
                encontrado.Idade = clienteDto.Idade;
                encontrado.Endereco = clienteDto.Endereco;
            }
		}

		[RelayCommand]
		private async Task Criar()
		{
			var uri = $"{nameof(ClientePage)}?id=0";
			await Shell.Current.GoToAsync(uri);
		}

		[RelayCommand]
		private async Task Editar(ClienteDTO clienteDto)
		{
			var uri = $"{nameof(ClientePage)}?id={clienteDto.IdCliente}";
			await Shell.Current.GoToAsync(uri);
		}

        [RelayCommand]
        private async Task Excluir(ClienteDTO clienteDto)
        {
			bool answer = await Shell.Current.DisplayAlert("Mensagem", "Deseja excluir o cliente ?", "Sim", "Não");

			if (answer)
			{
				var encontrado = await _dbContext.CLientes.FirstAsync(e => e.IDCliente == clienteDto.IdCliente);

				_dbContext.CLientes.Remove(encontrado);
				await _dbContext.SaveChangesAsync();
				ListaCliente.Remove(clienteDto);
			}
        }
    }
}

