﻿using ObservableTest.Data;
using SQLite;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ObservableTest.Model
{
    public class UsuarioModel : INotifyPropertyChanged
    {
        #region Propriedades

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Nome { get { return nome; } set { nome = value; OnPropertyChanged("Nome"); } }
        private string nome;

        public string Email { get { return email; } set { email = value; OnPropertyChanged("Email"); } }
        private string email;

        [Ignore]
        public string AvatarUrl { get { return avatarUrl;} set { avatarUrl = value;OnPropertyChanged("AvatarUrl"); }}
        private string avatarUrl;

        #endregion


        #region UsuarioModel Local Database

        public async Task<UsuarioModel> Get(int id)
        {
            var usuario = await App.Database.GetItemAsync(id);
            return usuario;
        }

        public async Task<int> Upsert(UsuarioModel usuario)
        {
            var id = await App.Database.SaveItemAsync(usuario);

            return id;
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); }
        
        #endregion
    }
}