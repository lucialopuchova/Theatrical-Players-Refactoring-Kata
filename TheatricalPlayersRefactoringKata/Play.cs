namespace TheatricalPlayersRefactoringKata
{
    public class Play
    {
        private string _name;
        private string _type;

        public string Name => _name;
        public string Type => _type;

        public Play(string name, string type)
        {
            _name = name;
            _type = type;
        }
    }
}
