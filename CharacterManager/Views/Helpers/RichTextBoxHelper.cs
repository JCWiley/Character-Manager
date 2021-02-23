using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

//Credit https://stackoverflow.com/questions/343468/richtextbox-wpf-binding
namespace CharacterManager.Views.Helpers
{
    public class RichTextBoxHelper : DependencyObject
    {
        public static string GetDocumentXaml(DependencyObject obj)
        {
            return (string)obj.GetValue(DocumentXamlProperty);
        }

        public static void SetDocumentXaml(DependencyObject obj, string value)
        {
            obj.SetValue(DocumentXamlProperty, value);
        }

        public static readonly DependencyProperty DocumentXamlProperty =
            DependencyProperty.RegisterAttached(
                "DocumentXaml",
                typeof(string),
                typeof(RichTextBoxHelper),
                new FrameworkPropertyMetadata
                {
                    BindsTwoWayByDefault = true,
                    PropertyChangedCallback = (obj, e) =>
                    {
                        var richTextBox = (RichTextBox)obj;

                    // Parse the XAML to a document (or use XamlReader.Parse())
                    var xaml = GetDocumentXaml(richTextBox);
                        var doc = new FlowDocument();
                        var range = new TextRange(doc.ContentStart, doc.ContentEnd);

                        byte[] DescriptionbyteArray = Encoding.ASCII.GetBytes(xaml);
                        using (MemoryStream ms = new MemoryStream(DescriptionbyteArray))
                        {
                            range.Load(ms, DataFormats.Rtf);
                        }

                        //range.Load(new MemoryStream(Encoding.UTF8.GetBytes(xaml)),
                        //      DataFormats.Xaml);

                        // Set the document
                        richTextBox.Document = doc;

                    // When the document changes update the source
                    range.Changed += (obj2, e2) =>
                        {
                            if (richTextBox.Document == doc)
                            {
                                MemoryStream buffer = new MemoryStream();
                                range.Save(buffer, DataFormats.Xaml);
                                SetDocumentXaml(richTextBox,
                                    Encoding.UTF8.GetString(buffer.ToArray()));
                            }
                        };
                    }
                });
    }
}