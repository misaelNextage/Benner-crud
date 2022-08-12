using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using WpfApp3.MVVM.crud;
using WpfApp3.MVVM.Model;

namespace WpfApp3.MVVM.ViewModel
{
    class ProdutoViewModel : ObservableCollection<Produto>
    {
        private Produto _produtoSelecionado;

        public NovoProduto Novo { get; private set; } = new NovoProduto();

        public DeletarProduto Deletar { get; private set; } = new DeletarProduto();

        public EditarProduto Editar { get; private set; } = new EditarProduto();

        private Produto _produtoEdit = new Produto();

        public Produto Produto { get; internal set; }
        
       
        public ObservableCollection<Produto> Produtos { get; private set; }       

        public Produto ProdutoSelecionado
        {
            get { return _produtoSelecionado; }
            set
            {
                _produtoSelecionado = value;
                Editar.RaiseCanExecuteChanged();
            }
        }
                
        public Produto ProdutoEdit
        {
            get { return _produtoEdit; }
            set {_produtoEdit = value; }
        }

        public ProdutoViewModel()
        {
            Produtos = new ObservableCollection<Produto>();
            PreparaProdutoCollection();
        }

        public void PreparaProdutoCollection()
        {
            List<Produto> source = new List<Produto>();

            using (StreamReader r = new StreamReader("produto.json"))
            {
                string json = r.ReadToEnd();
                source = JsonSerializer.Deserialize<List<Produto>>(json);
            }

            source.ForEach(p =>
            {
                Produtos.Add(p);
            });

            if (Produtos.Count > 0)
            {
                ProdutoSelecionado = Produtos.FirstOrDefault();
            }
        }
        public bool Edit = false;
    }
}