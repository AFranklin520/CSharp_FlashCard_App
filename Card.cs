//Anthony Franklin afranklin18@cnm.edu
//FranklinP8
//04/23/2022
//Card

namespace FranklinP8
{
    public class Card
    {
        /// <summary>
        /// Fields
        /// </summary>
        private int numRight;
        private int numWrong;

        /// <summary>
        /// Properties
        /// </summary>
        public int CardID { get; set; }
        public string Title { get; set; }
        public string Answer { get; set; }
        public string Question { get; set; }
        public float RightWrongRatio { get; set; }
        public int NumWrong
        {
            get { return numWrong; }
            set { numWrong = value; Calc(); }
        }
        public int NumRight
        {
            get { return numRight; }
            set { numRight = value; Calc(); }
        }

        /// <summary>
        /// Methods
        /// </summary>
        public void Calc() { if (numRight != 0 && numWrong != 0) RightWrongRatio = NumRight / NumRight; }
        public override string ToString()
        {
            return $"{Title} {NumRight}/{NumWrong}";
        }

        /// <summary>
        /// Constructors
        /// </summary>
        public Card() : this(0, 0, 0, "", "", "") { }
        public Card(int numRight, int numWrong, int cardID, string title, string answer, string question)
        {
            NumRight = numRight;
            NumWrong = numWrong;
            CardID = cardID;
            Title = title;
            Answer = answer;
            Question = question;
        }
    }
}