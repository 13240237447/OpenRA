namespace OpenRA
{
    public readonly struct CPos
    {
        // Coordinates are packed in a 32 bit signed int
		// X and Y are 12 bits (signed): -2048...2047
		// Layer is an unsigned byte
		// Packing is XXXX XXXX XXXX YYYY YYYY YYYY LLLL LLLL
		public readonly int Bits;

		// X is padded to MSB, so bit shift does the correct sign extension
		public int X => Bits >> 20;

		// Align Y with a short, cast, then shift the rest of the way
		// The signed short bit shift does the correct sign extension
		public int Y => ((short)(Bits >> 4)) >> 4;

		public byte Layer => (byte)Bits;

		public CPos(int bits) { Bits = bits; }
		public CPos(int x, int y)
			: this(x, y, 0) { }
		public CPos(int x, int y, byte layer)
		{
			Bits = (x & 0xFFF) << 20 | (y & 0xFFF) << 8 | layer;
		}

		public static readonly CPos Zero = new CPos(0, 0, 0);

		public static explicit operator CPos(int2 a) { return new CPos(a.X, a.Y); }

		public static CPos operator +(CVec a, CPos b) { return new CPos(a.X + b.X, a.Y + b.Y, b.Layer); }
		public static CPos operator +(CPos a, CVec b) { return new CPos(a.X + b.X, a.Y + b.Y, a.Layer); }
		public static CPos operator -(CPos a, CVec b) { return new CPos(a.X - b.X, a.Y - b.Y, a.Layer); }
		public static CVec operator -(CPos a, CPos b) { return new CVec(a.X - b.X, a.Y - b.Y); }

		public static bool operator ==(CPos me, CPos other) { return me.Bits == other.Bits; }
		public static bool operator !=(CPos me, CPos other) { return !(me == other); }

		public override int GetHashCode() { return Bits.GetHashCode(); }

		public bool Equals(CPos other) { return Bits == other.Bits; }
		public override bool Equals(object obj) { return obj is CPos && Equals((CPos)obj); }

		public override string ToString()
		{
			if (Layer == 0)
				return X + "," + Y;

			return X + "," + Y + "," + Layer;
		}

		public MPos ToMPos(Map map)
		{
			return ToMPos(map.Grid.Type);
		}

		public MPos ToMPos(MapGridType gridType)
		{
			if (gridType == MapGridType.Rectangular)
				return new MPos(X, Y);

			// Convert from RectangularIsometric cell (x, y) position to rectangular map position (u, v)
			//  - The staggered rows make this fiddly (hint: draw a diagram!)
			// (a) Consider the relationships:
			//  - +1x (even -> odd) adds (0, 1) to (u, v)
			//  - +1x (odd -> even) adds (1, 1) to (u, v)
			//  - +1y (even -> odd) adds (-1, 1) to (u, v)
			//  - +1y (odd -> even) adds (0, 1) to (u, v)
			// (b) Therefore:
			//  - ax + by adds (a - b)/2 to u (only even increments count)
			//  - ax + by adds a + b to v
			var u = (X - Y) / 2;
			var v = X + Y;
			return new MPos(u, v);
		}

    }
}
