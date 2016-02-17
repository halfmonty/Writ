using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Documents;

namespace Writ.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

        }

        private FlowDocument _doc;
        public FlowDocument Doc
        {
            get { return _doc; }
            set
            {
                _doc = value;
                RaisePropertyChanged("Doc");
            }
        }

        private string p_DocumentXaml;
        public string DocumentXaml
        {
            get { return p_DocumentXaml; }

            set
            {
                p_DocumentXaml = value;
                RaisePropertyChanged("DocumentXaml");
            }
        }


        private static string _openDocument;
        public string OpenDocument
        {
            get { return _openDocument; }
            set
            {
                _openDocument = value;
                RaisePropertyChanged("OpenDocument");
            }
        }
        
    }
}