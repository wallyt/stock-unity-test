using System.Collections;
using System.Collections.Generic;
using System;

namespace DictionaryHelper {

	// DictionaryHelper is to get around the lack of tuples in Unity's C# compiler

	public struct closeDetails {
		public double closePrice;
		public long volume;
	}

	public class closeDictionary : Dictionary<DateTime, closeDetails> {
		public void Add(DateTime key, double val1, long val2) {
			closeDetails val;
			val.closePrice = val1;
			val.volume = val2;
			this.Add(key, val);
		}
	}
}