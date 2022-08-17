using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using WpfApp3.core;
using WpfApp3.MVVM.crud;
using WpfApp3.MVVM.Model;

namespace WpfApp3.MVVM.ViewModel
{
    class ModalClienteViewModel : ObservableCollection<Pessoa>
    {
        private Pessoa _modalClienteSelecionado;

        //public NovoProduto Novo { get; private set; } = new NovoProduto();

        //public DeletarProduto Deletar { get; private set; } = new DeletarProduto();

        //public EditarProduto Editar { get; private set; } = new EditarProduto();

        private Produto _produtoEdit = new Produto();

        public Produto Produto { get; internal set; }

        public ObservableCollection<Pessoa> Pessoas { get; private set; }

        public Pessoa ModalClienteSelecionado
        {
            get { return _modalClienteSelecionado; }
            set
            {
                _modalClienteSelecionado = value;
                //Editar.RaiseCanExecuteChanged();
            }
        }

        //public Pessoa ProdutoEdit
        //{
        //    get { return _produtoEdit; }
        //    set {_produtoEdit = value; }
        //}

        private string _pesquisaText = "";
        public string PesquisaText
        {
            get { return _pesquisaText; }
            set { _pesquisaText = value; }
        }

        public PesquisaCliente Pesquisa { get; private set; } = new PesquisaCliente();

        public ModalClienteViewModel()
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
                ModalClienteSelecionado = Pessoas.FirstOrDefault();
            }
        }
    }

    class PesquisaCliente : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            return parameter is ModalClienteViewModel;
        }

        public override void Execute(object parameter)
        {
            var viewModel = (ModalClienteViewModel)parameter;

            string text = viewModel.PesquisaText;

            List<Pessoa> json = new List<Pessoa>();

            using (StreamReader r = new StreamReader("produto.json"))
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
                viewModel.ModalClienteSelecionado = viewModel.Pessoas.FirstOrDefault();
            }
        }
    }
}