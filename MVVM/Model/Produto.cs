using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WpfApp3.MVVM.Model
{
    public class Produto : INotifyPropertyChanged, ICloneable, BaseNotifyPropertyChanged,  INotifyDataErrorInfo
    {
        

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        // Esta rotina é chamada cada vez que o valor da propridade 
        // for definida. Isso vai disparar um evento para notificar 
        // a WPF via data binding que algo mudou
        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        private long _id;
        private string _nome;
        private int _codigo;
        private double _valor;


        public Produto() { }
        
        public long Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }
        
        public string Nome
        {
            get { return _nome; }
            set
            {
                _nome = value;
                OnPropertyChanged("Nome");
                ValidateUserName();
            }
        }
        
        public int Codigo
        {
            get { return _codigo; }
            set
            {
                _codigo = value;
                OnPropertyChanged("Codigo");
            }
        }
        
        public double Valor
        {
            get { return _valor; }
            set
            {
                _valor = value;
                OnPropertyChanged("Valor");
            }
        }

        public bool HasErrors => _errorsByPropertyName.Any();
        

        private readonly Dictionary<string, List<string>> _errorsByPropertyName = new Dictionary<string, List<string>>();

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsByPropertyName.ContainsKey(propertyName) ?
           _errorsByPropertyName[propertyName] : null;
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void ValidateUserName()
        {
            ClearErrors(nameof(Nome));
            if (string.IsNullOrWhiteSpace(Nome))
                AddError(nameof(Nome), "Campo obrigatório");
            if (Nome == null || Nome?.Length < 3)
                AddError(nameof(Nome), "Mínimo de 3 catacteres");
        }

        private void AddError(string propertyName, string error)
        {
            if (!_errorsByPropertyName.ContainsKey(propertyName))
                _errorsByPropertyName[propertyName] = new List<string>();

            if (!_errorsByPropertyName[propertyName].Contains(error))
            {
                _errorsByPropertyName[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        private void ClearErrors(string propertyName)
        {
            if (_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }
    }
    }
