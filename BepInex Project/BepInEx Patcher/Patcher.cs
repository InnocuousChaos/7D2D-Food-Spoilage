using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;

public static class Patcher
{
    // List of assemblies to patch
    public static IEnumerable<string> TargetDLLs { get; } = new[] { "Assembly-CSharp.dll" };

    // Patches the assemblies
    public static void Patch(AssemblyDefinition assembly)
    {
        var gm = assembly.MainModule.Types.First(d => d.Name == "ItemValue");
        var myTypeRef = gm.Fields.First(d => d.Name == "UseTimes");
        gm.Fields.Add(new FieldDefinition("NextSpoilageTick", FieldAttributes.Public, myTypeRef.FieldType));
        gm.Fields.Add(new FieldDefinition("CurrentSpoilage", FieldAttributes.Public, myTypeRef.FieldType));
    }
}