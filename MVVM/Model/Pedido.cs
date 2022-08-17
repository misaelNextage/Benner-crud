using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WpfApp3.MVVM.Model
{
    public class Pedido
    {
        public Pedido() { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        [Key]
        public long Id { get; set; }

        [Required]
        public Pessoa Pessoa { get; set; }

        [Required]
        public List<ItemPedido> ItemsPedido { get; set; }


        public double ValorTotal { get; set; }

        public DateTime DataVenda { get; set; }

        [Required]
        public FormaPagamentoEnum FormaPagamento { get; set; }

        public StatusEnum Status { get; set; }
        public object StatusPedido { get; internal set; }

        public enum FormaPagamentoEnum
        {
            Dinheiro,
            Cartao,
            Boleto
        }

        public enum StatusEnum
        {
            Pendente,
            Pago,
            Enviado,
            Recebido
        }
    }
}