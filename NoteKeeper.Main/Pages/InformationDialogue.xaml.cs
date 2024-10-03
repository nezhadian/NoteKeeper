using DORVPN.ExtendedControls.Navigation;
using NoteKeeper.Main.ViewModels;
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

namespace NoteKeeper.Main.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class InformationDialogue : DialogPage
    {
        public InformationDialogue()
        {
            InitializeComponent();
        }

        private void UseKey_Click(object sender, RoutedEventArgs e)
        {
            var bytes = new byte[SafeStorageViewModel.FullKeySize];

            if (!Convert.TryFromBase64String(txtKey.Text, bytes, out int bw))
            {
                MessageBox.Show("Wrong Key");
                return;
            }
            if (bw != SafeStorageViewModel.FullKeySize)
            {
                MessageBox.Show("Wrong Length");
                return;

            }



            MainViewModel.Default.storage.Key = txtKey.Text;
            MainViewModel.Default.notes.LoadItems();
            PageContainer.GoBack();

        }

        private void GenerateKey_Click(object sender, RoutedEventArgs e)
        {
            txtKey.Text = SafeStorageViewModel.GenerateKeyAndIv();
        }
    }
}
