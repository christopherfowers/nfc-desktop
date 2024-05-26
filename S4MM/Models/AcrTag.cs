using System.Text;
using NfcRW;
using PCSC;

namespace S4MM.Models;

public class AcrTag: ACR122U
{
    public string ReadId { get; private set; }
    public string Data { get; private set; }
    public string Type { get; private set; }
    public bool CardPresent { get; private set; }

    public AcrTag()
    {
        try
        {
            Init(false, 50, 4, 4, 200); // NTAG213
            CardInserted += OnCardInserted;
            CardRemoved += OnCardRemoved;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void OnCardRemoved()
    {
        CardPresent = false;
    }

    private void OnCardInserted(ICardReader reader)
    {
        try
        {
            ReadId = BitConverter.ToString(GetUID(reader)).Replace("-", string.Empty);
            Data = Encoding.ASCII.GetString(ReadData(reader));
            Type = GetTagType(reader);
            CardPresent = true;
        }
        catch (Exception ex)
        {
            ReadId = string.Empty;
            CardPresent = false;
            throw;
        }
    }
}