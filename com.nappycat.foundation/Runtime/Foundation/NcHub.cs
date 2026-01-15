// Packages/com.nappycat.foundation/Runtime/Foundation/NcHub.cs
namespace NappyCat
{
    /// <summary>
    /// Global Nc hub. modules contribute partial members so gameplay code can use a single
    /// entry point:
    /// <code>
    /// using NappyCat;
    /// if (Nc.Input.ActionDown("Jump")) { ... }
    /// await Nc.Save.SaveAsync("slot1");
    /// Nc.Tween.To(...);
    /// </code>
    /// </summary>
    public static partial class Nc
    {
    }
}

