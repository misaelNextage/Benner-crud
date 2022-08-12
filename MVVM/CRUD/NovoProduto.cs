using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using WpfApp3.core;
using WpfApp3.MVVM.Model;

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

            if (viewModel.ProdutoEdit.Id != null && viewModel.ProdutoEdit.Id > 0)
            {
                var produto = new Model.Produto();

                int contador = 0;
                int posicao = -1;
                foreach (Produto produto2 in viewModel.Produtos)
                {
                    if (produto2.Id == viewModel.ProdutoEdit.Id)
                    {
                        posicao = contador;
                    }
                    contador++;
                }

                viewModel.Produtos[posicao] = viewModel.ProdutoEdit;

                viewModel.ProdutoSelecionado = produto;
            }
            else
            {
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

                if (produto.Nome.Length == 0 || produto.Valor <= 0 || produto.Codigo <= 0)

                    MessageBox.Show("Por favor, preencha todos os campos!");
                    
                else{
                viewModel.Produtos.Add(produto);
                viewModel.ProdutoSelecionado = produto;               
                }
            }

            string jsonString = JsonSerializer.Serialize(viewModel.Produtos, new JsonSerializerOptions() { WriteIndented = true });
            using (StreamWriter outputFile = new StreamWriter("produto.json"))
            {
                outputFile.WriteLine(jsonString);
            }
        }
    }
}
