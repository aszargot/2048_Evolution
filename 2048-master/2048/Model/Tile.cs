using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using Game2048.Properties;

namespace Game2048.Model
{
    public class Tile : INotifyPropertyChanged
    {
        private int _value;
        private readonly Dictionary<int, Image> _imagesTable;
        private readonly Image _defaultImage = Resources._11;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged("Value");
                OnPropertyChanged("Image");
            }
        }

        public Image Image
        {
            get
            {
                if (_imagesTable.ContainsKey(Value))
                {
                    return _imagesTable[Value];
                }

                return _defaultImage;
            }
        }

        public Tile()
        {
            _imagesTable = new Dictionary<int, Image>
            {
                {0, Resources._0},
                {2, Resources._1 },
                {4, Resources._2 },
                {8, Resources._3 },
                {16, Resources._4 },
                {32, Resources._5 },
                {64, Resources._6 },
                {128,Resources._7 },
                {256, Resources._8 },
                {512, Resources._9 },
                {1024, Resources._10 },
                {2048, Resources._11 }
            };
        }

        protected void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
