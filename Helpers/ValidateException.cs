using System;
using System.Collections.Generic;

namespace LojaNemesis.Helpers
{
  public class ValidateException : Exception
  {
    public IEnumerable<string> Errors { get; set; }
  }
}