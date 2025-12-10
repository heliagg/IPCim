using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace IPCim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<HostEntry> Entries { get; } = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            LoadEntries();
        }

        private void LoadEntries()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "csudh.txt");
            if (!File.Exists(filePath))
            {
                MessageBox.Show($"Nem található a fájl: {filePath}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Entries.Clear();

            foreach (var line in File.ReadLines(filePath).Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line) || !line.Contains(';'))
                    continue;

                var parts = line.Split(';');
                if (parts.Length < 2)
                    continue;

                Entries.Add(new HostEntry
                {
                    DomainName = parts[0],
                    IpAddress = parts[1]
                });
            }
        }
    }

    public class HostEntry
    {
        public string DomainName { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
    }
}