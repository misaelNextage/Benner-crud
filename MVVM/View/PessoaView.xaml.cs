using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using WpfApp3.MVVM.Model;

namespace WpfApp3.MVVM.View
{
    /// <summary>
    /// Interaction logic for CadastrarPessoa.xaml
    /// </summary>
    public partial class CadastrarPessoa : UserControl
    {
        public CadastrarPessoa()
        {
            InitializeComponent();
        }

        public void salvar(object sender, RoutedEventArgs e)
        {
            /*Pessoa pessoa = new Pessoa();
            pessoa.Nome = Nome.Text;
            pessoa.Id = 1;
            pessoa.Cpf = Cpf.Text;
            pessoa.Endereco = Endereco.Text;*/


            List<Pessoa> source = new List<Pessoa>();

            using (StreamReader r = new StreamReader("pessoa.json"))
            {
                string json = r.ReadToEnd();
                source = JsonSerializer.Deserialize<List<Pessoa>>(json);
            }

            /*source.Add(pessoa);*/

            string jsonString = JsonSerializer.Serialize(source, new JsonSerializerOptions() { WriteIndented = true });
            using (StreamWriter outputFile = new StreamWriter("pessoa.json"))
            {
                outputFile.WriteLine(jsonString);
                outputFile.Close();
            }
        }

        private void Pesquisar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Cpf_LostFocus(object sender, RoutedEventArgs e)
        {
            string cpf = Cpf.Text;
            Cpf.MaxLength = 14;
            try
            {
                if(Cpf.Text != null && Cpf.Text != "")
                {
                    Cpf.Text = Cpf.Text.Length == 11 ? System.Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00"): cpf;
                }
                
            }
            catch { Cpf.Text = cpf; }
            
        }

        private void Cpf_GotFocus(object sender, RoutedEventArgs e)
        {
            Cpf.Text = System.Text.RegularExpressions.Regex.Replace(Cpf.Text, "[^0-9]","");
        }

        private void Cpf_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var cpfDigitado = sender as TextBox;
            Cpf.MaxLength = 11;
            e.Handled = System.Text.RegularExpressions.Regex.IsMatch(e.Text, "[^0-9]+"); //permite só números
        }

        private void DatagridPessoas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
