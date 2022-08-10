using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        public void salvar(object sender, RoutedEventArgs e)
        {   
            Produto produto = new Produto();
            
            produto.Nome = nomeProduto.Text;
            produto.Id =1;
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
    }
}
