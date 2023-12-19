namespace PowerPoint
{
    public class Global
    {
        public Global()
        {
            TopLeftX = 0;
            TopLeftY = 0;
            BottomRightX = 0;
            BottomRightY = 0;
        }

        public static float TopLeftX
        {
            get; set;
        }

        public static float TopLeftY
        {
            get; set;
        }

        public static float BottomRightX
        {
            get; set;
        }

        public static float BottomRightY
        {
            get; set;
        }
    }
}