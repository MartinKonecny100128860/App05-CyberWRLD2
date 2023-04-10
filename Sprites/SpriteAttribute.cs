namespace App05MonoGame.Sprites
{
    public class SpriteAttribute
    {
        public string Name { get; set; }

        public int Value { get; set; }

        public int MaximumValue { get; }

        public int MinimumValue { get;}

        public int Increment { get; set; }

        public int Decrement { get; set; }

        private int startValue;

        public SpriteAttribute(int min, int max, int startValue)
        {
            Increment = 1;
            Decrement = 1;

            MaximumValue = max;
            MinimumValue = min;

            this.startValue = startValue;
            Value = startValue;

        }

        public void Increase()
        {
            if(Value < MaximumValue)
                Value += Increment;
        }

        public void Decrease()
        {
            if(Value > MinimumValue)
                Value -= Decrement;
        }

        public void Reset()
        {
            Value = startValue;
        }
    }
}
