using System.Reflection;

var imageHelper = new ImageHelper();
Console.Out.WriteLine($"Version is: {Assembly.GetExecutingAssembly().GetName().Version}");

imageHelper.LoadImages(args[0], args[1]);
imageHelper.Resize();
var result = await imageHelper.SaveImagesAsync();
await imageHelper.CreateSortedImages();
await imageHelper.CreateOutputImage();