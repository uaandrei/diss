using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Chess.Game.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void NotifyPropertyChanged([CallerMemberName] string memberName = null)
        {
            if (memberName == null)
                throw new ArgumentNullException("memberName");

            var ev = PropertyChanged;
            if (ev != null)
            {
                ev(this, new PropertyChangedEventArgs(memberName));
            }
        }
    }
}
