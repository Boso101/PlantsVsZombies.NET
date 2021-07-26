﻿using System;
using System.Collections.Generic;

namespace Sexy.TodLib
{
	internal class TodWeightedGridArray
	{
		public static TodWeightedGridArray GetNewTodWeightedGridArray()
		{
			if (TodWeightedGridArray.unusedObjects.Count > 0)
			{
				return TodWeightedGridArray.unusedObjects.Pop();
			}
			return new TodWeightedGridArray();
		}

		private TodWeightedGridArray()
		{
		}

		public void Reset()
		{
			this.mX = (this.mY = (this.mWeight = 0));
		}

		public void PrepareForReuse()
		{
			this.Reset();
			TodWeightedGridArray.unusedObjects.Push(this);
		}

		public int mX;

		public int mY;

		public int mWeight;

		private static Stack<TodWeightedGridArray> unusedObjects = new Stack<TodWeightedGridArray>();
	}
}
