using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace ObservableTest.Model
{
    public class PostModel : INotifyPropertyChanged
    {
        public string Legenda { get; set; }

        public UsuarioModel Usuario
        {
            get
            {
                return usuario;
            }
            set
            {
                usuario = value;
                OnPropertyChanged("Usuario");
            }
        }
        private UsuarioModel usuario;


        public string FotoUrl
        {
            get
            {
                return fotoUrl;
            }
            set
            {
                fotoUrl = value;
                OnPropertyChanged("FotoUrl");
            }
        }
        private string fotoUrl;


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); }
    }

    public class DebugHelper
    {
        public void Print<T>(IEnumerable<T> lista)
        {
            Debug.WriteLine(" ****** * * * * *  DEBUNG PRINT * * * ******** ");

            Type typeParameterType = typeof(T);

            foreach (var obj in lista)
            {
                PrintProperties(obj, 4);
                Debug.WriteLine("- ");
            }
        }

        public void PrintProperties(object obj, int indent)
        {
            if (obj == null) return;
            string indentString = new string(' ', indent);
            Type objType = obj.GetType();
            var properties = objType.GetRuntimeProperties();
            Debug.WriteLine("{0}*{1}", indentString, objType.Name);
            foreach (PropertyInfo property in properties)
            {
                object propValue = property.GetValue(obj, null);

                if (propValue.GetType() == typeof(string) || propValue.GetType() == typeof(int)
                    || propValue.GetType() == typeof(decimal) || propValue.GetType() == typeof(float)
                    || propValue.GetType() == typeof(byte) || propValue.GetType() == typeof(long)
                    || propValue.GetType() == typeof(char) || propValue.GetType() == typeof(bool)
                    || propValue.GetType() == typeof(short) || propValue.GetType() == typeof(sbyte)
                    || propValue.GetType() == typeof(uint) || propValue.GetType() == typeof(Guid))
                {
                    Debug.WriteLine("{0}*{1}: {2}", indentString, property.Name, propValue);
                }
                else
                {
                    PrintProperties(propValue, indent * 2);
                }
            }
        }
    }
}