using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Series;
using Validator;
using Utilitys;

namespace WPFDataVisualization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private PlotModel MyModel;

        public MainWindow()
        {
            InitializeComponent();

            Tuple<PlotModel, BarSeries> myModel = this.NewPlotAndBar();

            MyModel = myModel.Item1;

            PV_Main.Model = MyModel;
        }

        private Tuple<PlotModel, BarSeries> NewPlotAndBar()
        {
            PlotModel MyModel = new PlotModel() { Title = "Data View" };

            BarSeries BarSeries = new BarSeries();
            BarSeries.IsStacked = false;

            MyModel.Series.Add(BarSeries);

            return Tuple.Create(MyModel, BarSeries);
        }

        private void NUMBERSONLY(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SOMETHINGSONLY(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Random_Clicked(object sender, RoutedEventArgs e)
        {
            Tuple<PlotModel, BarSeries> myModel = this.NewPlotAndBar();

            Tuple<bool, int> Amount = Validator.Validator.CheckInt(TB_DataAmountInput.Text);

            if (!Amount.Item1)
            {
                return;
            }

            for (int i = 0; i < Amount.Item2; i++)
            {
                myModel.Item2.Items.Add(new BarItem(Utilitys.RandomNumber.Between()));
            }

            MyModel = myModel.Item1;
            TB_DataInput.Text = "";
        }

        private void Submit_Clicked(object sender, RoutedEventArgs e)
        {
            Tuple<bool, int> Amount = Validator.Validator.CheckInt(TB_DataAmountInput.Text);
            Tuple<bool, int[]> CustomInput = Amount.Item1 ? Validator.Validator.CheckIntArray(TB_DataInput.Text, MaxLength: Amount.Item2) : Validator.Validator.CheckIntArray(TB_DataInput.Text);

            if (CustomInput.Item1)
            {
                Tuple<PlotModel, BarSeries> myModel = this.NewPlotAndBar();

                foreach (int i in CustomInput.Item2)
                {
                    myModel.Item2.Items.Add(new BarItem(i));
                }

                MyModel = myModel.Item1;
            }

            PV_Main.Model = new PlotModel();
            PV_Main.Model = MyModel;
        }
    }
}