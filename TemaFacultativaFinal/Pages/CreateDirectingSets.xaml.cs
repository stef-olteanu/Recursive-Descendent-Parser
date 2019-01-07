using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TemaFacultativaFinal
{
    /// <summary>
    /// Interaction logic for CreateDirectingSets.xaml
    /// </summary>
    public partial class CreateDirectingSets : Page
    {
        public CreateDirectingSets()
        {
            InitializeComponent();
        }

        private void _createSets_Click(object sender, RoutedEventArgs e)
        {
            if (!Grammar.areSetsCreated)
                Grammar.directingSets();
           
                _setsTxtb.Text = Grammar.showDirectingSet();
        }
    }
}
