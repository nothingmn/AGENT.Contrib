namespace AGENT.Contrib.Hardware
{
    public enum AgentButton
    {
		Empty = -1,
        TopLeft = 0,
        BottomLeft = 1,
        TopRight = 2,
        MiddleRight = 3,
        BottomRight = 4
    }

	public static class AgentButtonExtensions
	{
		public static AgentButton[] GetAll (this AgentButton self)
		{
			return new[] {
				             AgentButton.TopLeft,
				             AgentButton.BottomLeft,
				             AgentButton.TopRight,
				             AgentButton.MiddleRight,
				             AgentButton.BottomRight,
			             };
		}
		
		public static AgentButton[] GetAllLeft (this AgentButton self)
		{
			return new[] {
				             AgentButton.TopLeft,
				             AgentButton.BottomLeft,
			             };
		}

		public static AgentButton[] GetAllRight (this AgentButton self)
		{
			return new[] {
				             AgentButton.TopRight,
				             AgentButton.MiddleRight,
				             AgentButton.BottomRight,
			             };
		}
	}
}
