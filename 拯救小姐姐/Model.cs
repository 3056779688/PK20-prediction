using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace 拯救小姐姐
{
    public partial class MainWindow : Window
    {
        public class Rank
        {
            public int number;
            public double probality;
            public Rank(int number,double probality)
            {
                this.number = number;
                this.probality = probality;
            }
        }


        //Model 1 大数定律
        public List<List<Rank>> Model1(List<List<int>> numbers)
        {
            List<List<Rank>> result = new List<List<Rank>>();
               int length = numbers.Count;
            for(int j=0;j<10;++j)
            {
                int sum = 10 * length;
                List<Rank> rank = new List<Rank>();
                for(int k=0;k<10;++k)
                {
                    rank.Add(new Rank(k, length));
                }
                for(int i=0;i<length;++i)
                {
                    rank[numbers[i][j]].probality--;
                    sum--;
                }
                for (int k = 0; k < 10; ++k)
                {
                    rank[k].probality /= sum;
                }
                Insert_Sort(rank);
                result.Add(rank);
            }
            return result;
        }

        private void Mode1_ALL_Rank(object sender, RoutedEventArgs e)
        {
            try
            {
                string msg = GetInformation();
                sendmessage(msg);
            }
            catch
            {
                Console.WriteLine("error");
            }

            if (numbers != null)
            {
                List<List<Rank>> rank = Model1(numbers);
                label.Content = "";
                for(int i=0;i<10;++i)
                {
                    label.Content += "第(" + (i + 1) + ")位的推荐\n";
                    int j = 0;
                    for ( j=0;j<6;++j)
                    {
                        label.Content += rank[i][j].number.ToString() + "(" + rank[i][j].probality * 100 + "%) ";
                    }

                    while(j<10&&rank[i][j].probality==rank[i][j-1].probality)
                    {
                        label.Content += rank[i][j].number.ToString() + "(" + rank[i][j].probality * 100 + "%) ";
                        j++;
                    }
                    label.Content += "\n";
                }
            }
        }

        private void Model1_Best_Rank(object sender, RoutedEventArgs e)
        {
            if (numbers != null)
            {
                List<List<Rank>> rank = Model1(numbers);
                label.Content = "";
                double maxvalue = double.MinValue;
                int maxindex = -1; int j = 0;
                for (int i = 0; i < 10; ++i)
                {
                   // label.Content += "第(" + (i + 1) + ")位的推荐\n";
                 
                    double tmpvalue = 0;
                    for (j = 0; j < 6; ++j)
                    {
                        tmpvalue += rank[i][j].probality;
                    }
                    if(maxvalue<tmpvalue)
                    {
                        maxvalue = tmpvalue ;
                        maxindex = i;
                    }
                   // label.Content += "\n";
                }

                label.Content += "最优位置推荐，第(" + (maxindex + 1) + ")位\n";
               
                for (j = 0; j < 6; ++j)
                {
                    label.Content += rank[maxindex][j].number.ToString() + "(" + rank[maxindex][j].probality * 100 + "%) ";
                }

                while (j < 10 && rank[maxindex][j].probality == rank[maxindex][j - 1].probality)
                {
                    label.Content += rank[maxindex][j].number.ToString() + "(" + rank[maxindex][j].probality * 100 + "%) ";
                    j++;
                }


                label.Content += "\n";

            }
        }

        public void Insert_Sort(List<Rank> array)
        {
            int first = 0;
            int last = array.Count;
            int i,j;
            Rank temp;
            for (i = first + 1; i < last; i++)
            {
                temp = array[i];
                j = i - 1;
               
                while ((j >= 0) && (array[j].probality < temp.probality))
                {
                    array[j + 1] = array[j];
                    j--;
                }
                //存在大于temp的数
                if (j != i - 1)
                { array[j + 1] = temp; }
            }

        }
    }

}
