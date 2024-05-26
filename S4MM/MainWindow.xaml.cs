using System.Windows;
using PCSC;
using S4MM.Models;

namespace S4MM;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly AcrTag _acrTag;
    public MainWindow()
    {
        InitializeComponent();
        _acrTag = new AcrTag();
        _acrTag.Init(false, 50, 4, 4, 200);
        _acrTag.CardInserted += AcrTagOnCardInserted;
        _acrTag.CardRemoved += AcrTagOnCardRemoved;
    }

    private void AcrTagOnCardRemoved()
    {
        // Using Dispatcher to update the UI safely from a non-UI thread
        Dispatcher.Invoke(() => {
            NfcId.Text = string.Empty;
            NfcData.Text = string.Empty;
            NfcType.Text = string.Empty;
        });
    }

    private void AcrTagOnCardInserted(ICardReader reader)
    {
        Console.WriteLine("Card Inserted: {0}", _acrTag.ReadId);
        Dispatcher.Invoke(() => {
            NfcId.Text = _acrTag.ReadId;
            NfcData.Text = _acrTag.Data;
            NfcType.Text = _acrTag.Type;
        });
    }
}