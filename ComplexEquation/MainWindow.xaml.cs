using System;
using System.Windows;
using System.Windows.Controls;

namespace ComplexEquation
{
    /// <summary>
    ///     MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ComplexEquationObj _equation;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChangeArgCount(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(ArgCountTextBox.Text, out var num))
                return;

            _equation = new ComplexEquationObj(num);

            ArgView.Children.Clear();

            for (var x = 0; x < num + 1; ++x)
            for (var y = -1; y < num; ++y)
            {
                if (y == -1)
                {
                    var newLabel = new Label
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top,
                        Margin = new Thickness(10 + x * 110 + (x == num ? 20 : 0), 40 + y * 30, 0, 0),
                        Height = 25,
                        Width = 100,
                        FontSize = 14,
                        Content = x == num ? "b" : "X" + x
                    };

                    ArgView.Children.Add(newLabel);

                    continue;
                }

                var newArgTextBox = new TextBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    TextWrapping = TextWrapping.Wrap,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(10 + x * 110 + (x == num ? 20 : 0), 40 + y * 30, 0, 0),
                    Height = 25,
                    Width = 100,
                    FontSize = 14
                };
                var argX = y;
                var argY = x;

                newArgTextBox.TextChanged += (o, args) =>
                {
                    LogBar.Content = "";
                    try
                    {
                        if (argY != num)
                            _equation.Set(argX, argY, new ComplexNumber(newArgTextBox.Text));
                        else
                            _equation.b[argX] = new ComplexNumber(newArgTextBox.Text);
                    }
                    catch (Exception exception)
                    {
                        LogBar.Content = "(" + argX + "," + argY + ")" + exception.Message;
                    }
                };

                ArgView.Children.Add(newArgTextBox);
            }
        }

        private void ButtonSolve_Click(object sender, RoutedEventArgs e)
        {
            var result = _equation.Solve();

            for (var i = 0; i < result.Count; ++i)
                MessageBox.Show("X" + i + " = " + result[i]);
        }
    }
}