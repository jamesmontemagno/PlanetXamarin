using System.ComponentModel;

namespace CrossPlatformInVS.Portable.Helpers
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// When a property is changed notify the UI (WP/WinStore)
        /// </summary>
        /// <param name="propName"></param>
        protected virtual void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
