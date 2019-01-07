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
    /// Interaction logic for VerifyDisjoint.xaml
    /// </summary>
    public partial class VerifyDisjoint : Page
    {
        public VerifyDisjoint()
        {
            InitializeComponent();
        }

        private void verifyDisjoint_Click(object sender, RoutedEventArgs e)
        {
            if(!Grammar.isDisjointVerified)
            {
                if(Grammar.verifyDisjoint())
                {
                    Grammar.isGrammarDisjoint = true;
                    _disjointTxtb.Text = "The sets are disjoint, you can go on with generating the matrix!";
                }
                else
                {
                    Grammar.isGrammarDisjoint = false;
                    _disjointTxtb.Text = "The sets are not disjoint! Please reset and load another Grammar!";
                }
            }
        }
    }
}
