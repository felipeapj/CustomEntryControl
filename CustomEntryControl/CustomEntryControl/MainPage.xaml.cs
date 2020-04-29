using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CustomEntryControl
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ViewModelM vM = new ViewModelM();
            BindingContext = vM;
            CustomEntry.Completed = Entry_Completed;
        }

        public void Entry_Completed(object sender, EventArgs e)
        {
            Console.WriteLine("AEEEEEEEEE");
        }
    }
}
