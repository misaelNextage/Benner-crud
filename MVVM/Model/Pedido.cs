using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
       

        public Pedido() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }


        [Key]
        public long Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        [Required]
        public Pessoa Pessoa
        {
            get { return _pessoa; }
            set
            {
                _pessoa = value;
                OnPropertyChanged("Pessoa");
            }
        }

        [Required]
        public Double ValorTotal
        {
            get { return _valorTotal; }
            set
            {
                _valorTotal = value;
                OnPropertyChanged("ValorTotal");
            }
        }

    }
}