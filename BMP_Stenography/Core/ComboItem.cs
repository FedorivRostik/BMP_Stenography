﻿namespace BMP_Stenography.Core
{
    class ComboItem
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
