using QRCoder;

public class QrCodeHelper
{
    public string GenerateQrCode(string uniqueCode)
    {
        using var qrGenerator = new QRCodeGenerator();
        var qrCodeData = qrGenerator.CreateQrCode(uniqueCode, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new PngByteQRCode(qrCodeData);
        var qrCodeImage = qrCode.GetGraphic(20);
        return Convert.ToBase64String(qrCodeImage);
    }
}