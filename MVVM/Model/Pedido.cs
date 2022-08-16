using System;
using System.ComponentModel;

namespace WpfApp3.MVVM.Model
{
    class Pedido : INotifyPropertyChanged, ICloneable, BaseNotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }


        private long _id;
        private Pessoa _pessoa;
        private Double _valorTotal;
        private DateTime _dataPagamento;
        public enum status
        {
            Pendente,
            Pago,
            Enviado,
            Recebido
        }


        public enum pagamento
        {
            Dinheiro,
            Cartao,
            Boleto
        }

        public Pedido() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }


        public long Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public Pessoa Pessoa
        {
            get { return _pessoa; }
            set
            {
                _pessoa = value;
                OnPropertyChanged("Pessoa");
            }
        }

        public Double ValorTotal
        {
            get { return _valorTotal; }
            set
            {
                _valorTotal = value;
                OnPropertyChanged("ValorTotal");
            }
        }
        public DateTime DataPagamento
        {
            get { return _dataPagamento; }
            set
            {
                _dataPagamento = value;
                OnPropertyChanged("DataPagamento");
            }
        }
    }
}