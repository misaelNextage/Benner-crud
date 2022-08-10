using System.IO;
using System.Linq;
using System.Text.Json;
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

            if(viewModel.ProdutoEdit.Id != null)
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
            } else
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

                viewModel.Produtos.Add(produto);
                viewModel.ProdutoSelecionado = produto;
            }

                maxId = viewModel.Produtos.Max(f => f.Id);
            }
            if(viewModel.Edit == false)
            {
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
            else
            {
                string jsonString = JsonSerializer.Serialize(viewModel.Produtos, new JsonSerializerOptions() { WriteIndented = true });
                using (StreamWriter outputFile = new StreamWriter("produto.json"))
                {
                    outputFile.WriteLine(jsonString);
                }
            }
            
        }
        
    }
}
