using System.IO;
using System.Linq;
using System.Text.Json;
using WpfApp3.core;

namespace WpfApp3.MVVM.ViewModel
{
    public class DeletarPedidoCommand : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            var viewModel = parameter as ProdutoViewModel;
            return viewModel != null && viewModel.ProdutoSelecionado != null;
        }

        public override void Execute(object parameter)
        {
            var viewModel = (ProdutoViewModel)parameter;
            viewModel.Produtos.Remove(viewModel.ProdutoSelecionado);
            viewModel.ProdutoSelecionado = viewModel.Produtos.FirstOrDefault();

            string jsonString = JsonSerializer.Serialize(viewModel.Produtos, new JsonSerializerOptions() { WriteIndented = true });
            using (StreamWriter outputFile = new StreamWriter("pedido.json"))
            {
                outputFile.WriteLine(jsonString);
            }
        }
    }
}
