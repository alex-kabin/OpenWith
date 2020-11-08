namespace OpenWith.Model
{
    public class Style
    {
        public string BorderColor { get; set; }
        public int BorderThickness { get; set; }
        public string Margin { get; set; }
        public string Padding { get; set; }
        public string ForegroundColor { get; set; } = "Black";
        public string BackgroundColor { get; set; } = "White";
        public string FontFamily { get; set; } = "Arial";
        public string FontWeight { get; set; } = "Normal";
        public int FontSize { get; set; } = 14;
        public string FontStyle { get; set; } = "Normal";
    }
}