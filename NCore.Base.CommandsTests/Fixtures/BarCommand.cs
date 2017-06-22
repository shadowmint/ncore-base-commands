using NCore.Base.Commands;

namespace NCore.Base.CommandsTests.Fixtures
{
    public class BarCommand : ICommand
    {
        public float Value { get; set; }
    }
}