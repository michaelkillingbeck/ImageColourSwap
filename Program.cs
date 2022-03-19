using System.Reflection;

var imageHelper = new ImageHelper();

Console.Out.WriteLine($"Version is: {Assembly.GetExecutingAssembly().GetName().Version}");

var sourceImageLocation = imageHelper.ImportImage("Images/milky_way.jpg");
var palletteImageLocation = imageHelper.ImportImage("Images/colour_run.jpg");

var sourceSize = imageHelper.GetImageSize(sourceImageLocation);
var palletteSize = imageHelper.GetImageSize(palletteImageLocation);

if(sourceSize.Size < palletteSize.Size)
{
    imageHelper.Resize(palletteSize, sourceImageLocation);
}
else if(palletteSize.Size < sourceSize.Size)
{
    imageHelper.Resize(sourceSize, palletteImageLocation);
}

var sortedSourceImageData = imageHelper.CreateSortedImage(sourceImageLocation);
var sortedPalletteImageData = imageHelper.CreateSortedImage(palletteImageLocation);

var sourceImageOutputLocation = imageHelper.CreateOutputImage(sourceImageLocation, sortedSourceImageData, sortedPalletteImageData);