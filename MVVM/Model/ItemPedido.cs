using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WpfApp3.MVVM.Model
{
    public class ItemPedido : INotifyPropertyChanged, ICloneable, BaseNotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public double Valor { get; set; }

        [Required]
        public Produto Produto { get; set; }

        public ItemPedido() { }
    }
}
