using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Diagnostics.Metrics;
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
using System.Windows.Shapes;

namespace pereplet
{

    /// <summary>
    /// Логика взаимодействия для Basket.xaml
    /// </summary>
    public partial class Basket : Window
    {
        int count = 0;
        int columns = 0;
        int row = 0;
        public TextBlock price;
        public TextBlock cou;
        public Button del;
        private List<TextBlock> items = new List<TextBlock>();
        private List<TextBlock> item = new List<TextBlock>();
        public Basket()
        {
            InitializeComponent();
            var contex = new AppDbContext();
            var q = contex.Korzina.Count();
            var l = contex.Korzina.Where(x => x.Id > 0).ToList();
            int ss = l.Sum(x => Convert.ToInt32(x.Count));


            var w = contex.Korzina.Where(x => x.Id > 0).ToList();
            while (count < q)
            {
                if (columns == 7)
                {
                    columns = 0;
                    row += 1;
                    
                }
                del = new Button();
                Image image = new Image();
                string a = w[count].Image.ToString();
                image.Source = new BitmapImage(new Uri($"{a}", UriKind.RelativeOrAbsolute));
                TextBlock textBlock = new TextBlock();
                textBlock.Text = w[count].Names;
                textBlock.TextWrapping = TextWrapping.Wrap;
                 price = new TextBlock();
               cou = new TextBlock();
                Button button = new Button();
                Button button1 = new Button();
                price.Text = (Convert.ToInt32(w[count].Price)* Convert.ToInt32(w[count].Count)).ToString() + " руб.";
                cou.Text= w[count].Count;
                button.Content = "-";
                button.Width = 35;
                button.Height = 35;
                button.Template = (ControlTemplate)Resources["овальная кнопка"];
                button.CommandParameter = a;
                button.Click += Button_Click;
                button1.Content = "+";
                button1.Width = 35;
                button1.Height = 35;
                button1.Template = (ControlTemplate)Resources["овальная кнопка"];
                button1.CommandParameter = a;
                button1.Click += Button1_Click;
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = new BitmapImage(new Uri(@"C:\Users\Anna\Downloads\pereplet-master\image\delet.png", UriKind.RelativeOrAbsolute));
                del.BorderBrush = null;
                del.Background =imageBrush ;
                del.Click += Del_Click;
                del.CommandParameter = a;
                counts.Text = ss.ToString();
                d.Template = (ControlTemplate)Resources["овальная кнопка"];
                del.Template = (ControlTemplate)Resources["овальная кнопка"];
                Grid.SetColumn(image, columns);
                Grid.SetRow(image, row);
                Grid.SetColumn(textBlock, columns+1);
                Grid.SetRow(textBlock, row );
                Grid.SetColumn(button, columns + 2);
                Grid.SetRow(button, row);
                Grid.SetColumn(cou, columns + 3);
                Grid.SetRow(cou, row);
                Grid.SetColumn(button1, columns + 4);
                Grid.SetRow(button1, row);
                Grid.SetColumn(price, columns+5);
                Grid.SetRow(price, row);
                Grid.SetColumn(del, columns + 6);
                Grid.SetRow(del, row);
                dd.Children.Add(image);
                dd.Children.Add(textBlock);
                dd.Children.Add(button);
                dd.Children.Add(button1);
                dd.Children.Add(price);
                dd.Children.Add(cou);
                dd.Children.Add(del);
                items.Add(cou);
                item.Add(price);
                row++;
                count++;
                int it = l.Sum(x => Convert.ToInt32(x.Itog));
                money.Text = it.ToString() + " руб.";
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            Button del = sender as Button;
            var context = new AppDbContext();
            string par = del.CommandParameter as string;

            var r = context.Korzina.Where(x => x.Image == par).ToList();
            context.Korzina.Remove(r[0]);
            context.SaveChanges();
            Basket basket = new Basket();
            basket.Show();
            this.Close();

        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Button button1 = sender as Button;
            var context = new AppDbContext();
            string par = button1.CommandParameter as string;

            var r = context.Korzina.Where(x => x.Image == par).ToList();
           
           


                string prices = (Convert.ToInt32(r[0].Count) + 1).ToString();
            var h = context.Korzina.Where(x => x.Id == r[0].Id).AsEnumerable().Select(x => { x.Count = prices; return x; });
                foreach (var x in h)
                {
                    context.Entry(x).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
            var h1 = context.Korzina.Where(x => x.Id == r[0].Id).AsEnumerable().Select(x => { x.Itog = (Convert.ToInt32(r[0].Count) * Convert.ToInt32(r[0].Price)).ToString(); return x; });
            foreach (var x in h1)
            {
                context.Entry(x).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            context.SaveChanges();
                var l = context.Korzina.Where(x => x.Id > 0).ToList();
                int ss = l.Sum(x => Convert.ToInt32(x.Count));
           
                counts.Text = ss.ToString();
            int it = l.Sum(x => Convert.ToInt32(x.Itog));
            money.Text = it.ToString() + " руб.";
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].Image == par)
                {
                    item[i].Text = r[0].Itog + " руб.";
                    items[i].Text = r[0].Count;
                    break;
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var context = new AppDbContext();
            string par = button.CommandParameter as string;
            
            var r = context.Korzina.Where(x => x.Image == par).ToList();
            var l = context.Korzina.Where(x => x.Id > 0).ToList();
           
            if (Convert.ToInt32(r[0].Count) > 1)
            {


                string prices = (Convert.ToInt32(r[0].Count) - 1).ToString();
                var h = context.Korzina.Where(x => x.Id == r[0].Id).AsEnumerable().Select(x => { x.Count = prices; return x; });
                foreach (var x in h)
                {
                    context.Entry(x).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                var h1 = context.Korzina.Where(x => x.Id == r[0].Id).AsEnumerable().Select(x => { x.Itog = (Convert.ToInt32(r[0].Count) * Convert.ToInt32(r[0].Price)).ToString(); return x; });
                foreach (var x in h1)
                {
                    context.Entry(x).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                context.SaveChanges();
               
                int ss = l.Sum(x => Convert.ToInt32(x.Count));
                counts.Text = ss.ToString();
                int it = l.Sum(x => Convert.ToInt32(x.Itog));
                money.Text = it.ToString() + " руб.";
                for (int i = 0; i < l.Count; i++)
                {
                    if (l[i].Image == par)
                    {
                        item[i].Text = r[0].Itog + " руб.";
                        items[i].Text = r[0].Count;
                        break;
                    }
                }
               
            }
        }

        private void bas_Click(object sender, RoutedEventArgs e)
        {
            katalog katalog = new katalog();
            katalog.Show();
            this.Close();
        }

        private void zakaz_Click(object sender, RoutedEventArgs e)
        {
            
            var context = new AppDbContext();
            var t = context.Korzina.ToList();
            MessageBox.Show($"Вы оформили заказ на сумму {t.Sum(x => Convert.ToInt32(x.Itog))} рублей! Возвращайтесь ещё!");
            context.Korzina.RemoveRange(t);
            context.SaveChanges();
            Basket basket = new Basket();
            basket.Show();
            this.Close();
            
        }
    }
}
