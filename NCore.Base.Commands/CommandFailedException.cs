using System;

namespace NCore.Base.Commands
{
  public class CommandFailedException : Exception
  {
    public CommandFailedException(string message, Exception error) : base(message, error)
    {
    }
  }
}