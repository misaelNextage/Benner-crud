using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WpfApp3.core;
using WpfApp3.MVVM.ViewModel;

namespace WpfApp3.MVVM.CRUD
{
    class DeletarPessoa : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            var viewModel = parameter as CadastroPessoaViewModel;
            return viewModel != null && viewModel.PessoasSelecionado != null;
        }

        public override void Execute(object parameter)
        {
            var viewModel = (CadastroPessoaViewModel)parameter;
            viewModel.Pessoas.Remove(viewModel.PessoasSelecionado);
            viewModel.PessoasSelecionado = viewModel.Pessoas.FirstOrDefault();

            string jsonString = JsonSerializer.Serialize(viewModel.Pessoas, new JsonSerializerOptions() { WriteIndented = true });
            using (StreamWriter outputFile = new StreamWriter("pessoa.json"))
            {
                outputFile.WriteLine(jsonString);
            }
        }
    }
}
