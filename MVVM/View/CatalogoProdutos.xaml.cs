using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp3.MVVM.Model;

namespace WpfApp3.MVVM.View
{
    public partial class CatalogoProdutos : Window
    {
        public CatalogoProdutos(Pedido pedido)
        {
            InitializeComponent();
            this.carregarProdutos();
            datagridProdutos.ItemsSource = produtosDisponiveis;
            Quantidade.Text = "0";
            this.param = pedido;
        }
        Pedido param;
        List<Produto> produtosDisponiveis = new List<Produto>();
        List<ItemPedido> produtosAdicionados = new List<ItemPedido>();
        int numItens = 0;
        double totalGeral;
        private void carregarProdutos()
        {
            List<Produto> source = new List<Produto>();

            using (System.IO.StreamReader r = new System.IO.StreamReader("produto.json"))
            {
                string json = r.ReadToEnd();
                source = JsonSerializer.Deserialize<List<Produto>>(json);
            }
            source.ForEach(p =>
            {
                produtosDisponiveis.Add(p);
            });
        }
        private void selecionarProduto(object sender, RoutedEventArgs e)
        {
            int a = 0;
            Quantidade.Text = "0";
            try
            {
                Produto produtoChange = (Produto)this.datagridProdutos.SelectedItem;
                nomeProduto.Text = produtoChange.Nome;
                ValorUnitario.Text = "R$ " + produtoChange.Valor.ToString();
            }
            catch (Exception except)
            {
                return;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.datagridProdutos.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Selecione um produto antes de adicionar! Ativo", "Produto obrigatório", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Yes, MessageBoxOptions.ServiceNotification);
                return;
            }

            if (Quantidade.Text == "0")
            {
                System.Windows.MessageBox.Show("O Produto precisa de uma quantidade", "Quantidade obrigatória", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Yes, MessageBoxOptions.ServiceNotification);
                return;
            }

            Produto produtoChange = (Produto)this.datagridProdutos.SelectedItem;
            var valorDesseItem = produtoChange.Valor * Convert.ToDouble(Quantidade.Text);
            numItens += Convert.ToInt32(Quantidade.Text);
            totalGeral += valorDesseItem;
            totalPedido.Text = totalGeral.ToString();
            ItemPedido itemNovo = new ItemPedido();
            itemNovo.Quantidade = Convert.ToInt32(Quantidade.Text);
            itemNovo.Produto = produtoChange;
            itemNovo.Valor = valorDesseItem;
            produtosAdicionados.Add(itemNovo);
            totalItens.Text = numItens.ToString();
            Quantidade.Text = "0";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.param.ItemsPedido = produtosAdicionados;
            this.param.ValorTotal = Convert.ToDouble(totalGeral);
            this.Close();
        }
    }
}
