using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace OpenWith.Services.impl
{
    public class FileIconProvider : IIconProvider
    {
        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);

        private static ImageSource IconToImageSource(Icon icon) {            
            Bitmap bitmap = icon.ToBitmap();
            IntPtr hBitmap = bitmap.GetHbitmap();

            ImageSource wpfBitmap = Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions()
            );

            if (!DeleteObject(hBitmap)) {
                throw new Win32Exception();
            }

            return wpfBitmap;
        }

        public Task<ImageSource> GetIcon(string source) {
            Debug.WriteLine($"FileIconProvider::GetIcon({source}) at thread "+Thread.CurrentThread.ManagedThreadId);
            try {
                var icon = Icon.ExtractAssociatedIcon(source);
                return Task.FromResult(IconToImageSource(icon));
            }
            catch (Exception ex) {
                Debug.WriteLine("FileIconProvider::GetIcon - "+ex);
                throw;
            }
        }
    }
}
