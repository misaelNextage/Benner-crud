using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using WpfApp3.core;
using WpfApp3.MVVM.crud;
using WpfApp3.MVVM.CRUD;
using WpfApp3.MVVM.Model;

namespace WpfApp3.MVVM.ViewModel
{
    class CadastroPessoaViewModel : ObservableCollection<Pessoa>
    {
        public Pessoa Pessoa { get; internal set; }
        public ObservableCollection<Pessoa> Pessoas { get; private set; }
        public DeletarPessoa Deletar { get; private set; } = new DeletarPessoa();

        public EditarPessoa Editar { get; private set; } = new EditarPessoa();

        public PesquisaPessoa Pesquisa { get; private set; } = new PesquisaPessoa();

        private string _pesquisaText = "";
        public string PesquisaText
        {
            get { return _pesquisaText; }
            set { _pesquisaText = value; }
        }

        private Pessoa _pessoaSelecionado;
        public Pessoa PessoasSelecionado
        {
            get { return _pessoaSelecionado; }
            set { _pessoaSelecionado = value; }
        }

        private Pessoa _pessoaEdit = new Pessoa();
        public Pessoa PessoaEdit
        {
            get { return _pessoaEdit; }
            set { _pessoaEdit = value; }
        }


        public NovaPessoa Novo { get; private set; } = new NovaPessoa();

        public CadastroPessoaViewModel()
        {
            Pessoas = new ObservableCollection<Pessoa>();
            PreparaPessoaCollection();
        }

        public void PreparaPessoaCollection()
        {
            List<Pessoa> source = new List<Pessoa>();

            using (StreamReader r = new StreamReader("pessoa.json"))
            {
                string json = r.ReadToEnd();
                source = JsonSerializer.Deserialize<List<Pessoa>>(json);
            }

            source.ForEach(p =>
            {
                Pessoas.Add(p);
            });

            if (Pessoas.Count > 0)
            {
                PessoasSelecionado = Pessoas.FirstOrDefault();
            }
        }
        public bool Edicao = false;
    }

    class PesquisaPessoa : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            return parameter is CadastroPessoaViewModel;
        }

        public override void Execute(object parameter)
        {
            var viewModel = (CadastroPessoaViewModel)parameter;

            string text = viewModel.PesquisaText;

            List<Pessoa> json = new List<Pessoa>();

            using (StreamReader r = new StreamReader("pessoa.json"))
            {
                string jsonStr = r.ReadToEnd();
                json = JsonSerializer.Deserialize<List<Pessoa>>(jsonStr);
            }
            viewModel.Pessoas.Clear();

            json.ForEach(p =>
            {
                if (p.Nome != null && p.Nome.ToLower().Contains(text.ToLower()))
                {
                    viewModel.Pessoas.Add(p);
                }
            });

            if (viewModel.Pessoas.Count > 0)
            {
                viewModel.PessoasSelecionado = viewModel.Pessoas.FirstOrDefault();
            }
        }
    }

}
