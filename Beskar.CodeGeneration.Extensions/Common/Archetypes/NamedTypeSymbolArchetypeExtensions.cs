using Beskar.CodeGeneration.Extensions.Common.Enums;
using Beskar.CodeGeneration.Extensions.Common.Specs;
using Beskar.CodeGeneration.Extensions.Models.Symbols.Archetypes;
using Me.Memory.Code;
using Microsoft.CodeAnalysis;

namespace Beskar.CodeGeneration.Extensions.Common.Archetypes;

public static class NamedTypeSymbolArchetypeExtensions
{
   extension(ref NamedTypeSymbolArchetype archetype)
   {
      public bool IsGuid => archetype.Symbol.IsGuid;

      public string GetClassStructModifiers(bool addPartial = false)
      {
         var writer = new CodeTextWriter(stackalloc char[256], stackalloc char[12]);

         try
         {
            var visibility = archetype.Symbol.Accessibility.ToKeywordString();
            writer.WriteInterpolated($"{visibility} ");

            if (archetype.Type.Kind is TypeKind.Class)
            {
               if (archetype.Symbol.IsStatic)
               {
                  writer.Write("static ");
               }
               else if (archetype.Symbol.IsAbstract)
               {
                  writer.Write("abstract ");
               }
               else if (archetype.Symbol.IsSealed)
               {
                  writer.Write("sealed ");
               }
               
               if (addPartial)
               {
                  writer.Write("partial ");
               }

               writer.Write(archetype.Type.IsRecord ? "record" : "class");
            }
            else if (archetype.Type.Kind is TypeKind.Struct)
            {
               if (archetype.Type.IsReadOnly)
               {
                  writer.Write("readonly ");
               }

               if (archetype.Type.IsRefLikeType)
               {
                  writer.Write("ref ");
               }

               if (addPartial)
               {
                  writer.Write("partial ");
               }

               if (archetype.Type.IsRecord)
               {
                  writer.Write("record ");
               }
               
               writer.Write("struct");
            }
            else
            {
               throw new InvalidOperationException();
            }
            
            return writer.ToString();
         }
         finally
         {
            writer.Dispose();
         }
      }
   }
}