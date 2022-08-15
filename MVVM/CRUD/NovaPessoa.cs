using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using WpfApp3.core;
using WpfApp3.MVVM.ViewModel;

namespace WpfApp3.MVVM.CRUD
{
    class NovaPessoa : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            return parameter is CadastroPessoaViewModel;
        }

        public override void Execute(object parameter)
        {
            var viewModel = (CadastroPessoaViewModel)parameter;
            var pessoa = new Model.Pessoa();
            long maxId = 0;
            if (viewModel.Pessoas.Any())
            {
                maxId = viewModel.Pessoas.Max(f => f.Id);
            }
            if(viewModel.Edicao == false) {
                    pessoa.Id = maxId + 1;
                    pessoa.Nome = viewModel.PessoaEdit.Nome;
                    pessoa.Cpf = viewModel.PessoaEdit.Cpf;
                    pessoa.Endereco = viewModel.PessoaEdit.Endereco;
                
                if (pessoa.Nome == null || pessoa.Cpf == null || pessoa.Endereco == null || pessoa.Nome == "" || pessoa.Cpf == "" || pessoa.Endereco == "")

                    MessageBox.Show("Todos os campos são obrigatórios!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Error);

                else if (!viewModel.PessoaEdit.Cpf.All(char.IsDigit))
                    MessageBox.Show("CPF precisa ser numérico!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    viewModel.Pessoas.Add(pessoa);
                    viewModel.PessoasSelecionado = pessoa;
                    viewModel.PessoaEdit.Id = 0;
                    viewModel.PessoaEdit.Nome = "";
                    viewModel.PessoaEdit.Endereco = "";
                    viewModel.PessoaEdit.Cpf = "";
                }

                string jsonString = JsonSerializer.Serialize(viewModel.Pessoas, new JsonSerializerOptions() { WriteIndented = true });
                using (StreamWriter outputFile = new StreamWriter("pessoa.json"))
                {
                    outputFile.WriteLine(jsonString);
                }               
            }
            
            else
            {
                string jsonString = JsonSerializer.Serialize(viewModel.Pessoas, new JsonSerializerOptions() { WriteIndented = true });
                using (StreamWriter outputFile = new StreamWriter("pessoa.json"))
                {
                    outputFile.WriteLine(jsonString);
                }
            }
        }
    }
}
