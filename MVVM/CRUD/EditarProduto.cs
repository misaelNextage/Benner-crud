using WpfApp3.core;
using WpfApp3.MVVM.ViewModel;

namespace WpfApp3.MVVM.crud
{
    class EditarProduto : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            var viewModel = parameter as ProdutoViewModel;
            return viewModel != null && viewModel.ProdutoSelecionado != null;
        }

        public override void Execute(object parameter)
        {
            var viewModel = new ProdutoViewModel();
            viewModel = (ProdutoViewModel)parameter;

            var cloneProduto = (Model.Produto)viewModel.ProdutoSelecionado.Clone();

            viewModel.ProdutoEdit.Id = cloneProduto.Id;
            viewModel.ProdutoEdit.Nome = cloneProduto.Nome;
            viewModel.ProdutoEdit.Codigo = cloneProduto.Codigo;
            viewModel.ProdutoEdit.Valor = cloneProduto.Valor;

            viewModel.Produtos.Clear();
            viewModel.PreparaProdutoCollection();
        }
    }
}
