using System;
using System.ComponentModel;

namespace ObservableTest.Model
{
    public class UsuarioModel : INotifyPropertyChanged
    {
        public string Nome
        {
            get
            {
                return nome;
            }
            set
            {
                nome = value;
                OnPropertyChanged("Nome");
            }
        }
        private string nome;
        
        public object AvatarResource
        {
            get
            {
                return avatarResource;
            }
            set
            {
                avatarResource = value;
                OnPropertyChanged("AvatarResource");
            }
        }
        private object avatarResource;

        public Guid UID { get; set; }//temp

        #region Construtor

        public UsuarioModel(string nome, object avatarResource)
        {
            Nome = nome;
            AvatarResource = avatarResource;
            UID = Guid.NewGuid();//temp
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); }
        
        #endregion
    }
}