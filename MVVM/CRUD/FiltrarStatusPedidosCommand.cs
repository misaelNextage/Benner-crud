using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using WpfApp3.core;
using WpfApp3.MVVM.Model;
using WpfApp3.MVVM.ViewModel;
using System.Windows.Controls;
using WpfApp3.MVVM.View;

namespace WpfApp3.MVVM.CRUD
{
    class FiltrarStatusPedidosCommand : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            var viewModel = parameter as CadastroPessoaViewModel;
            return viewModel != null && viewModel.PessoasSelecionado != null;
        }

        public override void Execute(object parameter)
        {            
            var viewModel = (CadastroPessoaViewModel)parameter;

            viewModel.PedidosFiltrados.Clear();
            foreach (Pedido pedido in viewModel.TodosPedidos)
            {
                if (pedido.StatusPedido.Equals(CadastrarPessoa.nomeBotaoFiltro))
                {
                    viewModel.PedidosFiltrados.Add(pedido);
                }

            }         

        }
    }
}
