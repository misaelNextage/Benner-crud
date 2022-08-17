using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp3.MVVM.Model;
using System.IO;
using System.Text.Json;
using WpfApp3.MVVM.ViewModel;

namespace WpfApp3.MVVM.View
{
    /// <summary>
    /// Interação lógica para VenderView.xam
    /// </summary>
    public partial class CadastrarPedido : UserControl
    {
        public CadastrarPedido()
        {
            InitializeComponent();
            DataContext = new ViewModel.PedidoViewModel();
            pedidoEmCriacao = new Pedido();
            datagridNovoPedidos.ItemsSource = this.pedidoEmCriacao.ItemsPedido;
        }
        public Pedido pedidoEmCriacao;

        public void salvar(object sender, RoutedEventArgs e)
        {

            Pedido pedido = new Pedido();

            List<Pedido> source = new List<Pedido>();

            using (System.IO.StreamReader r = new System.IO.StreamReader("pedido.json"))
            {
                string json = r.ReadToEnd();
                source = JsonSerializer.Deserialize<List<Pedido>>(json);
            }

            source.Add(pedido);

            string jsonString = JsonSerializer.Serialize(source, new JsonSerializerOptions() { WriteIndented = true });
            using (StreamWriter outputFile = new StreamWriter("pedido.json"))
            {
                outputFile.WriteLine(jsonString);
            }
        }
        public void editar(object sender, RoutedEventArgs e)
        {
            string teste = "teste";

        }

        private void DatagridPedidos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count > 0)
            {
                int index = datagridNovoPedidos.SelectedIndex;
                object a = e.Source;

                object b = e.AddedItems;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PessoaViewModel param = new PessoaViewModel();
            Window1 janela = new Window1(param);
            janela.ShowDialog();
            pedidoEmCriacao.Pessoa = param.PessoaSelecionada;
            nomePessoa.Text = pedidoEmCriacao.Pessoa.Nome;
            idPessoa.Text = pedidoEmCriacao.Pessoa.Id.ToString();
            cpfPessoa.Text = pedidoEmCriacao.Pessoa.Cpf;
            enderecoPessoa.Text = pedidoEmCriacao.Pessoa.Endereco;
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            if (pedidoEmCriacao.Pessoa == null)
            {
                System.Windows.MessageBox.Show("Selecione o cliente primeiro Boy!!!", "Cliente obrigatório", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Yes, MessageBoxOptions.ServiceNotification);
                return;
            }
            CatalogoProdutos cp = new CatalogoProdutos(pedidoEmCriacao);
            cp.ShowDialog();
            datagridNovoPedidos.ItemsSource = pedidoEmCriacao.ItemsPedido;
            totalDoPedido.Content = pedidoEmCriacao.ValorTotal;


        }
    }
}

