using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using TesteDPlus.DataAccess;
using TesteDPlus.Db;
using TesteDPlus.DTOs;
using TesteDPlus.Model;

namespace TesteDPlus.ViewModels
{
    public partial class ClienteViewModel : ObservableObject, IQueryAttributable
    {
        private readonly ClienteDbContext _dbContext;

        [ObservableProperty]
        private ClienteDTO clienteDto = new ClienteDTO();

        [ObservableProperty]
        private string tituloPagina;

        private int IDCliente;

        [ObservableProperty]
        private bool loadingIsVisible = false;

        public ClienteViewModel(ClienteDbContext context)
        {
            _dbContext = context;
             
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var id = int.Parse(query["id"].ToString());
            IDCliente = id;
            if(IDCliente == 0)
            {
                TituloPagina = "Novo Cliente";
            }
            else
            {
                TituloPagina = "Editar cliente";
                LoadingIsVisible = true;
                await Task.Run(async () =>
                {
                    var encontrado = await _dbContext.CLientes.FirstAsync(e => e.IDCliente == IDCliente);
                    ClienteDto.IdCliente = encontrado.IDCliente;
                    ClienteDto.Idade = encontrado.Idade;
                    ClienteDto.Nome = encontrado.Nome;
                    ClienteDto.SobreNome = encontrado.SobreNome;
                    ClienteDto.Endereco = encontrado.Endereco;

                    MainThread.BeginInvokeOnMainThread(() => { LoadingIsVisible = false; });
                });
            }
        }

        [RelayCommand]
        private async Task Guardar()
        {
            LoadingIsVisible = true;
            ClienteMensagem mensagem = new ClienteMensagem();
            await Task.Run(async () =>
            {
                if (IDCliente == 0)
                {
                    var tbCliente = new Cliente
                    {
                        Nome = ClienteDto.Nome,
                        SobreNome = ClienteDto.SobreNome,
                        Idade = ClienteDto.Idade,
                        Endereco = ClienteDto.Endereco,
                    };
                    _dbContext.CLientes.Add(tbCliente);
                    await _dbContext.SaveChangesAsync();

                    ClienteDto.IdCliente = tbCliente.IDCliente;

                    mensagem = new ClienteMensagem()
                    {
                        EsCrear = true,
                        ClienteDto = ClienteDto
                    };
                }
                else
                {
                    var encontrado = await _dbContext.CLientes.FirstAsync(e => e.IDCliente == IDCliente);
                    encontrado.Nome = ClienteDto.Nome;
                    encontrado.Idade = ClienteDto.Idade;
                    encontrado.SobreNome = ClienteDto.SobreNome;
                    encontrado.Endereco = ClienteDto.Endereco;

                    await _dbContext.SaveChangesAsync();

                    mensagem = new ClienteMensagem()
                    {
                        EsCrear = true,
                        ClienteDto = ClienteDto
                    };
                }

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    LoadingIsVisible = false;
                    WeakReferenceMessenger.Default.Send(new ClienteMensageria(mensagem));
                    await Shell.Current.Navigation.PopAsync();
                });
            });
        }
    }
}

