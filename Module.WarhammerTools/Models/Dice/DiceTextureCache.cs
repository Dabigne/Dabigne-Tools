using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Module.WarhammerTools.Models.Dice;

public static class DiceTextureCache
{
	private static readonly Dictionary<Color, WriteableBitmap> BitmapCache = new();

	public static WriteableBitmap GetNoisyBitmap(Color baseColor)
	{
		if (BitmapCache.TryGetValue(baseColor, out var cached))
			return cached;

		const int size = 128;
		var pixels = new int[size * size];
		var rng = new Random(baseColor.GetHashCode());

		for (int y = 0; y < size; y++)
		{
			for (int x = 0; x < size; x++)
			{
				double fineGrain = (rng.NextDouble() - 0.5) * 32.0;
				double swirl = Math.Sin(x * 0.12 + Math.Cos(y * 0.11) * 2.2) * 16.0;

				double fleckRoll = rng.NextDouble();
				double fleck = fleckRoll > 0.965 ? 48.0 : (fleckRoll < 0.025 ? -38.0 : 0.0);

				int delta = (int)(fineGrain + swirl + fleck);

				byte r = (byte)Math.Clamp(baseColor.R + delta, 0, 255);
				byte g = (byte)Math.Clamp(baseColor.G + delta, 0, 255);
				byte b = (byte)Math.Clamp(baseColor.B + delta, 0, 255);

				// Bgra8888 layout: B | (G << 8) | (R << 16) | (A << 24)
				pixels[y * size + x] = b | (g << 8) | (r << 16) | (255 << 24);
			}
		}

		var bitmap = new WriteableBitmap(
			new PixelSize(size, size),
			new Vector(96, 96),
			PixelFormat.Bgra8888,
			AlphaFormat.Premul);

		using (var fb = bitmap.Lock())
		{
			Marshal.Copy(pixels, 0, fb.Address, pixels.Length);
		}

		BitmapCache[baseColor] = bitmap;
		return bitmap;
	}
}