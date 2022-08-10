using System;
using System.IO;
using System.Linq;
using System.Text.Json;
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
            pessoa.Id = maxId + 1;
            pessoa.Nome = viewModel.PessoaEdit.Nome;
            pessoa.Cpf = viewModel.PessoaEdit.Cpf;
            pessoa.Endereco = viewModel.PessoaEdit.Endereco;

            viewModel.Pessoas.Add(pessoa);
            viewModel.PessoasSelecionado = pessoa;

            string jsonString = JsonSerializer.Serialize(viewModel.Pessoas, new JsonSerializerOptions() { WriteIndented = true });
            using (StreamWriter outputFile = new StreamWriter("pessoa.json"))
            {
                outputFile.WriteLine(jsonString);
            }
        }
    }
}
