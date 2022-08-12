using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using WpfApp3.MVVM.Model;

namespace WpfApp3.MVVM.View
{
    /// <summary>
    /// Interação lógica para CadastroProduto.xam
    /// </summary>
    public partial class CadastroProduto : UserControl
    {
        public CadastroProduto()
        {
            InitializeComponent();
            DataContext = new ViewModel.ProdutoViewModel();
        }

        public void Teste(object produto)
        {
            var a = 0;
            //Button_Click(produto);
        }
        public void salvar(object sender, RoutedEventArgs e)
        {
            Produto produto = new Produto();

            produto.Nome = nomeProduto.Text;
            produto.Id = 1;
            produto.Codigo = int.Parse(codigoProduto.Text);
            produto.Valor = double.Parse(valorProduto.Text);


            List<Produto> source = new List<Produto>();

            using (StreamReader r = new StreamReader("produto.json"))
            {
                string json = r.ReadToEnd();
                source = JsonSerializer.Deserialize<List<Produto>>(json);
            }

            source.Add(produto);

            string jsonString = JsonSerializer.Serialize(source, new JsonSerializerOptions() { WriteIndented = true });
            using (StreamWriter outputFile = new StreamWriter("produto.json"))
            {
                outputFile.WriteLine(jsonString);
                outputFile.Close();
            }
        }

        private void NomeProduto_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(nomeProduto.Text) && nomeProduto.Text.Length > 3)
                Cadastrar.IsEnabled = true;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var t = sender;
            nomeProduto.Text = "";
            valorProduto.Text = "";
            codigoProduto.Text = "";

            //View
        }
    }
}
