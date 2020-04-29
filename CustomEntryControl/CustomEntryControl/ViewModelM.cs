using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms.Internals;

namespace CustomEntryControl
{
    [Preserve(AllMembers = true)]
    public class ViewModelM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool isValid;
        public bool IsValid { get { return isValid; } set { isValid = value; OnPropertyChanged(); } }
        private string text;
        public string Text
        { 
            get { return text; } 
            set { 
                text = value; 
                OnPropertyChanged(); 
                if (!string.IsNullOrEmpty(value))
                    if ( value.Equals("ab"))
                        IsValid = true; 
                    else 
                        IsValid = false;
                else
                    IsValid = false;
            }
        }

    }
}
