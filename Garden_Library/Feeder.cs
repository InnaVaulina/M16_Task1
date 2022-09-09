namespace Garden_Library
{
    public class Feeder
    {
        int id;
        int food;
        int birdsCapacity;
        int birdsInside;

        public event FeederHendler FeederFillNotify;

        public int ID
        {
            get { return id; }
        }

        /// <summary>
        /// количество птичек внутри
        /// </summary>
        public int BirdsInside
        {
            get { return birdsInside; }
            set { birdsInside = value; }
        }

        /// <summary>
        /// количество корма
        /// </summary>
        public int Food
        {
            get { return food; }
        }

        /// <summary>
        /// создание кормушки 
        /// </summary>
        /// <param name="capacity">количество птичек, 
        /// которые поместятся в кормушку одновременно</param>
        public Feeder(int id, int capacity)
        {
            this.id = id;
            this.birdsCapacity = capacity;
            this.food = 0;
            this.BirdsInside = 0;
        }

        /// <summary>
        /// наполнение кормушки
        /// </summary>
        /// <param name="food"></param>
        public void FillFood(int food)
        {
            this.food += food;
            FeederFillNotify?.Invoke(new FeederEventArgs(this));
        }

        /// <summary>
        /// птичка занимает место в корушке
        /// </summary>
        /// <returns></returns>
        public bool LetBird()
        {
            if (this.BirdsInside < this.birdsCapacity)
            {
                this.BirdsInside++;
                return true;
            }
            else return false;
        }

        /// <summary>
        /// птичка улетает
        /// </summary>
        public void BirdFlewAway()
        {
            if (this.BirdsInside > 0) this.BirdsInside--;
        }

        /// <summary>
        /// кто-то ест корм
        /// </summary>
        public bool Exploit()
        {
            if (this.food > 0)
            { this.food--; return true; }
            else return false;
        }
    }
}
