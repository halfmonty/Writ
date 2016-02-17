using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Writ
{
    /// <summary>
    /// Interaction logic for FullScreenWriting.xaml
    /// </summary>
    public partial class FullScreenWriting : Window
    {
        public FullScreenWriting()
        {
            InitializeComponent();
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        }

        private void FullScreenKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                windowClosing();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.KeyDown += new KeyEventHandler(FullScreenKeyDown);
            this.window.Activate();
            this.EditBox.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            windowClosing();
        }

        private void windowClosing()
        {
            EditBox.UpdateDocumentBindings();
            var mainwindow = Application.Current.MainWindow;
            if (mainwindow.WindowState == WindowState.Minimized)
                mainwindow.WindowState = WindowState.Normal;
            this.Close();
        }

        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            backLayer.Background = new SolidColorBrush(
                Color.FromRgb(ClrPcker_Background.SelectedColor.R, 
                ClrPcker_Background.SelectedColor.G, 
                ClrPcker_Background.SelectedColor.B));
        }

        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontFamily.SelectedItem != null)
                EditBox.TextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
        }

        private void cmbFontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            double value;
            if (Double.TryParse(cmbFontSize.Text, out value))
                EditBox.TextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, value);
        }

        private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object temp = EditBox.TextBox.Selection.GetPropertyValue(Inline.FontWeightProperty);
            //btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));
            temp = EditBox.TextBox.Selection.GetPropertyValue(Inline.FontStyleProperty);
            //btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));
            temp = EditBox.TextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            //btnUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

            temp = EditBox.TextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            cmbFontFamily.SelectedItem = temp;
            temp = EditBox.TextBox.Selection.GetPropertyValue(Inline.FontSizeProperty);
            cmbFontSize.Text = temp.ToString();
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Open);
                TextRange range = new TextRange(EditBox.TextBox.Document.ContentStart, EditBox.TextBox.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Rtf);
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                TextRange range = new TextRange(EditBox.TextBox.Document.ContentStart, EditBox.TextBox.Document.ContentEnd);
                range.Save(fileStream, DataFormats.Rtf);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditBox.TextBox.Document.Blocks.Clear();
        }

        private void SpellCheck_Clicked(object sender, MouseButtonEventArgs e)
        {
            if (EditBox.TextBox.SpellCheck.IsEnabled)
                EditBox.TextBox.SpellCheck.IsEnabled = false;
            else
                EditBox.TextBox.SpellCheck.IsEnabled = true;
        }
    }
}
