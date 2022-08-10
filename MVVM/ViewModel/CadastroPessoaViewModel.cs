using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using WpfApp3.MVVM.CRUD;
using WpfApp3.MVVM.Model;

namespace WpfApp3.MVVM.ViewModel
{
    class CadastroPessoaViewModel : ObservableCollection<Pessoa>
    {
        public Pessoa Pessoa { get; internal set; }
        public ObservableCollection<Pessoa> Pessoas { get; private set; }

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
    }
}
