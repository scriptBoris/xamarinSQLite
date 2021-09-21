using FFImageLoading.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace XamarinSQLite.Elements
{
    public class ImageBase64 : CachedImage
    {
        //private CancellationToken cancel;

        public static readonly BindableProperty Base64SourceProperty =
            BindableProperty.Create(
                nameof(Base64Source),
                typeof(string),
                typeof(ImageBase64),
                null,
                propertyChanged: (b,o,n) =>
                {
                    var self = b as ImageBase64;
                    self.UpdateBase64(n as string);
                }
            );

        public string Base64Source
        {
            get => GetValue(Base64SourceProperty) as string;
            set => SetValue(Base64SourceProperty, value);
        }

        private void UpdateBase64(string base64)
        {
            //cancel.ThrowIfCancellationRequested();
            //cancel = new CancellationToken();

            if (base64 == null)
            {
                Source = null;
                return;
            }

            byte[] bytes = Convert.FromBase64String(base64);
            var mem = new MemoryStream(bytes);
            Source = ImageSource.FromStream(() => mem);
        }
    }
}
