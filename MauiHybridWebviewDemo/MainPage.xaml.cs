namespace MauiHybridWebviewDemo;

public partial class MainPage : ContentPage
{


	public MainPage()
	{
		InitializeComponent();
	}

    private async void OnHybridWebViewRawMessageReceived(object sender, HybridWebView.HybridWebViewRawMessageReceivedEventArgs e)
    {

        if(e.Message == "open_camera")
        {
            await Dispatcher.DispatchAsync(async () =>
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                    if (photo != null)
                    {
                        // save the file into local storage
                        string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                        using Stream sourceStream = await photo.OpenReadAsync();
                        using FileStream localFileStream = File.OpenWrite(localFilePath);
                        await sourceStream.CopyToAsync(localFileStream);

                        // convert the file into base64 string
                        byte[] byteArray;
                        using (MemoryStream stream = new MemoryStream())
                        {   sourceStream.Position = 0;
                            await sourceStream.CopyToAsync(stream);
                            byteArray = stream.ToArray();
                        }
                        string base64String = Convert.ToBase64String(byteArray);

                        // Display the image in the HybridWebview
                        await myHybridWebview.InvokeJsMethodAsync("DisplayPhoto", base64String);

                    }
                    else
                    {
                        await DisplayAlert("Error", "An error occured while taking the photo.", "OK");
                    }
                }
            });
            return;
        }

        await Dispatcher.DispatchAsync(async () =>
        {
            await DisplayAlert("Message from JavaScript in C#!", e.Message, "OK");
        });
    }
}

