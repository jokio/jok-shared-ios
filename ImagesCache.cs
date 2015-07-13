using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using System.IO;

namespace Jok.Shared
{
	public class ImagesCache
	{
		static Dictionary<string, UIImage> Items;
		static string DocumentsPath, ImagesCachePath, NativeImagesPath;

		static ImagesCache ()
		{
			DocumentsPath = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			NativeImagesPath = "ChannelLogos";
			ImagesCachePath = Path.Combine (DocumentsPath, "ChannelLogos");

			Items = new Dictionary<string, UIImage> ();
		}


		public static UIImage FromUrl (string uri)
		{
			var imageName = uri.Substring(uri.LastIndexOf ('/') + 1);

			if (File.Exists (Path.Combine (NativeImagesPath, imageName)))
				return UIImage.FromFile (Path.Combine (NativeImagesPath, imageName));

			if (File.Exists (Path.Combine (ImagesCachePath, imageName)))
				return UIImage.FromFile (Path.Combine (ImagesCachePath, imageName));

			if (Items.ContainsKey (uri))
				return Items [uri];


			using (var url = new NSUrl (uri))
			using (var data = NSData.FromUrl (url)) {
				var image = UIImage.LoadFromData (data);

				if (!NSFileManager.DefaultManager.FileExists (ImagesCachePath))
					NSFileManager.DefaultManager.CreateDirectory (ImagesCachePath, false, null);

				var result = NSFileManager.DefaultManager.CreateFile (Path.Combine (ImagesCachePath, imageName), data, new NSFileAttributes ());
				if (!result)
					Items [uri] = image;

				return image;
			}
		}
	}
}
