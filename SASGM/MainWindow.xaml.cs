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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SASGM
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			GetSaveFiles();
		}


		private void loadSaveBtn_Click(object sender, RoutedEventArgs e)
		{
			if (saveListBox.SelectedItem == null)
			{
				MessageBox.Show("Please select a file to load!", "No file selected");
				return;
			}

			string fileToLoad = "saves\\" + saveListBox.SelectedItem.ToString().Replace("System.Windows.Controls.ListBoxItem: ","") + ".sav";

			if (!File.Exists(fileToLoad))
			{
				MessageBox.Show("Save file can't be located. Has it been moved or deleted?", "File not found");
				GetSaveFiles();
				return;
			}

			string pathToSave = "Save_" + saveSlotSelector.SelectedIndex + ".sav";
			try
			{
				File.Copy(fileToLoad, pathToSave, true);
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "Error");
			}
		}

		private void GetSaveFiles()
		{
			if(!Directory.Exists("saves"))
			{
				MessageBox.Show("Can't find the \"Saves\" directory", "Directory not found");
				return;
			}

			string[] files = Directory.GetFiles("saves");
			saveListBox.Items.Clear();
			foreach (string file in files)
			{
				if (!file.EndsWith(".sav")) continue;
				string saveName = file.Replace("saves\\", "").Replace(".sav", "");
				saveListBox.Items.Add(new ListBoxItem() { Content = saveName });
			}
		}

		private void refreshBtn_Click(object sender, RoutedEventArgs e)
		{
			GetSaveFiles();
		}
	}
}
