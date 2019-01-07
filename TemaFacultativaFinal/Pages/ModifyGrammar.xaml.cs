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
    /// Interaction logic for ModifyGrammar.xaml
    /// </summary>
    public partial class ModifyGrammar : Page
    {
        public ModifyGrammar()
        {
            InitializeComponent();
        }

        private void modifyGrammar_Click(object sender, RoutedEventArgs e)
        {
            if (!Grammar.isModified)
                Grammar.ModifyGrammar();
             _modifyTxtb.Text = Grammar.ShowGrammar();
        }
    }
}
