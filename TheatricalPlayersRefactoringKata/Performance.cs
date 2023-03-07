namespace TheatricalPlayersRefactoringKata
{
    public class Performance
    {
        private string _playID;
        private int _audience;

        public string PlayID => _playID;
        public int Audience => _audience;

        public Performance(string playID, int audience)
        {
            _playID = playID;
            _audience = audience;
        }

    }
}
