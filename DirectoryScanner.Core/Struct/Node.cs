using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DirectoryScanner.Core.Struct {
	public abstract class Node : INotifyPropertyChanged {
		public abstract long? Size { get; }

		public string FormatedSize => Size.HasValue ? $"Size {Size.Value}" : "Size -";

		public string Name => Path.GetFileName(Fullpath);

		public string Fullpath;

		public DirectoryNode? Parent;

		public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "") {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		public float? Percent => Parent != null && Parent.Size.HasValue ? Size * 100 / ((float) Parent.Size) : null;

		public string FormatedPercent => Percent.HasValue ? $"Percent {Percent.Value:0.00}%" : "Percent -";

		public Node(string path, DirectoryNode? directoryNode) {
			Fullpath = path;
			Parent = directoryNode;
			if (Parent != null) {
				PropertyChanged += (x, y) => Parent.OnPropertyChanged(y.PropertyName);
			}

		}
	}
}
