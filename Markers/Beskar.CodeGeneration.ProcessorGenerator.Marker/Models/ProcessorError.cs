namespace Beskar.CodeGeneration.ProcessorGenerator.Marker.Models;

/// <summary>
/// You can inherit from this class to add more information to the error.
/// </summary>
public class ProcessorError(int code, string? message = null)
{
   public int Code { get; set; } = code;

   public string? Message { get; set; } = message;

   public ProcessorError()
      : this(500)
   {
      
   }

   public ProcessorError(string message)
      : this(500, message)
   {
      
   }
   
   public static NoneProcessorError None => NoneProcessorError.Instance;
}

public class NoneProcessorError()
   : ProcessorError(200, "No error occurred")
{
   public static readonly NoneProcessorError Instance = new();
}