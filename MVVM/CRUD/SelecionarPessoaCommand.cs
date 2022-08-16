﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using WpfApp3.core;
using WpfApp3.MVVM.Model;
using WpfApp3.MVVM.ViewModel;

namespace WpfApp3.MVVM.CRUD
{
    class SelecionarPessoaCommad : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            var viewModel = parameter as CadastroPessoaViewModel;
            return viewModel != null && viewModel.PessoasSelecionado != null;
        }

        public override void Execute(object parameter)
        {
            var viewModel = (CadastroPessoaViewModel)parameter;

            var clonePessoa = (Model.Pessoa)viewModel.PessoasSelecionado.Clone();

            IEnumerable<Pedido> pedidos = from pedido in viewModel.TodosPedidos
                                          where pedido.Pessoa.Id == clonePessoa.Id
                                            select pedido;

            viewModel.PedidosFiltrados.Clear();
            foreach (Pedido elm in pedidos)
            {
                viewModel.PedidosFiltrados.Add(elm);
            }         

        }
    }
}
