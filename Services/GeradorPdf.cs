using DinkToPdf;
using DinkToPdf.Contracts;
using System.Runtime.InteropServices;

namespace Pdf.Services
{
    public class GeradorPdf
    {
        private readonly IConverter _converter;

        public GeradorPdf()
        {
            _converter = new SynchronizedConverter(new PdfTools());
        }

        public byte[] GeneratePdf(string htmlContent)
        {
            var doc = new HtmlToPdfDocument
            {
                GlobalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                },
                Objects = { new ObjectSettings { HtmlContent = htmlContent } }
            };

            return _converter.Convert(doc);
        }

        private string GetPlatformSpecificLibrary()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return "libwkhtmltox.dll";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return "libwkhtmltox.so";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return "libwkhtmltox.dylib";

            throw new PlatformNotSupportedException("Sistema operacional não suportado.");
        }
    }
}
