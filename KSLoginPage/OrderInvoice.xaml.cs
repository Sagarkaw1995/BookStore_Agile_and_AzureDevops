using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace BookStoreGUI
{
     /// <summary>
     /// Interaction logic for OrderInvoice.xaml
     /// </summary>
     public partial class OrderInvoice : Window
     {
          public OrderInvoice()
          {
               InitializeComponent();
          }

          private void downloadbutton_click(object sender, RoutedEventArgs e)
          {

               //size of window
               Rect window = VisualTreeHelper.GetDescendantBounds(invoice);

               //create visual for the window
               DrawingVisual screen = new DrawingVisual();

               // create a brush with the window
               using (DrawingContext context = screen.RenderOpen())
               {
                    VisualBrush brush = new VisualBrush(invoice);
                    context.DrawRectangle(brush, null, new Rect(window.Size));
               }

               // Make a bitmap and render the window
               int width = (int)invoice.ActualWidth;
               int height = (int)invoice.ActualHeight;
               RenderTargetBitmap rtb = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
               rtb.Render(screen);

               // Make a PNG encoder.
               PngBitmapEncoder encoder = new PngBitmapEncoder();
               encoder.Frames.Add(BitmapFrame.Create(rtb));

               // Save the file.
               using (FileStream fs = new FileStream("invoice.png", FileMode.Create, FileAccess.Write, FileShare.None))
               {
                    encoder.Save(fs);

               }
          }
     }
}
