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

namespace S201
{
    /// <summary>
    /// Logique d'interaction pour GererClient.xaml
    /// </summary>
    public partial class GererClient : UserControl
    {
        public GererClient()
        {
           /* InitializeComponent();*/
            
        }

        private void ChargerClients()
        {
            try
            {
                Client client = new Client();
                
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}

