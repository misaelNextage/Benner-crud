using WpfApp3.core;
using WpfApp3.MVVM.Model;
using WpfApp3.MVVM.View;
using WpfApp3.MVVM.ViewModel;

namespace WpfApp3.MVVM.CRUD
{
    public class FiltrarStatusPedidosCommand : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            var viewModel = parameter as PessoaViewModel;
            return viewModel != null && viewModel.PessoasSelecionado != null;
        }

        public override void Execute(object parameter)
        {
            var viewModel = (PessoaViewModel)parameter;

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