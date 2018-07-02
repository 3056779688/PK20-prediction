using Microsoft.Win32;
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

using System.Drawing;
using System.Windows.Interop;

namespace 拯救小姐姐
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        List<List<int>> numbers = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Click Event

        //open PK10 picture and recognize the numbers by baidu api
        private void Open_Picture(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.CheckFileExists = true;//检查文件是否存在
            openFile.CheckPathExists = true;//检查路径是否存在
            

            if (openFile.ShowDialog() == true)
            {
                String filename = openFile.FileName;
                //GeneralBasicDemo(filename);
                List<List<int>> numbers =  GetNumbersByPictureColor(filename);
                this.numbers = numbers;

                 //WriteToFile(numbers);
            }

        }

        //open PK10 txt and record the numbers
        private void Open_Txt(object sender, RoutedEventArgs e)
        {

        }


        #endregion

        
    }
}
