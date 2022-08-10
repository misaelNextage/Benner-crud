using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WpfApp3.core;
using WpfApp3.MVVM.Model;
using WpfApp3.MVVM.View;

namespace WpfApp3.MVVM.ViewModel
{
    class NovoProduto : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            return parameter is ProdutoViewModel;
        }

        public override void Execute(object parameter)
        {
            var viewModel = (ProdutoViewModel)parameter;
            var produto = new Model.Produto();
            long maxId = 0;
            if (viewModel.Produtos.Any())
            {
                maxId = viewModel.Produtos.Max(f => f.Id);
            }
            produto.Id = maxId + 1;
            produto.Nome = viewModel.ProdutoEdit.Nome;
            produto.Codigo = viewModel.ProdutoEdit.Codigo;
            produto.Valor = viewModel.ProdutoEdit.Valor;

            viewModel.Produtos.Add(produto);
            viewModel.ProdutoSelecionado = produto;

            string jsonString = JsonSerializer.Serialize(viewModel.Produtos, new JsonSerializerOptions() { WriteIndented = true });
            using (StreamWriter outputFile = new StreamWriter("produto.json"))
            {
                outputFile.WriteLine(jsonString);
            }
        }
    }
}
