using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EPSILab.Saturn.ViewModel.Objets
{
    /// <summary>
    ///     Classe implémentant l'interface INotifyPropertyChanged afin de modifier l'interface à chaud
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}